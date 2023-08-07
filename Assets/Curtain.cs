using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]


public class Curtain : MonoBehaviour
{
    private const float _delta = .01f;

    [SerializeField] private CanvasGroup _curtain;

    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(.03f);

    private void OnEnable()
    {
        EventBus.Current.Subscribe<ShowCurtainSignal>(Show);
        EventBus.Current.Subscribe<HideCurtainSignal>(Hide);

        if (_curtain == null)
            _curtain = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        EventBus.Current.Unsubscribe<ShowCurtainSignal>(Show);
        EventBus.Current.Unsubscribe<HideCurtainSignal>(Hide);
    }

    public void Show(ShowCurtainSignal signal) =>
        _curtain.alpha = 1.0f;

    public void Hide(HideCurtainSignal signal) =>
        StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha > 0)
            _curtain.alpha -= _delta;

        yield return _delay;
    }    
}
