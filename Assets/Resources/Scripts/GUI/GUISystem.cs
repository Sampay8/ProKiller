using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(LevelPresenter))] 

public class GUISystem : MonoBehaviour
{
    public event Action<bool> PausaChanged;

    [SerializeField] private LevelPresenter _levelPresenter;
    [SerializeField] private Button _pausaBtn;

    private bool _isPausa = false;
    private Pause _pausaSignalRouter;

    private GUIButtons _menuUi;
    private Window _activWindow;
    private Curtain _curtain;
    private Transform _menuButtonsPanel => _menuUi.transform;
    private WeaponUI _weaponUI;

    [Inject]
    private void Construct()
    {
        _pausaSignalRouter = new Pause(this);
    }

    private void OnPausaClick()
    {
        _isPausa = !_isPausa;
        ChangePauseMode(_isPausa);
    }

    private void OnLevelWasLoaded(int level)
    {
        ChangePauseMode(false);
        ShowLvllUI();
    }

    private void Awake()
    {
        _curtain ??= GetComponentInChildren<Curtain>();
        _levelPresenter ??= GetComponent<LevelPresenter>();
        _weaponUI ??= FindObjectOfType<WeaponUI>();
    }

    private void Start()
    {
        _pausaBtn.gameObject.SetActive(false);
        _pausaBtn?.onClick.AddListener(OnPausaClick);
        _menuUi = WindowCreator.GetWindow<MenuButtonsDialog>().GetComponent<GUIButtons>();
        _menuUi.NewWindowRequested += ChangeActivWindow;
        _levelPresenter.LevelStarted += ShowLvllUI;
    }

    private void ChangePauseMode(bool pauseValue) 
    {        
        _isPausa = pauseValue;

        if(_isPausa)
            ShowMainMenuUI();
        else
            ShowLvllUI();

        PausaChanged?.Invoke(_isPausa);
    }

    private void ShowMainMenuUI() => 
        _menuButtonsPanel.gameObject.SetActive(true);

    private void HideMainMenuUI()
    { 
        _menuButtonsPanel.gameObject.SetActive(false);
        
        if(_activWindow != null)
            Destroy(_activWindow.gameObject);
    }

    private void ShowLvllUI()
    { 
        _pausaBtn.gameObject.SetActive(true);
        HideMainMenuUI();
    }


    private void ChangeActivWindow(MenuButton button)
    {
        Window newWindow =  new() ;
        switch (button)
        {
            case MenuButton.Pausa:
                newWindow = WindowCreator.GetWindow<PausaDialog>();
                break;
            case MenuButton.Map:
                newWindow = WindowCreator.GetWindow<MapDialog>();
                break;
            case MenuButton.Liders:
                newWindow = WindowCreator.GetWindow<LidersInfoDialog>();
                break;
            case MenuButton.Settings:
                newWindow = WindowCreator.GetWindow<SettingsDialog>();
                break;
            case MenuButton.Shop:
                newWindow = WindowCreator.GetWindow<ShopDialog>();
                break;
            case MenuButton.LevelInfo:
                newWindow = WindowCreator.GetWindow<LevelInfoDialog>();
                break;
            default:Debug.LogError("MenuButtonError");
                break;
        }

        if (_activWindow == null)
        {
            _activWindow = newWindow;
        }
        else
        {
            if (_activWindow.GetType() == newWindow.GetType())
            {
                Destroy(newWindow.gameObject);
                Destroy(_activWindow.gameObject);
            }
            else
            { 
                Destroy(_activWindow.gameObject);
                _activWindow = newWindow;
            }
        }
    }

    private void OnDestroy()
    {
        _menuUi.NewWindowRequested -= ChangeActivWindow;
        _pausaBtn.onClick.RemoveAllListeners();
        _levelPresenter.LevelStarted -= ShowLvllUI;
    }

    private void ShowCurtain(ShowCurtainSignal signal) => _curtain.Show();
    private void HideCurtaine(LevelIsLoadedSignal signal) => _curtain.Hide();
}