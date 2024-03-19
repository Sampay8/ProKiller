using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class BrifingDialog : Window
{
    public event Action BrifingCompleted;

    [SerializeField] private Button _startButton;
    [SerializeField] private ScrollRect _targrtsPresenter;

    private void Start()
    {
        _startButton.onClick.AddListener(Play);
    }

    private new void Hide()
    {
        _startButton.onClick.RemoveAllListeners();
        base.Hide();
    }

    private void Play()
    {
        BrifingCompleted?.Invoke();
        EventBus.Current.Invoke(new BrifingIsFinishSignal());

        Hide();
    }
}
