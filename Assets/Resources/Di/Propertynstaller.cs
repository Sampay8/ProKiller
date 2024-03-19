using System;
using UnityEngine;
using Zenject;

public class Propertynstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private GUISystem _guiSystem;

    private readonly EventBus _eventBus = new EventBus();

    public override void InstallBindings()
    {
        BindGUI();
        BindPlayerData();
        //BindMenuCamera();
        //BindInput();
        BindTimer();
        //BindPlayer();
    }

    private void BindGUI() => Container.Bind<GUISystem>().FromComponentInHierarchy(_guiSystem).AsSingle();
    private void BindMenuCamera() => Container.Bind<MenuCamera>().AsSingle();
    private void BindPlayerData() => Container.Bind<PlayerData>().FromInstance(_playerData).AsSingle();
    private void BindTimer() => Container.Bind<Timer>().AsSingle();

}