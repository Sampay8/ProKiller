using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CharacterFSM;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterRagDoll))]

public class Character : MonoBehaviour
{
    public const string HeadName = "head";
    
    private const float _minTargetDistance = 0.8f;

    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _health = 100.0f;

    public Transform _curTarget;


    public event Action<Character> Killed;
    public event Action<bool> MovmentFreezing;
    public event Action<Motion> MotionChanged;
    public event Action Scared;
    

    public bool IsAlive { get; private set; }
    public bool IsMove { get; private set; }

    [SerializeField] private CharacterWayPoint _wayPoints;
    private CharacterAnimator _anim;
    private CharacterMover _mover;
    private CharacterSM _sm;
    private Motion motion;

    #region Subscrible

    private void OnEnable()
    {
        EventBus.Current.Subscrible<OnShootSignal>(StartFreze);
        EventBus.Current.Subscrible<FollowCompletteSignal>(StopFreze);
        EventBus.Current.Subscrible<HideMainMenuSignal>(OnPlay);
        EventBus.Current.Subscrible<LevelIsPausedSignal>(OnStop);
        EventBus.Current.Subscrible<TimeStartSignal>(Begin);
    }

    private void OnDisable()
    {
        EventBus.Current.Unsubscrible<OnShootSignal>(StartFreze);
        EventBus.Current.Unsubscrible<FollowCompletteSignal>(StopFreze);
        EventBus.Current.Unsubscrible<HideMainMenuSignal>(OnPlay);
        EventBus.Current.Unsubscrible<LevelIsPausedSignal>(OnStop);
        EventBus.Current.Unsubscrible<TimeStartSignal>(Begin);
    }

    #endregion

    #region Subscribles
    private void StartFreze(OnShootSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(true);
    }

    private void StopFreze(FollowCompletteSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(false);
    }

    private void OnPlay(HideMainMenuSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(false);
    }

    private void OnStop(LevelIsPausedSignal signal)
    {
        if (IsAlive)
            MovmentFreezing?.Invoke(true);
    }

    private void Begin(TimeStartSignal signal)
    {
        Move();
    }
    #endregion

    public void Init(CharacterWayPoint wayPoints, List<Vector3> spawnPos )
    {
        IsAlive = true;
        _wayPoints = wayPoints;
        transform.position = spawnPos[0];
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void ChangeMotion(Motion motion ) => MotionChanged?.Invoke(motion);

    public void GetDamage(Collider hit, Bullet bullet)
    {
        if (hit.transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        { 
            string shootPoint = rb.transform.name;
            bool isHeadShot = shootPoint == HeadName;

            if (isHeadShot)
                ApplyDamage(value: _health);
        
            else
                ApplyDamage(value: bullet.Damage);
                
        }

        if (IsAlive == false)
            Death(rb, bullet);
            
        else
            Scared?.Invoke();
    }

    private void ApplyDamage(float value)
    {
        _health = Mathf.Max(0, _health - value);
        IsAlive = _health > 0;
    }

    private void Death(Rigidbody rb, Bullet bullet)
    {
        rb.transform.localPosition += bullet.transform.forward * Time.deltaTime;//не корректно, исправить
        Killed?.Invoke(this);
    }

    private void Awake()
    {
        _wayPoints = FindObjectOfType<CharacterWayPoint>();
        _anim = new CharacterAnimator(this, gameObject.GetComponent<Animator>());
        _mover = new CharacterMover(this, gameObject.GetComponent<NavMeshAgent>());
        _sm = new CharacterSM(this, _anim, _mover);
    }

    private void FixedUpdate() 
    {
        if(_curTarget != null)
            _navMeshAgent.destination = _curTarget.position;

        _sm.Update();
    
    }
    
    private void Move() 
    {
        IsMove = true;
        Debug.LogError("character  MOve");
    }

    private void Stand() =>  IsMove = false; 

    internal Vector3 GetWay()
    {
        return _wayPoints.GetNewPointToMove();
    }

    //private void Update()
    //{
    //    if(Input.anyKey)
    //    {
    //        if (Input.GetKeyDown(KeyCode.T))
    //            motion = Motion.Idle;
    //        if (Input.GetKeyDown(KeyCode.W))
    //            motion = Motion.WalkFwdLoop;
    //        if (Input.GetKeyDown(KeyCode.E))
    //            motion = Motion.Scared;
    //        if (Input.GetKeyDown(KeyCode.R))
    //            motion = Motion.RunFwdLoop;

    //        MotionChanged(motion);
    //        Debug.ClearDeveloperConsole();
    //        Debug.LogError(motion.ToString());
    //    }
    //}
}

public enum Motion
{
    Idle,
    WalkFwdLoop,
    RunFwdLoop,
    Scared
}