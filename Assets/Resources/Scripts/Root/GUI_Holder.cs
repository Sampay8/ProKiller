using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DontDestroy))]

public class GUI_Holder : MonoBehaviour
{
    public static Transform Transform { get; private set; }

    private void Awake()
    {
        Transform = transform;

    }
}       

