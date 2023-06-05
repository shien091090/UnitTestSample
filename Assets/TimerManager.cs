public class TimerManager
{
    private float timer;
    private readonly ITimerView timerView;
    public TimerState CurrentTimerState { get; private set; }

    public TimerManager(ITimerView view)
    {
        CurrentTimerState = TimerState.Start;
        timer = 0;
        timerView = view;
        RefreshButtonStateText();
        RefreshTimer(0);
    }

    public void RefreshTimer(float deltaTime)
    {
        timer += deltaTime;
        timerView.SetTimerText(timer.ToString("0.0"));
    }

    private void RefreshButtonStateText()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
                timerView.SetButtonStateText("O");
                break;

            case TimerState.Play:
                timerView.SetButtonStateText(">");
                break;

            case TimerState.Stop:
                timerView.SetButtonStateText("=");
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
                timerView.StartUpdateTimer();
                break;

            case TimerState.Play:
                CurrentTimerState = TimerState.Stop;
                timerView.StopUpdateTimer();
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