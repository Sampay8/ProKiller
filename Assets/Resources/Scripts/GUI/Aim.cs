using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject _aimVisor;
    [SerializeField] private float _zoomRange => _playerData.WeaponZoom;
    [SerializeField] private Slider _slider;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private const float _minFOV = 1f;
    private const float _maxFOV = 61f;

    private float _curFOV = _maxFOV;
    private float _targetFOV;

    private float _zoom;
    private PlayerData _playerData;
    private float _curZoom = 1f;
    private float _targetZoom;
    private float _zoomSpeed = 10f;
    private Coroutine _zoomRoutine;

    private void OnEnable()
    {
        EventBus.Current.Subscribe<WeaponIsReadySignal>(ShowAim);
        EventBus.Current.Subscribe<WeaponOnShoot>(Hide);
        EventBus.Current.Subscribe<EscortBulletComlette>(Show);
        EventBus.Current.Subscribe<LevelIsPausedSignal>(Hide);
    }

    private void ShowAim(WeaponIsReadySignal signal) => _aimVisor.SetActive(signal.State);

    private void Show(EscortBulletComlette signal) =>
        _aimVisor.SetActive(true);

    private void Show(LevelIsPlayedSignal signal) =>
        _aimVisor.SetActive(true);

    private void Hide(WeaponOnShoot signal) =>
        _aimVisor.SetActive(false);

    private void Hide(LevelIsPausedSignal signal) =>
        _aimVisor.SetActive(false);



    private void OnDisable()
    {
        EventBus.Current.Unsubscribe<WeaponIsReadySignal>(ShowAim);
        EventBus.Current.Unsubscribe<WeaponOnShoot>(Hide);
        EventBus.Current.Unsubscribe<EscortBulletComlette>(Show);
        EventBus.Current.Subscribe<LevelIsPlayedSignal>(Show);
    }

    private void Awake()
    {
        _playerData = GameObject.FindObjectOfType<GameRoot>().PlayerData;
        _camera.m_Lens.FieldOfView = _maxFOV;
    }

    private void Start()
    {
        _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _playerData.AimShake;
    }

    private void LateUpdate()
    {
        _zoom = (_slider.value * _zoomRange) ;
        _targetFOV = Mathf.Clamp(_maxFOV / _zoom, _minFOV, _maxFOV);

        if (_zoomRoutine == null && _targetFOV != _curFOV)
            _zoomRoutine = StartCoroutine(Zoom());
    }

    private IEnumerator Zoom()
    {
        while (_targetFOV != _curFOV)
        {
            _curFOV = Mathf.MoveTowards(_curFOV, _targetFOV, _zoomSpeed * Time.deltaTime);
            _camera.m_Lens.FieldOfView = _curFOV;
            yield return null;
        }

        if (_zoomRoutine != null)
            StopCoroutine(_zoomRoutine);
        _zoomRoutine = null;
    }
}