using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]


public class Curtain : MonoBehaviour
{
    private const float _delta = .05f;

    [SerializeField] private CanvasGroup _curtain;

    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(.01f);

    private void OnEnable()
    {
        _curtain ??= GetComponent<CanvasGroup>();
    }

    public void Show() => _curtain.alpha = 1.0f;

    public void Hide() => StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha > 0)
        { 
            _curtain.alpha -= _delta;
            yield return _delay;
        }
        yield return null;
    }    
}
