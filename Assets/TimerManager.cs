using System;

public class TimerManager
{
    public event Action<string> OnRefreshButtonStateText;
    public event Action<string> OnRefreshTimerText;
    
    public TimerState CurrentTimerState { get; private set; }
    public float Timer { get; private set; }

    public TimerManager()
    {
        CurrentTimerState = TimerState.Start;
        RefreshButtonStateText();
    }

    public void CheckUpdateTimer(float deltaTime)
    {
        if (CurrentTimerState != TimerState.Play)
            return;

        RefreshTimer(deltaTime);
    }

    private void RefreshTimer(float deltaTime)
    {
        Timer += deltaTime;
        OnRefreshTimerText?.Invoke(Timer.ToString("0.0"));
    }

    private void RefreshButtonStateText()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
                OnRefreshButtonStateText?.Invoke("0");
                break;

            case TimerState.Play:
                OnRefreshButtonStateText?.Invoke(">");
                break;

            case TimerState.Stop:
                OnRefreshButtonStateText?.Invoke("=");
                break;
        }
    }

    public void OnClickButton()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
            case TimerState.Stop:
                CurrentTimerState = TimerState.Play;
                break;

            case TimerState.Play:
                CurrentTimerState = TimerState.Stop;
                break;
        }

        RefreshButtonStateText();
    }
}

public enum TimerState
{
    Start,
    Play,
    Stop
}