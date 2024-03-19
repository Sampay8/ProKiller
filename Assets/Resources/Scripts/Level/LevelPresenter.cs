using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelPresenter : MonoBehaviour
{
    public event Action LevelStarted;

    [SerializeField] private Timer _timer;
    [SerializeField] private PlayerCamerasHolder _playerCameraHolder;
    private ScriptableObject _levelData;
    private Spawner _spawner;
    private MissionLogic _missionLogic;
    private BrifingDialog _brifing;

    #region Subscrible
    private void OnEnable()
    {
        EventBus.Current.Subscrible<PreViewLevelSignal>(Prepare);
        EventBus.Current.Subscrible<LevelIsLoadedSignal>(PrepareLevel);
        _timer.TimerLaunched += LaunchLevel;
        _timer.TimeIsOver += ShowScore;
    }
    private void OnDisable()
    {
        EventBus.Current.Unsubscrible<PreViewLevelSignal>(Prepare);
        EventBus.Current.Unsubscrible<LevelIsLoadedSignal>(PrepareLevel);
        _timer.TimerLaunched -= LaunchLevel;
        _timer.TimeIsOver -= ShowScore;
    }
    #endregion

    private void Prepare(PreViewLevelSignal signal)
    {
        _levelData = signal.GetLevel().LevelData;
    }

    private void LaunchLevel()
    {
        Debug.LogError("ebash");
    }

    private void ShowScore()
    {
        Debug.LogError("Time Is Over");
    }


    private void PrepareLevel(LevelIsLoadedSignal signal)
    {
        _spawner = FindObjectOfType<Spawner>();
        _missionLogic = new MissionLogic(_timer, _spawner);
        ShowBrifing();

        _playerCameraHolder.StalkingCompletted += SetCompette;
    }

    private void SetCompette()
    {
        if (_missionLogic.Completed || _missionLogic.Failed)
        {
            WindowCreator.ShowWindow<MissionCompletteDialog>();
            _playerCameraHolder.StalkingCompletted -= SetCompette;
        }
    }


    private void ShowBrifing()
    {
        _brifing = WindowCreator.GetWindow<BrifingDialog>();
        _brifing.BrifingCompleted += StartLevel;
    }

    private void StartLevel()
    {
        _brifing.BrifingCompleted -= StartLevel;
        LevelStarted?.Invoke();
    }
}