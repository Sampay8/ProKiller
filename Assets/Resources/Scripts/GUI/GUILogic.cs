using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GUIButtons))]
public class GUILogic : MonoBehaviour
{
    private  EventBus _eventBus;
    private  List<Transform> _panels = new List<Transform>();

    #region [UI_ELEMENTS]
    [SerializeField] private Transform _pauseButton;
    [SerializeField] private Transform _menuButtonsPanel;
    [SerializeField] private Transform _mapPanel;
    [SerializeField] private Transform _lidersPanel;
    [SerializeField] private Transform _settingsPanel;
    [SerializeField] private Transform _upgradePanel;
    #endregion

    private void Awake()
    {
        _eventBus = EventBus.Current;
    }

    private void Start()
    {
        _panels.Add(_mapPanel);
        _panels.Add(_lidersPanel);
        _panels.Add(_settingsPanel);
    }

    private void OnEnable()
    {
        _eventBus.Subscribe<GoToMenuSignal>(ShowMenu);
        _eventBus.Subscribe<MapSignal>(ShowMap);
        _eventBus.Subscribe<ReitingsSignal>(ShowLiders);
        _eventBus.Subscribe<SettingsSignal>(ShowSettings);
        _eventBus.Subscribe<ShopSignals>(ShowShop);
        _eventBus.Subscribe<LevelIsPlayedSignal>(HideMainMenu);
    }
    private void OnDisable()
    {
        _eventBus.Unsubscribe<GoToMenuSignal>(ShowMenu);
        _eventBus.Unsubscribe<MapSignal>(ShowMap);
        _eventBus.Unsubscribe<ReitingsSignal>(ShowLiders);
        _eventBus.Unsubscribe<SettingsSignal>(ShowSettings);
        _eventBus.Unsubscribe<LevelIsPlayedSignal>(HideMainMenu);
    }

    private void HideMainMenu(LevelIsPlayedSignal signal)
    {
        HidePanels();
        _menuButtonsPanel.gameObject.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
    }

    private void ShowShop(ShopSignals signals)
    {
        WindowManager.ShowWindow<ShopDialog>();
    }

    private void ShowSettings(SettingsSignal signal)
    {
        HidePanels();
        _settingsPanel.gameObject.SetActive(true);
    }

    private void ShowLiders(ReitingsSignal signal)
    {
        HidePanels();
        _lidersPanel.gameObject.SetActive(true);
    }

    private void ShowMenu(GoToMenuSignal signal)
    {
        HidePanels();
        _lidersPanel.gameObject.SetActive(true);
        _menuButtonsPanel.gameObject.SetActive(true);
    }

    private void ShowMap(MapSignal signal)
    {
        HidePanels();
        _mapPanel.gameObject.SetActive(true);
    }


    private void HidePanels()
    {
        for (int i = 0; i < _panels.Count; i++)
            _panels[i].gameObject.SetActive(false);
    }

    
}