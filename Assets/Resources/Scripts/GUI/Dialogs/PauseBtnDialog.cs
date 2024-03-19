using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtnDialog : Window
{
    public event Action<MenuButton> NewWindowRequested;

    [SerializeField] private Button _pauseBtn;

    private void Awake()
    {
        _pauseBtn ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _pauseBtn.onClick.AddListener(SetPausa);
    }

    private void SetPausa()
    {
        NewWindowRequested?.Invoke(MenuButton.Menu);
    }
}
