using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;

    private void Awake()
    {
        if(_cameraHolder == null)
        _cameraHolder = gameObject.GetComponentInChildren<Camera>().transform;

        EventBus.Current.Subscribe<LevelIsPlayedSignal>(Disable);
        EventBus.Current.Subscribe<LevelIsPausedSignal>(Enable);
    }
    private void OnDestroy()
    {
        EventBus.Current.Unsubscribe<LevelIsPlayedSignal>(Disable);
        EventBus.Current.Unsubscribe<LevelIsPausedSignal>(Enable);
    }

    private void Enable(LevelIsPausedSignal signal) => _cameraHolder.gameObject.SetActive(true);
    private void Disable(LevelIsPlayedSignal signal) => _cameraHolder.gameObject.SetActive(false);
}
