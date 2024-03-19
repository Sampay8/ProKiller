using UnityEngine;
using Zenject;

[RequireComponent(typeof(DontDestroy))]

public class Player : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private PlayerCamerasHolder _playerCamerasHolder;

    private Transform _cameraTransform => _playerCamerasHolder.transform;
    private PlayerData _data;
    private Player_view _view;
    private Camera _camera;

    [Inject]
    private void Construct(PlayerData data)
    {
        _data = data;
    }

    private void Awake()
    {
        // Init();
    }

    private void Start()
    {
        _cameraTransform.localPosition += Vector3.up * 1.6f;

    }
}