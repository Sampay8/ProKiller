using System;
using System.Collections;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class Level : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private Sprite _uiBackground;
    [SerializeField] ScriptableObject _levelData;
    [SerializeField] private Button _presentButton;

    private WaitForSecondsRealtime _requestDelay = new WaitForSecondsRealtime(0.1f);

    public string Name => _sceneName;
    public Sprite PreView => _uiBackground;

    private void Awake()
    {
        _presentButton??= gameObject.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _presentButton.onClick.AddListener(OnPresent);
    }

    private void OnDisable()
    {
        _presentButton.onClick.RemoveAllListeners();
    }

    public void Load() => StartCoroutine(LoadRoutine());

    private IEnumerator LoadRoutine( Action onLoaded = null)
    {
        EventBus.Current.Invoke(new ShowCurtainSignal());
        AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(_sceneName);

        yield return _requestDelay;

        while (waitNextScene.isDone == false)
            yield return null;

        onLoaded?.Invoke();
        EventBus.Current.Invoke(new LevelIsLoadedSignal());
        EventBus.Current.Invoke(new HideCurtainSignal());
    }

    private void OnPresent()
    {
        WindowManager.ShowWindow<LevelInfoDialog>();
        EventBus.Current.Invoke(new PreViewLevelSignal(this));
    }
}