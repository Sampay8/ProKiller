using UnityEngine;
using Zenject;
public class Game : MonoBehaviour
{

    [SerializeField] private MenuCamera _menuCamera;
    [SerializeField] private PlayerCamerasHolder _playerCamera;

    private PlayerInput _input;
    private GUISystem _gui;
    private CameraSelector _cameraSelector;
    private Coroutine _cor;

    private void Start()
    {
        Pause.Signal.PausaModeChanged += ChangeTimeScale;
        _cameraSelector = new CameraSelector(_menuCamera, _playerCamera);
    }

    private void ChangeTimeScale(bool pausa)
    {
        float scale = pausa ? 0.0f : 1.0f;
        Time.timeScale = scale;
        _cameraSelector.SetMode(pausa);
    }

    private void OnDestroy()
    {
        Pause.Signal.PausaModeChanged -= ChangeTimeScale;
    }
}
