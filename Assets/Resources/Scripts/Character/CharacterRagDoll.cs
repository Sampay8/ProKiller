using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterRagDoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigitBodys;

    private void Awake()
    {
        foreach (var body in _rigitBodys)
            body.isKinematic = true;
    }

    public void Death()
    {
        _animator.enabled = false;

        foreach (var body in _rigitBodys)
            body.isKinematic = false;
    }
}
