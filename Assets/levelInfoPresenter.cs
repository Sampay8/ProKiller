using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelInfoPresenter : MonoBehaviour
{
    [SerializeField] private Transform _infoPresenter;
    
    private Image image;
    private EventBus _eventBus;
    private Button _loadButton;

    private void Start()
    {
        _eventBus = EventBus.Current;
        _eventBus.Subscrible<PreViewLevelSignal>(Present);
        image = _infoPresenter.GetComponentInChildren<Image>();
        _loadButton = _infoPresenter.GetComponentInChildren<Button>();
    }

    public void Present(PreViewLevelSignal signal)
    {
        AddButtonListener(signal.GetLevel());
        _infoPresenter.gameObject.SetActive(true);
        image.sprite = signal.Sprite;
    }

    private void AddButtonListener(Level level)
    {
        _loadButton.onClick.RemoveAllListeners();
        _loadButton.onClick.AddListener(level.Load);
    }
}
