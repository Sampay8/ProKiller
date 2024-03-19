using System;
using UnityEngine;
using UnityEngine.UI;

public class RequestDialog : Window
{
    public event Action<RequestResponse> ResponseReceived;

    [SerializeField] private Button _okBtn;
    [SerializeField] private Button _cancelBtn;

    private void OnEnable()
    {
        _okBtn.onClick.AddListener(PressOk);
        _cancelBtn.onClick.AddListener(PressCancel);
    }
    private void OnDisable()
    {
        _okBtn.onClick.RemoveAllListeners();
        _cancelBtn.onClick.RemoveAllListeners();
    }

    private void PressOk()
    {
        ResponseReceived?.Invoke(RequestResponse.OK);
        Hide();
    }
    private void PressCancel() 
    {
        ResponseReceived?.Invoke(RequestResponse.CANCEL);
        Hide();
    }
}

public enum RequestResponse
{ 
    OK,
    CANCEL
}