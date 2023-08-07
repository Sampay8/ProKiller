using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private const float _speed = 15f;

    private Rigidbody _rB;

    private void Awake()
    {
        if (_rB == null)
            _rB = transform.GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        EventBus.Current.Invoke<WeaponCompletteShoot>(new WeaponCompletteShoot());
    }

    internal void DoShot(Vector3 target)
    {
        gameObject.SetActive(true);
        transform.LookAt(target);
        _rB.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.TryGetComponent<Character>(out Character human);
        if (human)
        {
            human.GetDamage(collision, transform);
        }

        else
        { 
            _rB.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
