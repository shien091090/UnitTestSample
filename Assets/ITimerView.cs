public interface ITimerView
{
    void StopUpdateTimer();
    void StartUpdateTimer();
    void SetTimerText(string timerText);
    void SetButtonStateText(string buttonStateText);
}