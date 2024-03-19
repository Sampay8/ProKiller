using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VizorSelector : MonoBehaviour
{
    [SerializeField] private WeaponUI _ui;
    [SerializeField] private Transform _vizorScope;
    [SerializeField] private Transform _aimVizor;

    private bool _isSelected;

    private void Awake()
    {
        _ui.VizorEnabled += OnChangeSelectedMode;
    }

    private void OnEnable()
    {
        AplySelect();       
    }

    private void OnDisable()
    {
        _vizorScope.gameObject.SetActive(false);
        _aimVizor.gameObject.SetActive(false);
    }

    private void OnChangeSelectedMode(bool isSelected)
    {
        _isSelected = isSelected;
        AplySelect();
    }

    private void AplySelect()
    {
        _vizorScope.gameObject.SetActive(_isSelected);
        _aimVizor.gameObject.SetActive(_isSelected);
    }

    private void OnDestroy()
    {
        _ui.VizorEnabled -= OnChangeSelectedMode;
    }
}
