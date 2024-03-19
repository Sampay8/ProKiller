using System;
using System.Diagnostics;

namespace CharacterFSM
{
    public class CharacterSM
    {
        public readonly CharacterSM SM;

        private bool _isScared = false;

        private Character _character;
        private CharacterAnimator _animator;
        private CharacterMover _mover;
        private CharacterState _curState;
        public readonly Idle Idle;
        public readonly Walk Walk;
        public readonly Fear Fear;


        public CharacterSM(Character character, CharacterAnimator animator, CharacterMover mover)
        {
            _character = character;
            _animator = animator;
            _mover = mover;
            _character.Scared += Scared;

            SM = this;
            Idle = new Idle(this,_character, _animator, _mover);
            Walk = new Walk(this, _character, _animator, _mover);
            Fear = new Fear(this, _character, _animator, _mover);



            SetState(Idle);
        }

        private void Scared()
        {
            if (_isScared == false)
                SetState(Fear);

            _isScared = true;

        }

        public void SetState(CharacterState state)
        {
            if (_curState != null)
                _curState.Exit();

            _curState = state;
            _curState.Enter();
        }

        public virtual void Update()
        {
            if (_curState != null)
                _curState.Update();
        }

    }

    public abstract class CharacterState
    {
        protected CharacterSM SM;
        protected Character Character;
        protected CharacterAnimator Animator;
        protected CharacterMover Mover;

        public CharacterState(CharacterSM sm, Character character, CharacterAnimator animator, CharacterMover mover)
        {
            SM = sm;
            Character = character;
            Animator = animator;
            Mover = mover;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }

    public class Idle : CharacterState
    {
        public Idle(CharacterSM sm, Character character, CharacterAnimator animator, CharacterMover mover) : base(sm, character, animator, mover)
        {
        }

        public override void Enter()
        {   
            //Mover.SetMoveTarget( Character.GetWay());
            //Animator.ChangeMotion(Motion.Idle);
        }

        public override void Update()
        {
            if (Character.IsMove == true)
                SM.SetState(SM.Walk);
        }

        public override void Exit()
        {
            
        }
    }

    public class Walk : CharacterState
    {
        public Walk(CharacterSM sm, Character character, CharacterAnimator animator, CharacterMover mover) : base(sm, character, animator, mover)
        {
        }

        public override void Enter()
        {
            Character.ChangeMotion(Motion.WalkFwdLoop);
            Mover.SetMoveTarget( Character.GetWay());
        }

        public override void Update()
        {
            Console.WriteLine("Walk State Is Update");
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
    public class Run : CharacterState
    {
        public Run(CharacterSM sm, Character character, CharacterAnimator animator, CharacterMover mover) : base(sm, character, animator, mover)
        {
        }

        public override void Enter()
        {
            throw new NotImplementedException();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            // Display message that Run state is updating
            System.Console.WriteLine("Run state is updating");
        }
    }
    public class Fear : CharacterState
    {
        private const float _fearDuration = 5f;
                
        private float _fearTimer;
        private float _delta = .1f;
        private bool _isFearActive;

        public Fear(CharacterSM sm, Character character, CharacterAnimator animator, CharacterMover mover) : base(sm, character, animator, mover)
        {
        }

        public override void Enter()
        {
            _fearTimer = _fearDuration;
            _isFearActive = true;
            Character.ChangeMotion(Motion.Scared);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            _isFearActive =  _fearTimer >= 0;
            if (_isFearActive == false)
                SM.SetState(SM.Idle);

            if(_isFearActive)
                _fearTimer -= _delta;

        }
    }

}
