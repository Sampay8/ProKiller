using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector2> Vector2Changed;
    public event Action TochUsed;
    
    private  InputMap _input;
    
    private Vector2 _axis;
    public Vector2 Axis => _axis;

    private void Awake()
    {
        _input = new InputMap();
        SetOn();
    }

    private void OnEnable()
    {
        Pause.Signal.PausaModeChanged += Switch;
    }

    private void OnDisable()
    {
        Pause.Signal.PausaModeChanged -= Switch;
    }

    private void Switch(bool pausaValue)
    {
        if (pausaValue)
            SetOFF();
        else
            SetOn();
    }

    private void UseTouch()
    {
        Debug.Log("Mouse klicked");
        //TochUsed?.Invoke();
    }

    private void SetOn()
    {        
        _input.Enable();
        _input.Player.Vector2Delta.performed += ctx => DoRatate();
        _input.Player.Shot.performed += ctx => UseTouch();
    }

    private void SetOFF()
    {
        _input.Disable();
        _input.Player.Vector2Delta.performed -= ctx => DoRatate();
        _input.Player.Shot.performed -= ctx => UseTouch();
    }

    private void DoRatate()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
            if (Input.GetMouseButton(0))
            {
                Vector2Changed?.Invoke(_input.Player.Vector2Delta.ReadValue<Vector2>());
                _axis = _input.Player.Vector2Delta.ReadValue<Vector2>();
            }
    }   
}