using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterRagDoll))]
[RequireComponent(typeof(CharacterMover))]

public class Character : MonoBehaviour
{
    private const float _minTargetDistance = 1.0f;

    public event Action Killed;
    public event Action<bool> MovmentFreezing;
    public event Action<Motion> MotionChanged;
    public event Action Walking;
    public event Action Standing;

    [SerializeField] private CharacterMover _mover;
    [SerializeField] private CharacterRagDoll _ragDoll;


    private Coroutine _movingRoutine;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(5.0f);
    private NavMeshAgent _agent;
    private CharacterAnimator _anim;

    public bool IsAlive { get; private set; }
    public bool IsMove { get; private set; }

    private void Awake()
    {
        IsAlive = true;
        _mover ??= transform.GetComponent<CharacterMover>();
        _ragDoll ??= transform.GetComponent<CharacterRagDoll>();
        _agent ??= transform.GetComponent<NavMeshAgent>();
        _anim ??= new CharacterAnimator(this, gameObject.GetComponent<Animator>());

    }

    private void OnEnable()
    {
        EventBus.Current.Subscribe<WeaponOnShoot>(StartFreze);
        EventBus.Current.Subscribe<WeaponCompletteShoot>(StopFreze);
        EventBus.Current.Subscribe<LevelIsPlayedSignal>(OnPlay);
        EventBus.Current.Subscribe<LevelIsPausedSignal>(OnStop);
        EventBus.Current.Subscribe<TimeStartSignal>(Begin);
    }

    private void Begin(TimeStartSignal signal)
    {
        IsAlive = true;
        Move();
        _movingRoutine = StartCoroutine(MovingCycle());
    }

    public void GetDamage(Collision collision, Transform bulletTransform)
    {
        IsAlive = false;
        _mover.enabled = false;
        _agent.enabled = false;
        _ragDoll.Death();
        collision.transform.position += bulletTransform.forward * Time.deltaTime;
        Killed?.Invoke();
    }


    private void OnPlay(LevelIsPlayedSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(false);
    }

    private void OnStop(LevelIsPausedSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(true);
    }

    private void StartFreze(WeaponOnShoot signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(true);
    }

    private void StopFreze(WeaponCompletteShoot signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(false);
    }

    private void OnDisable()
    {
        EventBus.Current.Unsubscribe<WeaponOnShoot>(StartFreze);
        EventBus.Current.Unsubscribe<WeaponCompletteShoot>(StopFreze);
        EventBus.Current.Unsubscribe<LevelIsPlayedSignal>(OnPlay);
        EventBus.Current.Unsubscribe<LevelIsPausedSignal>(OnStop);
        EventBus.Current.Unsubscribe<TimeStartSignal>(Begin);
    }


    private IEnumerator MovingCycle()
    {
        while (IsAlive)
        {
            if (IsMove == true)
            {
                if (_mover.GetDistanceToTarget() <= _minTargetDistance)
                {
                    Stand();
                    yield return _delay;
                    Move();
                }
            }
            yield return null;  
        }
        StopCoroutine(_movingRoutine);
    }

    private void Move()
    {
        IsMove = true;
        Walking?.Invoke();
    }

    private void Stand()
    {
        IsMove = false;
        Standing?.Invoke();
    }
}


public enum Motion
{
    Idle,
    WalkFwdLoop
}

//public class CharacterSM
//{ 
//    public readonly CharacterSM SM;

//    public CharacterSM(Character character, CharacterMover mover, CharacterAnimator animator)
//    {
//        SM = this;
//    }


//    public void Enter() { }
//    public void Exit() { }
//    public void Update() { }


//}