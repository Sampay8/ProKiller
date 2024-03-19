using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WeaponUI : MonoBehaviour
{
    public event Action<bool> VizorEnabled;
    public event Action ShotBtnPressed;
    public event Action<float> SliderValueChanged;

    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private Button _vizorBtn;
    [SerializeField] private Button _shotBtn;
    [SerializeField] private Slider _slider;
    [SerializeField] private StalkerCM _stalker;

    private bool _isVizorEnabled = false;
    private bool _isCanUseUIPanel = true;

    private void OnEnableUI(bool isPausa)
    {
        bool canShow = !isPausa && _isCanUseUIPanel;
        _uiPanel.SetActive(canShow);
    }
    
    private void Start()
    {
        OnEnableUI(true);
    }

    private void OnLevelWasLoaded(int level)
    {
        _isVizorEnabled = false;
        VizorEnabled?.Invoke(_isVizorEnabled);
    }

    private void OnEnable()
    {
        Pause.Signal.PausaModeChanged += OnEnableUI;
        _vizorBtn.onClick.AddListener(ChangeVizorState);   
        _shotBtn.onClick.AddListener(OnShot);   
        _slider.onValueChanged.AddListener(RunSliderEvent);
        _stalker.Escorted += OnBlockUI;
    }

    private void OnBlockUI(bool hide)
    {
        _isCanUseUIPanel = !hide;
        _uiPanel.SetActive(_isCanUseUIPanel);
    }

    private void OnDisable()
    {
        Pause.Signal.PausaModeChanged -= OnEnableUI;
        _vizorBtn.onClick.RemoveAllListeners();
        _shotBtn.onClick.RemoveAllListeners();
        _slider.onValueChanged.RemoveAllListeners();
        _stalker.Escorted -= OnBlockUI;
    }

    private void ChangeVizorState()
    {
        _isVizorEnabled = !_isVizorEnabled;
        VizorEnabled?.Invoke(_isVizorEnabled);

    }
    private void OnShot() => ShotBtnPressed?.Invoke();
    private void RunSliderEvent(float value) => SliderValueChanged?.Invoke(value);
}
