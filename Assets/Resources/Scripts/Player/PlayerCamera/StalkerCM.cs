using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CinemachineVirtualCamera))]

public class StalkerCM : MonoBehaviour
{
    public event Action<bool> Escorted;

    private Vector3 _startPosition;
    private CinemachineVirtualCamera _camera;
    private Transform _targetTransform;
    private Bullet _targetComponent;

    public bool IsFollow { get; private set; }

    private void Awake()
    {
        _camera ??= GetComponent<CinemachineVirtualCamera>();
        _startPosition = transform.position;
    }

    private void OnEnable()
    {
        Escorted?.Invoke(true);
    }
    private void OnDisable()
    {
        Escorted?.Invoke(false);
    }

    public void Init(Bullet bullet)
    {
        _targetComponent = bullet;
        _targetTransform = bullet.transform;

        Escort();
    }

    public void Escort()
    {
        gameObject.SetActive(true);
        if (_targetComponent == null)
            Debug.LogError("Bullet is null!");

        _camera.transform.position = _startPosition;
        _camera.LookAt = _targetTransform;
        _camera.Follow = _targetTransform;

        StartCoroutine(Folow());
    }

    private IEnumerator Folow()
    {
        IsFollow = true;
        _targetComponent.Triggered += CompletteFollow;
        CinemachineBrain.SoloCamera = _camera;
        
        while (_targetTransform.gameObject.activeInHierarchy == false)
            yield return null;

        while (_camera.Follow != null)
            yield return null;
        CinemachineBrain.SoloCamera = null;
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        IsFollow = false;
    }

    private void CompletteFollow()
    {
        _camera.Follow = null;
        _targetComponent.Triggered -= CompletteFollow;
    }
}
