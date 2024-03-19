using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    private LevelPresenter _levelPresenter;
    private AimSelector _aimPresenter;

    private bool _isWaiting = true;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(.1f);

    private void OnEnable()
    {
        Debug.LogError("presenter enabled");
        EventBus.Current.Subscrible<FollowCompletteSignal>(Change);
        StartCoroutine(Waiting());
    }

    private void Change(ISignal signal)
    {
        _isWaiting = false;
    }

    private IEnumerator Waiting()
    {
        while (_isWaiting)
        {
            yield return _delay;
        }
    }
}
