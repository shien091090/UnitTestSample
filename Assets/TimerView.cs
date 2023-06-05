using System;
using UnityEngine;
using UnityEngine.UI;

public interface ITimerView
{
    void SetTimerDisplay(string timerText);
    void SetButtonText(string text);
}

public class TimerView : MonoBehaviour, ITimerView
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private TimerManager timerManager;
    
    public event Action<bool> OnChangeTimerState;

    private void Start()
    {
        timerManager = new TimerManager(this);
    }

    private void Update()
    {
        timerManager.CheckRefreshTimer(Time.deltaTime);
    }

    public void SetTimerDisplay(string timerText)
    {
        txt_timer.text = timerText;
    }

    public void SetButtonText(string text)
    {
        txt_buttonState.text = text;
    }

    public void OnClickButton()
    {
        timerManager.OnClickButton();
        switch (timerManager.CurrentTimerState)
        {
            case TimerState.Start:
            case TimerState.Stop:
                OnChangeTimerState?.Invoke(false);
                break;

            case TimerState.Play:
                OnChangeTimerState?.Invoke(true);
                break;
        }
    }
}