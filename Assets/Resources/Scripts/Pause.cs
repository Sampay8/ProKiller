using System;
using UnityEngine;

public class Pause
{
    public event Action<bool> PausaModeChanged;
    
    public static Pause Signal;

    public Pause(GUISystem gUISystem)
    {
        if (Signal == null)
        {
            Signal = this;
            gUISystem.PausaChanged += OnRouteSignal;
        }
        else
        {
            Debug.LogError("Pausa : the class already exists and cannot be created twice");
        }
    }

    private void OnRouteSignal(bool pauseValue) => PausaModeChanged?.Invoke(pauseValue);
}