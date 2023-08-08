using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class BrifingDialog : Window
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private ScrollRect _targrtsPresenter;

    private void Start()
    {
        _menuButton.onClick.AddListener(OnMenu);
        _startButton.onClick.AddListener(Play);
        _shopButton.onClick.AddListener(OnShop);
    }

    private new void Hide()
    {
        _menuButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();
        _shopButton.onClick.RemoveAllListeners();
        base.Hide();
    }

    private void OnShop()
    {
        //?
        WindowManager.ShowWindow<ShopDialog>();
    }

    private void Play()
    {
        EventBus.Current.Invoke(new LevelIsPlayedSignal());
        Hide();
    }

    private void OnMenu()
    {
        EventBus.Current.Invoke(new GoToMenuSignal());
        Hide();
    }
}
