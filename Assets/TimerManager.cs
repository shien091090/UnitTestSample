public class TimerManager
{
    private ITimerView timerView;

    public float Timer { get; private set; }
    public TimerState CurrentTimerState { get; private set; }

    public TimerManager(ITimerView view)
    {
        Timer = 0;
        CurrentTimerState = TimerState.Start;
        timerView = view;
        RefreshButtonStateText();
        timerView.SetTimerDisplay(ConvertTimerText(Timer));
    }

    public void CheckRefreshTimer(float deltaTime)
    {
        if (CurrentTimerState != TimerState.Play)
            return;

        Timer += deltaTime;
        timerView.SetTimerDisplay(ConvertTimerText(Timer));
    }

    private string ConvertTimerText(float timer)
    {
        return timer.ToString("0.0");
    }

    private void RefreshButtonStateText()
    {
        switch (CurrentTimerState)
        {
            case TimerState.Start:
                timerView.SetButtonText("O");
                break;

            case TimerState.Play:
                timerView.SetButtonText(">");
                break;

            case TimerState.Stop:
                timerView.SetButtonText("=");
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