using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public static Transform Transform => Transform;
    public event Action Shooting;
    public event Action Triggered;
     
    //private const float _speed = 15f;
    [SerializeField] private float _speed = 15.0f;
    [SerializeField] private float _damage = 52.0f;

    private Rigidbody _rB;

    private Vector3 _targetPoint = Vector3.zero;

    public float Damage => _damage;

    private void Awake()
    {
        if (_rB == null)
            _rB = transform.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Pause.Signal.PausaModeChanged += SetFreeze;
        DoShot();
    }

    private void SetFreeze(bool isPausa)
    {
        if (gameObject.activeInHierarchy)
        { 
            if(isPausa)
                _rB.velocity = Vector3.zero;
            else
                _rB.velocity = transform.forward * _speed;
        }
    }

    private void OnDisable()
    {
        Pause.Signal.PausaModeChanged -= SetFreeze;
    }

    private void DoShot( )
    {
        if (_targetPoint == Vector3.zero)
        { 
            gameObject.SetActive(false);
            return;
        }

        transform.LookAt(_targetPoint);
        _rB.velocity = transform.forward * _speed;
        Shooting?.Invoke();
    }
            

    private void OnTriggerEnter(Collider col)
    {
        Triggered?.Invoke();
        Character character = col.transform.GetComponentInParent<Character>();

        if (character == null)
        { 
            _rB.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            return;
        }
        
        if(character.IsAlive)
            character.GetDamage(col, this);

    }

    internal void SetTarget(Vector3 targetPoint) => _targetPoint = targetPoint;
}
