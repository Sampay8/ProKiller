using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public event Action TimerLaunched;
    public event Action Ticked;

    private const byte _timeToStart = 10;
    private const byte _timeValue = 30;

    [SerializeField] private GameObject _uiPanel;
    [SerializeField] private TMP_Text  _timeValueText;

    private Coroutine _timerRoutine;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(1.0f);

    public byte CurTimeValue { get; private set; }
    
    private void OnEnable()
    {
        CurTimeValue = _timeToStart;

        EventBus.Current.Subscribe<TimeStartSignal>(Lauch);
    }

    private void Start()
    {
        TimePresenter timePresenter = new TimePresenter(this, _timeValueText);
        _timerRoutine = StartCoroutine(Ticker());//
    }

    private void Lauch(TimeStartSignal signal)
    {
        _timerRoutine = StartCoroutine(Ticker());
    }

    private void OnDestroy()
    {
        EventBus.Current.Unsubscribe<TimeStartSignal>(Lauch);
    }

    private IEnumerator Ticker()
    {
        while (CurTimeValue > 0)
        {
            yield return _delay;
            CurTimeValue -= 1;
            Ticked?.Invoke();
        }
        CurTimeValue = _timeValue;
        TimerLaunched?.Invoke();

        while (CurTimeValue > 0)
        {
            yield return _delay;
            CurTimeValue -= 1;
            Ticked?.Invoke();
        }

        yield return null;
    }
}

public class TimePresenter
{
    private TMP_Text _text;
    private Timer _timer;

    public TimePresenter(Timer timer, TMP_Text text)
    {
        _timer = timer;
        _text = text;
        _timer.Ticked += UpdateText;
    }

    private void UpdateText()
    {
        string str = _timer.CurTimeValue.ToString();
        if (_timer.CurTimeValue >= 10)
            _text.text = str;
        else
            _text.text = "0" + str;
    }
}
