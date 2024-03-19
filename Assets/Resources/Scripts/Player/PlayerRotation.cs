using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Player))]

public class PlayerRotation : MonoBehaviour
{
    private const float _minX = -60.0f;
    private const float _minY = -60.0f;
    private const float _maxX = 60.0f;
    private const float _maxY = 45.0f;

    private float _sensetivity = .2f;
    private float _xRot;
    private float _yRot;

    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerCamerasHolder _camerasHolder;
        
    private void OnEnable()
    {
        _input.Vector2Changed += Rotate;
    }

    private void OnDisable()
    {
        _input.Vector2Changed -= Rotate;
    }

    private void Rotate(Vector2 deltaDirection)
    {
        _xRot += deltaDirection.x * _sensetivity;
        _xRot = Math.Clamp(_xRot, _minX, _maxX);
        _yRot += deltaDirection.y * _sensetivity;
        _yRot = Math.Clamp(_yRot, _minY, _maxY);

        transform.rotation = Quaternion.Euler(0, _xRot, 0);
        _camerasHolder.transform.localRotation = Quaternion.Euler(0, 0, -_yRot);
    }
}
