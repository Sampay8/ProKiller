using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class CharacterMover : MonoBehaviour
{
    private const float _minSpeed =.01f;
    private const float _maxSpeed =1.0f;

    [SerializeField] private Character _character;
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField] private WayPoint _wayPoints;

    private Transform _curTarget;
    private Transform _newTarget;

    public float GetDistanceToTarget()
    {
        return Vector3.Distance(transform.position, _curTarget.position);
    }

    private void Awake()
    {
        _agent??= gameObject.GetComponent<NavMeshAgent>();
        _character ??= transform.GetComponent<Character>();
        _wayPoints ??= FindObjectOfType<WayPoint>();
        StartCoroutine(GetNewPoinToWai());
    }

    private void OnEnable()
    {
        _character.MovmentFreezing += FreezeMoving;
        _character.Walking += FindWaiPoint;
        _character.MotionChanged += ChangeSpeed;
    }
        
    private void FindWaiPoint() =>
        StartCoroutine(GetNewPoinToWai());

    private void FreezeMoving(bool value)
    {
        if (value)
            _agent.speed = _minSpeed;
        else
            _agent.speed = _maxSpeed;
    }

    private void ChangeSpeed(Motion motion)
    {
        switch (motion)
        {
            case Motion.Idle:
                _agent.speed = _minSpeed;
                break;
            case Motion.WalkFwdLoop:
                _agent.speed = _maxSpeed;
                break;
        }
    }

        private void OnDisable()
    {
        _character.MovmentFreezing -= FreezeMoving;
        _character.Walking -= FindWaiPoint;
        _character.MotionChanged -= ChangeSpeed;
    }

    private IEnumerator GetNewPoinToWai()
    {
        do
            _newTarget = _wayPoints.GetNewPointToMove();
        while (_curTarget == _newTarget);
        
        _curTarget = _newTarget;
        _agent.SetDestination(_curTarget.position);
        yield return null;
    }
}