using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Animator))]
public class CharacterRagDoll : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigitBodys;

    private Character _character;

    private void Awake()
    {
        if (_animator == null)
            Debug.LogError("Animator NOT assigned");

        _character ??= transform.GetComponent<Character>();
        _character.Killed += Death;


        foreach (var body in _rigitBodys)
            body.isKinematic = true;
    }

    private void Death(object character = null)
    {
        _animator.enabled = false;

        foreach (var body in _rigitBodys)
            body.isKinematic = false;

        _character.Killed -= Death;
    }
}
