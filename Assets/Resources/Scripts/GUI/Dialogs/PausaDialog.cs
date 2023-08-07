using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausaDialog :  Window
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _goToMenuButton;

    private void Awake()
    {
        EventBus.Current.Invoke(new LevelIsPausedSignal());
    }

    private void Start()
    {
        _continueButton.onClick.AddListener(Continue);
        _goToMenuButton.onClick.AddListener(GoToMenu);
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
        _goToMenuButton.onClick.RemoveAllListeners();
        _restartButton.onClick.RemoveAllListeners();
    }

    private void Continue()
    {
        EventBus.Current.Invoke(new LevelIsPlayedSignal());
        Hide();
    }

    private void Restart()
    {
        //RestartSignal
        Hide();
    }

    private void GoToMenu()
    {
        EventBus.Current.Invoke(new GoToMenuSignal());
        Hide();
    }
}
