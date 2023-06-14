using System;

public interface ITimerModel
{
    event Action<TimerState> OnRefreshTimerState;
    TimerState CurrentTimerState { get; }
}

public class TimerModel : ITimerModel
{
    public event Action<string> OnRefreshButtonStateText;
    public event Action<string> OnRefreshTimerText;
    public event Action<TimerState> OnRefreshTimerState;

    public TimerState CurrentTimerState { get; private set; }
    public float Timer { get; private set; }

    public TimerModel()
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
        OnRefreshTimerState?.Invoke(CurrentTimerState);
    }
}

public enum TimerState
{
    Start,
    Play,
    Stop
}