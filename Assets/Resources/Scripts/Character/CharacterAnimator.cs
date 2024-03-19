using UnityEngine;

public class CharacterAnimator
{
    private const float _slowSpeed = .01f;
    private const float _defoultSpeed = 1.0f;

    private Animator _animator;
    private Character _character;

    public CharacterAnimator(Character character, Animator animator)
    {
        _character = character;
        _animator = animator;

        _character.MovmentFreezing += SetFreeze;
        _character.MotionChanged += ChangeMotionSpeed;
        _character.MotionChanged += SetMotion;
    }

    private void SetFreeze(bool value) =>
        SetSlowMotion(value);

    private void OnDestroy()
    {
        _character.MovmentFreezing -= SetFreeze;
    }

    public void ChangeMotionSpeed(Motion motion)
    {
        switch (motion)
        {
            case Motion.Idle:
                _animator.speed = _slowSpeed;
                break;
            case Motion.WalkFwdLoop:
                _animator.speed = _defoultSpeed;
                break;
        }
    }

    private void SetSlowMotion(bool value)
    {
        if (value)
            _animator.speed = _slowSpeed;
        else
            _animator.speed = _defoultSpeed;
    }

    private void SetMotion(Motion motion) 
    {
        if(_character.IsAlive)
            _animator.Play(motion.ToString());
    }
}