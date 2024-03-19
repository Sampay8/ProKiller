using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public static event Action FixUpdated;

    public event Action Ticked;
    public event Action TimerLaunched;
    public event Action TimeIsOver;

    private const byte _prepareTime = 3;
    private const byte _playTime = 10;

    [SerializeField] private GameObject _timePanel;
    [SerializeField] private TMP_Text  _timeValueText;

    private Coroutine _timerRoutine;
    private WaitForSecondsRealtime _delay = new WaitForSecondsRealtime(1.0f);
    private TimePresenter _timePresenter;
    private bool _isPlaying;
    private bool _isUsed;
    public byte CurTimeValue { get; private set; }

    #region Subscribles

    //private void OnEnable()
    //{
    //    EventBus.Current.Subscrible<BrifingIsFinishSignal>(Lauch);
    //    EventBus.Current.Subscrible<LevelIsPausedSignal>(Hide);
    //    EventBus.Current.Subscrible<LevelIsPlayedSignal>(Show);
    //    EventBus.Current.Subscrible<OnShootSignal>(PauseTime);
    //    EventBus.Current.Subscrible<FollowCompletteSignal>(PlayTime);

    //}

    //private void OnDestroy()
    //{
    //    EventBus.Current.Unsubscrible<BrifingIsFinishSignal>(Lauch);
    //    EventBus.Current.Unsubscrible<LevelIsPlayedSignal>(Show);
    //    EventBus.Current.Unsubscrible<LevelIsPausedSignal>(Hide);
    //    EventBus.Current.Unsubscrible<OnShootSignal>(PauseTime);
    //    EventBus.Current.Unsubscrible<FollowCompletteSignal>(PlayTime);

    //}
    #endregion

    private void PauseTime(OnShootSignal signal)
    {
        _isPlaying = false;
    }

    private void PlayTime(FollowCompletteSignal signal)
    {
        _isPlaying = true;
    }

    private void Show(LevelIsPlayedSignal signal)
    {
        _isPlaying = true;
        _timePanel.SetActive(_isPlaying);
    }

    private void Hide(LevelIsPausedSignal signal)
    {
        _isPlaying = false;
        _timePanel.SetActive(_isPlaying);
    }

    private void Start()
    {
        _timePanel.SetActive(false);
    }

    private void FixedUpdate() => FixUpdated?.Invoke();

    private void Lauch(BrifingIsFinishSignal signal)
    {
        _isUsed = true;
        _isPlaying = true;
        CurTimeValue = _prepareTime;
        _timePresenter ??= new TimePresenter(this, _timeValueText);
        _timePanel.SetActive(true);

        if (_timerRoutine != null)
            StopCoroutine(_timerRoutine);
        _timerRoutine = StartCoroutine(Ticker());
    }

    

    private void ChekTime(ref bool prepareTime,ref bool playTime) 
    {
        if (prepareTime)
        {
            prepareTime = CurTimeValue > 0;
            if (prepareTime == false)
            { 
                playTime = true;
                TimerLaunched?.Invoke();
                CurTimeValue = _playTime;
                EventBus.Current.Invoke(new TimeStartSignal());
            }
        }

        if (playTime)
        {
            playTime = CurTimeValue > 0;
            if (playTime == false)
                _isUsed = false;            
        }
    }

    private IEnumerator Ticker()
    {
        bool prepareTime = true;
        bool playTime = false;

        CurTimeValue = _prepareTime;
        Ticked.Invoke();
        yield return _delay;

        while (_isUsed)
        {
            if (_isPlaying && CurTimeValue >0)
            {
                CurTimeValue -= 1;
                Ticked.Invoke();
            }
                yield return _delay;
            ChekTime(ref prepareTime,ref playTime);
        }
        TimeIsOver?.Invoke();
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
        if (str.Length > 1)
            _text.text = str;
        else
            _text.text = "0" + str;
    }
}