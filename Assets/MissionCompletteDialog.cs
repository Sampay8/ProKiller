using UnityEngine;
using UnityEngine.UI;

public class MissionCompletteDialog : Window
{
    [SerializeField] private Button _continueButton;
    private void Awake()
    {
        EventBus.Current.Invoke(new LevelIsPausedSignal());
    }

    private void Start()
    {
        _continueButton.onClick.AddListener(Continue);
    }

    private void OnDestroy()
    {
        _continueButton.onClick.RemoveAllListeners();
    }

    private void Continue()
    {
        EventBus.Current.Invoke(new GoToMenuSignal());
        Hide();
    }
}
