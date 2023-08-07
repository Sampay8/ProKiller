using Cinemachine;
using System;
using System.Collections;
using UnityEngine;


public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    
    public event Action FollowCompleted;

    private const float _minX = -60.0f;
    private const float _minY = -75.0f;
    private const float _maxX = 60.0f;
    private const float _maxY = 75.0f;

    [SerializeField] private InputLogic _input;
    [SerializeField] private CinemachineVirtualCamera _aim;
    [SerializeField] private CinemachineVirtualCamera _back;
    [SerializeField] private CinemachineVirtualCamera _side;

    private CinemachineVirtualCamera _cur;
    private Weapon _weapon;
    private Transform _bulletTransform => _weapon.Bullet.transform;
    private Vector3 _sideStartPosition;
    private float _sensetivity = .2f;
    private float _xRot;
    private float _yRot;
        
    private void Awake()
    {
        _input = FindObjectOfType<InputLogic>();
        _cameraHolder.gameObject.SetActive(false);
    }

     
    private void Start()
    { 
        _sideStartPosition = _side.gameObject.transform.localPosition;
        Change(_aim);
    }

    private void OnEnable()
    {
        if (_weapon == null)
            _weapon = gameObject.GetComponentInChildren<Weapon>();

        EventBus.Current.Subscribe<LevelIsPausedSignal>(Disable);
        EventBus.Current.Subscribe<LevelIsPlayedSignal>(Enable);
        EventBus.Current.Subscribe<WeaponOnShoot>(EscortBullet);
        _input.Vector2Changed += Rotate;
    }

    private void Enable(LevelIsPlayedSignal signal) => _cameraHolder.gameObject.SetActive(true);
    private void Disable(LevelIsPausedSignal signal) => _cameraHolder.gameObject.SetActive(false);
    private void EscortBullet(WeaponOnShoot signal) => StartCoroutine(Escort(_bulletTransform));

    private void OnDisable()
    {
        EventBus.Current.Unsubscribe<LevelIsPausedSignal>(Disable);
        EventBus.Current.Unsubscribe<WeaponOnShoot>(EscortBullet);
        EventBus.Current.Unsubscribe<LevelIsPlayedSignal>(Enable);

        _input.Vector2Changed -= Rotate;
    }
      
    private void Change(CinemachineVirtualCamera next)
    {
        if (_cur != null)
            _cur.gameObject.SetActive(false);

        _cur = next;
        _cur.gameObject.SetActive(true);
    }

    private void Rotate(Vector2 deltaDirection)
    {
        _xRot += deltaDirection.x * _sensetivity;
        _xRot = Math.Clamp(_xRot, _minX, _maxX);
        _yRot += deltaDirection.y * _sensetivity;
        _yRot = Math.Clamp(_yRot, _minY, _maxY);

        transform.rotation = Quaternion.Euler(0, _xRot, -_yRot);
    }


    private IEnumerator Escort(Transform bullet)
    {
        _side.transform.position = _sideStartPosition;
        _side.LookAt = bullet;
        _side.Follow = bullet;
        Change(_side);
        CinemachineBrain.SoloCamera = _side;

        while (bullet.gameObject.activeInHierarchy == false)
            yield return null;

        while (bullet.gameObject.activeInHierarchy)
            yield return null;

        CinemachineBrain.SoloCamera = null;
        yield return new WaitForSeconds(2.5f);
        Change(_back);
        yield return new WaitForSeconds(2.5f);
        Change(_aim);
        EventBus.Current.Invoke(new EscortBulletComlette());
    }
}