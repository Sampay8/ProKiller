using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialog : Window
{
    [SerializeField] private Button _closeButton;

    private new void Hide()
    { 
        _closeButton.onClick.RemoveAllListeners();

        base.Hide();
    }

    private void Start()
    {
        _closeButton.onClick.AddListener(Hide);   
    }
}
