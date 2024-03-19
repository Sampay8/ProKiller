using Cinemachine;
using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public partial class PlayerCamerasHolder
{
    public class CameraFSM
    {
        private PlayerCamerasHolder _camerasHolder;
        private CameraState _curState;

        private CinemachineVirtualCamera _idle;
        private CinemachineVirtualCamera _back;
        private CinemachineVirtualCamera _side;
        private CinemachineVirtualCamera _stalker;

        private IdleCameraState _idleState;
        private VizorCameraState _vizorState;
        private SideCameraState _sideState;
        private BackCameraState _backState;
        private CameraState _stalkerState;

        public CameraFSM(PlayerCamerasHolder camerasHolder,  CinemachineVirtualCamera idle, CinemachineVirtualCamera back, CinemachineVirtualCamera side, CinemachineVirtualCamera stalker)
        {
            _camerasHolder = camerasHolder;
            _idle = idle;
            _side = side;
            _back = back;
            _stalker = stalker;

            _idleState = new IdleCameraState(this, _idle);
            _vizorState = new VizorCameraState(this, idle);
            _sideState = new SideCameraState(this, _side);
            _backState = new BackCameraState(this, _back);
            _stalkerState = new StalkerCameraState(this, _stalker);

            
            Timer.FixUpdated += Update;
            _camerasHolder.PressedI += SetIdle;
            _camerasHolder.PressedB += SetBack;
            _camerasHolder.PressedS += SetSide;
            _camerasHolder.PressedF += SetStalker;
            _camerasHolder.PressedV += SetVizor;
        }


        private void SetIdle() => ChangeState(_idleState);
        private void SetVizor() => ChangeState(_vizorState);
        private void SetSide() => ChangeState(_sideState);
        private void SetBack() => ChangeState(_backState);
        private void SetStalker() => ChangeState(_stalkerState);

        private void Update() => _curState?.Update();

        private void ChangeState(CameraState state)
        { 
            if(_curState != null)
                _curState.Exit();
            _curState = state;
            _curState.Enter();
        }
    }


    public abstract class CameraState
    {
        public abstract void Enter();

        public abstract void Update();

        public abstract void Exit();
    }

    public class IdleCameraState : CameraState
    {
        private CinemachineVirtualCamera _idleCamera { get; }
        protected CameraFSM CameraFSM { get; }

        public IdleCameraState(CameraFSM fSM, CinemachineVirtualCamera idleCamera )
        {
            CameraFSM = fSM;
            _idleCamera = idleCamera;
        }


        public override void Enter()
        {
            Debug.LogError("IdleCameraState is Enter");
        }

        public override void Update()
        {

            Debug.LogError("IdleCameraState is Update");
        }

        public override void Exit()
        {
            Debug.LogError("IdleCameraState is Exit");

        }
    }

    public class VizorCameraState : CameraState
    {

        private bool _isActive = false;
        private CinemachineVirtualCamera _idleCamera { get; }
        protected CameraFSM CameraFSM { get; }

        public VizorCameraState(CameraFSM fSM, CinemachineVirtualCamera idleCamera )
        {
            CameraFSM = fSM;
            _idleCamera = idleCamera;
        }


        public override void Enter()
        {
            _isActive = !_isActive;
            Debug.LogError("VizorCameraState is Enter " + "IsActive = " + _isActive.ToString());
        }

        public override void Update()
        {

            Debug.LogError("VizorCameraState is Update");
        }

        public override void Exit()
        {
            Debug.LogError("VizorCameraState is Exit");

        }
    }

    public class SideCameraState : CameraState
    {
        protected CameraFSM CameraFSM { get; }
        private CinemachineVirtualCamera _sideCamera { get; }

        public SideCameraState(CameraFSM fSM, CinemachineVirtualCamera sideCamera)
        {
            CameraFSM = fSM;
            _sideCamera = sideCamera;
        }


        public override void Enter()
        {
            Debug.LogError("SideCameraState is Enter");
            _sideCamera.gameObject.SetActive(true);
        }

        public override void Update()
        {

            Debug.LogError("SideCameraState is Update");
        }

        public override void Exit()
        {
            Debug.LogError("SideCameraState is Exit");
            _sideCamera.gameObject.SetActive(false);

        }
    }

      public class BackCameraState : CameraState
    {
        private readonly CinemachineVirtualCamera _backCamera;

        protected CameraFSM CameraFSM { get; }

        public BackCameraState(CameraFSM fSM, CinemachineVirtualCamera backCamera)
        {
            CameraFSM = fSM;
            _backCamera = backCamera;
        }


        public override void Enter()
        {
            Debug.LogError("BackCameraState is Enter");
            _backCamera.gameObject.SetActive(true);
        }

        public override void Update()
        {

            Debug.LogError("BackCameraState is Update");
        }

        public override void Exit()
        {
            Debug.LogError("BackCameraState is Exit");
            _backCamera.gameObject.SetActive(false);

        }
    }
    
      public class StalkerCameraState : CameraState
    {
        private readonly CinemachineVirtualCamera _stalkerCamera;

        protected CameraFSM CameraFSM { get; }

        public StalkerCameraState(CameraFSM fSM, CinemachineVirtualCamera stalkerCamera)
        {
            CameraFSM = fSM;
            _stalkerCamera = stalkerCamera;
        }


        public override void Enter()
        {
            Debug.LogError("StalkerCameraState is Enter");
            _stalkerCamera.gameObject.SetActive(true);
        }

        public override void Update()
        {

            Debug.LogError("StalkerCameraState is Update");
        }

        public override void Exit()
        {
            Debug.LogError("StalkerCameraState is Exit");
            _stalkerCamera.gameObject.SetActive(false);

        }
    }


}