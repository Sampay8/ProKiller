using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLogic : MonoBehaviour
{
    public event Action<Vector2> Vector2Changed;
    public event Action TochUsed;
        
    private InputMap _input;
    public InputMap Inputs => _input;

    private Vector2 _axis;

    public Vector2 Axis => _axis;

    private void UseTouch()
    {
        Debug.Log("Mouse klicked");
        //TochUsed?.Invoke();
    }

    private void OnEnable()
    {
        _input = new InputMap();
        _input.Enable();
        _input.Player.Vector2Delta.performed += ctx => DoRatate();
        _input.Player.Shot.performed += ctx => UseTouch();
    }

    private void OnDisable()
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