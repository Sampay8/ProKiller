using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using Zenject;

public class AimVizor : MonoBehaviour
{
    private const float _minFOV = 3f;
    private const float _maxFOV = 45f;

    [SerializeField] private Camera _aimCamera;
    [SerializeField] private WeaponUI _weaponUI;

    private PlayerData _playerData;

    private CinemachineBasicMultiChannelPerlin _shake;
    private Coroutine _zoomRoutine;

    private float _curFOV;
    private float _targetFOV;
    private float _zoom;
    private float _zoomSpeed = 10f;

    [Inject]
    private void Construct(PlayerData data)
    {
        _playerData = data;
    }

    public RaycastHit[] GetTargetHit()
    {
        Ray ray = new Ray(_aimCamera.transform.position, _aimCamera.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        return hits;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _targetFOV = _maxFOV;
        _aimCamera.fieldOfView = _maxFOV;
        Debug.LogError("Aim");
    
    }

    private void OnEnable()
    {
        _weaponUI.SliderValueChanged += ChangeZoom;
    }

    private void ChangeZoom(float sliderValue)
    {
        _curFOV = _aimCamera.fieldOfView;
        sliderValue = Mathf.Clamp(sliderValue, 0.01f, 1f);
        float target = (sliderValue - 1) / -0.025f + 3;
        _targetFOV = Mathf.Clamp(target, _minFOV, _maxFOV);
        
        if (_targetFOV !=  _curFOV)
            AplyZoomValue();
    }

    private void AplyZoomValue()
    {
        if (_zoomRoutine != null)
        { 
            StopCoroutine(_zoomRoutine);
            _zoomRoutine = null;
        }
         _zoomRoutine = StartCoroutine(Zoom());
    }

    private void OnDisable()
    {
        _weaponUI.SliderValueChanged -= ChangeZoom;

        _targetFOV = _curFOV;
    }

    private IEnumerator Zoom()
    {
        while (_targetFOV != _curFOV)
        {
            _curFOV = Mathf.MoveTowards(_curFOV, _targetFOV, _zoomSpeed * Time.deltaTime);
            _aimCamera.fieldOfView = _curFOV;
            yield return null;
        }

        if (_zoomRoutine != null)
            StopCoroutine(_zoomRoutine);
        _zoomRoutine = null;
    }
}