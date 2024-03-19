using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof (Image))]
public class PauseBtn : MonoBehaviour
{
    [SerializeField] private GUISystem _gui;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Button _btn;

    private Image _curImage;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _gui = gameObject.GetComponentInParent<GUISystem>();
        if (_gui != null)
            Debug.LogWarning("Can't find GUI_System!");

        _btn ??= GetComponent<Button>();
        _curImage ??= GetComponent<Image>();
        _curImage.sprite ??= _pauseSprite;
    }

    private void OnEnable()
    {
        _gui.PausaChanged += ChangeSprite;
    }

    private void OnDisable()
    {
        _gui.PausaChanged -= ChangeSprite;
    }

    private void ChangeSprite(bool paused)
    { 
        if(paused)
            _curImage.sprite = _playSprite;
        else
            _curImage.sprite = _pauseSprite;
    }
}