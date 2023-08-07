using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DontDestroy))]

public class GameRoot : MonoBehaviour
{
    private readonly EventBus _eventBus = new EventBus();
    public static Transform GUI;

    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GUIButtons _uIButtons;
    [SerializeField] private GameObject _playerTemplate;
    [SerializeField] private Player _player;

    public PlayerData PlayerData => _playerData;
    public States States => _states;

    private States _states;

    private void Awake()
    {
        GUI = gameObject.GetComponentInChildren<GUILogic>().transform;
        
        _player = CreatePlayer();

        _states = new States();

        //InitServices();
    }

    

    //private void InitServices()
    //{
    //    _ui.SceneLauched += LaunchLevel;


    //}


    private Player CreatePlayer()
    {
        if (_player == null)
        {
            _player = GameObject.FindObjectOfType<Player>();
            if (_player == null)
            {
                _player = Instantiate(_playerTemplate, Vector3.zero, Quaternion.identity).GetComponent<Player>();
                _player.gameObject.AddComponent<DontDestroy>();
            }
        }
        return _player;
    }
}

