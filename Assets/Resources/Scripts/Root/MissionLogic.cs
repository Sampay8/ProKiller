using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public  class MissionLogic
{
    public bool Completed { get; private set; }
    public bool Failed { get; private set; }

    private Timer _timer;
    private Spawner _spawner;
    private List< Character > _targetToKill;
    private bool _isMissionFailed;


    public MissionLogic(Timer timer, Spawner spawner)
    {
        Completed = false;
        Failed = false;
        _timer = timer;
        _spawner = spawner;
        InitTarget(_spawner.Pool);
        
    }

    private void InitTarget(List<Character> targets)
    {
        _targetToKill = targets;// пока так, позже создать отдельный список для целей

        for (int i = 0; i < _targetToKill.Count; i++)
            _targetToKill[i].Killed += CheckTarget;
    }

    public void CheckTarget(Character  target)
    {
        if (_targetToKill.Contains(target))
        { 
            _targetToKill.Remove(target);
            target.Killed -= CheckTarget;

            Completed = _targetToKill.Count <= 0;
        }
    }
}