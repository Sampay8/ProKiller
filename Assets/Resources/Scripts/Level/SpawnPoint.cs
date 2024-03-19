using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _playerPoint;
    [SerializeField] private List<Transform> _characterPoints; 

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        var _player = FindObjectOfType<Player>();
        _player.transform.position = this.transform.position;

        PlayerCamerasHolder playerCamera = _player.GetComponentInChildren<PlayerCamerasHolder>();
        playerCamera.transform.rotation = this.transform.rotation;
    }
}
