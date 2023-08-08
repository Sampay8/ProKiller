using UnityEngine;
using UnityEngine.UI;
using UI;

public class GUIButtons : MonoBehaviour
{
    private EventBus _eventBus;

    #region [BUTTONS]
    [SerializeField] private Button _pause;
    [SerializeField] private Button _map;
    [SerializeField] private Button _liders;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _selectLevel;
    [SerializeField] private Button _startScene;
    #endregion

    private void Awake()
    {
        _eventBus = EventBus.Current;
    }

    private void OnEnable()
    {
        _pause.onClick.AddListener(UsePausa);
        _map.onClick.AddListener(UseMap);
        _liders.onClick.AddListener(UseLiders);
        _settings.onClick.AddListener(UseSettings);
        _shop.onClick.AddListener(UseShop);
    }

    private void OnDisable()
    {
        _pause.onClick.RemoveAllListeners();
        _map.onClick.RemoveAllListeners();
        _liders.onClick.RemoveAllListeners();
        _settings.onClick.RemoveAllListeners();
        _shop.onClick.RemoveAllListeners();
    }

    private void UsePausa()
    {
        WindowManager.ShowWindow<PausaDialog>();
        _pause.gameObject.SetActive(false);
    }
    
    

    private void UseMap() => _eventBus.Invoke(new MapSignal());
    private void UseLiders() => _eventBus.Invoke(new ReitingsSignal());
    private void UseSettings() => _eventBus.Invoke(new SettingsSignal());
    private void UseShop() => _eventBus.Invoke(new ShopSignals());
}
