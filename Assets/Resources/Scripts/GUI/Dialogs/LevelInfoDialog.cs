using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoDialog : Window
{
    [SerializeField]  private EventBus _eventBus;
    [SerializeField]  private Image image;
    [SerializeField] private Button _startButton;

    //реализовать outSide button => Hide;
    private void Awake()
    {
        EventBus.Current.Subscribe<PreViewLevelSignal>(OnPresentLoad);
    }
    private void OnPresentLoad(PreViewLevelSignal signal)
    {
        AddButtonListener(signal.GetLevel());
        image.sprite = signal.Sprite;
    }

    private void AddButtonListener(Level level)
    {
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.AddListener(level.Load);
        _startButton.onClick.AddListener(Hide);
    }
}
