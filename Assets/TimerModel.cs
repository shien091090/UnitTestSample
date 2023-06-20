using System;

public interface ITimerModel
{
    event Action<TimerState> OnRefreshTimerState;
    TimerState CurrentTimerState { get; }
}

public class TimerModel : ITimerModel
{
    public TimerState CurrentTimerState { get; private set; }
    public event Action<string> OnRefreshMainButtonStateText;
    public event Action<string> OnRefreshTimerText;
    public event Action<bool> OnRefreshStopButtonActive;
    public float Timer { get; private set; }

    public void Init()
    {
        CurrentTimerState = TimerState.Start;
        RefreshMainButtonStateText();
        RefreshSubButtonState();
    }

    public event Action<TimerState> OnRefreshTimerState;

    public void CheckUpdateTimer(float deltaTime)
    {
        if (CurrentTimerState != TimerState.Play)
            return;

        RefreshTimer(deltaTime);
    }

    private void RefreshSubButtonState()
    {
        OnRefreshStopButtonActive?.Invoke(CurrentTimerState != TimerState.Start);
    }

    private void RefreshTimer(float deltaTime)
    {
        Timer += deltaTime;
        SendRefreshTimerEvent();
    }

    private void RefreshMainButtonStateText()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
                OnRefreshMainButtonStateText?.Invoke("O");
                break;

            case TimerState.Play:
                OnRefreshMainButtonStateText?.Invoke(">");
                break;

            case TimerState.Pause:
            case TimerState.Stop:
                OnRefreshMainButtonStateText?.Invoke("=");
                break;
        }
    }

    private void SendRefreshTimerEvent()
    {
        OnRefreshTimerText?.Invoke(Timer.ToString("0.0"));
    }

    public void OnClickMainButton()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
            case TimerState.Stop:
            case TimerState.Pause:
                CurrentTimerState = TimerState.Play;
                break;

            case TimerState.Play:
                CurrentTimerState = TimerState.Pause;
                break;
        }

        RefreshMainButtonStateText();
        RefreshSubButtonState();
        OnRefreshTimerState?.Invoke(CurrentTimerState);
    }

    public void OnClickStopButton()
    {
        CurrentTimerState = TimerState.Stop;
        Timer = 0;
        RefreshMainButtonStateText();
        SendRefreshTimerEvent();
        OnRefreshTimerState?.Invoke(CurrentTimerState);
    }
}

public enum TimerState
{
    Start,
    Play,
    Stop,
    Pause
}