using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class CharacterMover
{
    private const float _minSpeed =.01f;
    private const float _defoultSpeed =1.0f;

    private Character _character;
    private NavMeshAgent _agent;

    public CharacterMover(Character character, NavMeshAgent agent)
    {
        _character = character;
        _agent = agent;
        _character.Killed += Disable;
        _character.MotionChanged += ChangeMovment;
        _character.MovmentFreezing += FreezeMoving;

    }

    private void Disable(Character obj)
    {
        _agent.enabled = false;
    }

    private void ChangeMovment(Motion motion)
    {
        switch (motion)
        {
            case Motion.Idle:
                _agent.speed = 0;
                break;
            case Motion.WalkFwdLoop:
                _agent.speed = _defoultSpeed;
                break;
        }
    }

    public void SetMoveTarget(Vector3 newTarget) => _agent.destination = newTarget;


    private void FreezeMoving(bool value)
    {
        if (value)
            _agent.speed = _minSpeed;
        else
            _agent.speed = _defoultSpeed;
    }
}