using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelInfoDialog : Window
{
    [SerializeField]  private EventBus _eventBus;
    [SerializeField]  private Image image;
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        EventBus.Current.Subscrible<PreViewLevelSignal>(OnPresentLoad);
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

    private void OnDestroy()
    {
        EventBus.Current.Unsubscrible<PreViewLevelSignal>(OnPresentLoad);
    }

}
