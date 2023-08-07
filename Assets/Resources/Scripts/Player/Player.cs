using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public event Action Shooting;
    
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Camera _camera;

    private EventBus _eventBus;
    private LevelPresenter _presenter;
    private RaycastHit _hit;

    public void Init()
    {
        if (_camera == null)
            _camera = gameObject.GetComponentInChildren<Camera>();

        _weapon.Init();
    }

    private void Awake()
    {
        Init();
        _eventBus = EventBus.Current;
        _eventBus.Subscribe<LevelIsPlayedSignal>(ChangeAimState);
        
    }
    private void Start()
    {
        _eventBus.Invoke<WeaponIsReadySignal>(new WeaponIsReadySignal());
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<LevelIsPlayedSignal>(ChangeAimState);
    }

    private void ChangeAimState(LevelIsPlayedSignal signal) => _eventBus.Invoke<WeaponIsReadySignal>(new WeaponIsReadySignal(true));
    

    private void Shoot()
    {
        Shooting?.Invoke();
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Physics.Raycast(ray, out _hit);
        _weapon.DoShoot(target: _hit.point);
    }
}
