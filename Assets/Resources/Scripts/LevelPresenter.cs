using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class LevelPresenter : MonoBehaviour
{


    private void Start()
    {
        WindowManager.ShowWindow<BrifingDialog>();
    }

}