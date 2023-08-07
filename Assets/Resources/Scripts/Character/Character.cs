using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    }

    private void Start()
    {
        IsAlive = true;
        IsMove = true;
        _movingRoutine = StartCoroutine(MovingCycle());
    }

    private void OnPlay(LevelIsPlayedSignal signal)
    {
        Walk();
    }


    private void OnStop(LevelIsPausedSignal signal)
    {
        Stand();
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
    }

    private void Walk()
    {
        _agent.speed = 1;
    }

    private void Stand()
    {
        _agent.speed = 0;
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

    private IEnumerator MovingCycle()
    {
        while (IsAlive)
        {
            if (IsMove == true)
            {
                if (_mover.GetDistanceToTarget() <= _minTargetDistance)
                {
                    IsMove = false;
                    Standing?.Invoke();

                    yield return _delay;
                    IsMove = true;
                    Walking?.Invoke();
                }
            }
            yield return null;  
        }
        StopCoroutine(_movingRoutine);
    }
}
public enum Motion
{
    Idle,
    WalkFwdLoop
}