using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPresenter : MonoBehaviour
{
    [SerializeField] private GameRoot _root;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerCamera _playerCamera;

    public event Action<bool> PlayerShooting;

    private bool _isCanShoot = true;
    public bool IsCanShoot => _isCanShoot;

    private Bullet _bullet;

    //private void Awake()
    //{
    //    Init();
    //}

    //private void Init()
    //{
    //    _root = FindObjectOfType<GameRoot>();
    //    _player = InitPlayer();
    //}

    //private Player InitPlayer()
    //{
    //    Player player = GameObject.FindObjectOfType<Player>();
    //    _playerCamera = _player.GetComponentInChildren<PlayerCamera>();
    //    player.Init(this,out _bullet);
    //    return player;
    //}

    //private void OnEnable()
    //{
    //    _player.Shooting += OnShootMode;
    //    _playerCamera.FollowCompleted += OnCameraCoplete;
    //}

    //private void OnDisable()
    //{
    //    _player.Shooting -= OnShootMode;
    //    _playerCamera.FollowCompleted -= OnCameraCoplete;
    //}

    //private void OnShootMode()
    //{
    //    _isCanShoot = false;
    //    _playerCamera.EscortBullet(_bullet.transform);
    //    PlayerShooting?.Invoke(IsCanShoot == true);
    //}


    //private void OnCameraCoplete()
    //{
    //    _isCanShoot = true;
    //    PlayerShooting?.Invoke(IsCanShoot == true);
    //}

}
