using UnityEngine;

public class CharacterAnimator
{
    private const float _slowSpeed = .1f;
    private const float _normalSpeed = 1.0f;

    private Animator _animator;
    private Character _character;

    public CharacterAnimator(Character character, Animator animator)
    {
        _character = character;
        _animator = animator;

        _character.MovmentFreezing += SetFreeze;
        _character.Walking += OnWalk;
        _character.Standing += OnStanding;
        _character.MotionChanged += ChengeMotion;
    }

    private void OnStanding() =>
        _animator.Play(Motion.Idle.ToString());

    private void OnWalk() =>
        _animator.Play(Motion.WalkFwdLoop.ToString());

    private void SetFreeze(bool value) => 
        SetSlowMotion(value);

    private void OnDestroy()
    {
        _character.MovmentFreezing -= SetFreeze;
        _character.Walking -= OnWalk;
        _character.Standing -= OnStanding;
    }

    private void ChengeMotion(Motion motion)
    {
        switch (motion)
        {
            case Motion.Idle:
                OnStanding();
                break;
            case Motion.WalkFwdLoop:
                OnWalk();
                break;
        }
    }

    private void SetSlowMotion(bool value)
    {
        if (value)
            _animator.speed = _slowSpeed;
        else
            _animator.speed = _normalSpeed;
    }
}

