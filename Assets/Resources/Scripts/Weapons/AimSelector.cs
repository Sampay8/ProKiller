using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSelector 
{
    private const string _levelIsLoadedSignal = "LevelIsLoadedSignal";
    private const string _timeStartSignal = "TimeStartSignal";
    private const string _onShootSignal = "OnShootSignal";
    private const string _followCompletteSignal = "FollowCompletteSignal";
    private const string _levelIsPausedSignal = "LevelIsPausedSignal";
    private const string _levelIsPlayedSignal = "LevelIsPlayedSignal";

    public event Action<bool> StateChanged;

    private bool _isEqup = false;
    private bool _isPlay = false;

    public AimSelector()
    {
        EventBus.Current.Subscrible<OnShootSignal>(ChangeVisibleMode);
        EventBus.Current.Subscrible<LevelIsLoadedSignal>(ChangeVisibleMode);
        EventBus.Current.Subscrible<FollowCompletteSignal>(ChangeVisibleMode);
        EventBus.Current.Subscrible<TimeStartSignal>(ChangeVisibleMode);
        EventBus.Current.Subscrible<LevelIsPausedSignal>(ChangeVisibleMode);
        EventBus.Current.Subscrible<LevelIsPlayedSignal>(ChangeVisibleMode);
    }

    private void ChangeVisibleMode(ISignal shoot)
    {
        bool value;
        string shootType = shoot.GetType().ToString();
      
        if (shootType == _levelIsLoadedSignal)
            _isEqup = false;
        
        if (shootType == _timeStartSignal)
            _isEqup = true;

        if (shootType == _onShootSignal)
            _isEqup = false;
        
        if (shootType == _followCompletteSignal)
            _isEqup = true;
        
        if (shootType == _levelIsPausedSignal)
            _isPlay = false;

        if (shootType == _levelIsPlayedSignal)
            _isPlay = true;

        value = _isPlay && _isEqup;
        StateChanged?.Invoke(value);
    }
}