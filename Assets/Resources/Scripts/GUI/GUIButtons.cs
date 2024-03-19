using UnityEngine;
using UnityEngine.UI;
using UI;
using System;

public class GUIButtons : MonoBehaviour
{
    public event Action<MenuButton> NewWindowRequested;

    #region [MENU_PANEL_BUTTONS]
    [field: SerializeField] private Button _map;
    [field: SerializeField] private Button _liders;
    [field: SerializeField] private Button _settings;
    [field: SerializeField] private Button _shop;
    #endregion

    private void RequestWindow(MenuButton button)  => NewWindowRequested?.Invoke(button);

    private void OnEnable()
    {
        _map.onClick.AddListener(ShowMap);
        _liders.onClick.AddListener(UseLiders);
        _settings.onClick.AddListener(UseSettings);
        _shop.onClick.AddListener(UseShop);
    }


    private void OnDisable()
    {        
        _map.onClick.RemoveAllListeners();
        _liders.onClick.RemoveAllListeners();
        _settings.onClick.RemoveAllListeners();
        _shop.onClick.RemoveAllListeners();
    }

    private void ShowMap() => RequestWindow(MenuButton.Map); 
    private void UseLiders() => RequestWindow(MenuButton.Liders);
    private void UseSettings() => RequestWindow(MenuButton.Settings);
    private void UseShop() => RequestWindow(MenuButton.Shop);
}