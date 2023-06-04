using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private TimerState currentTimerState = TimerState.Start;
    private float timer = 0;

    private void Start()
    {
        RefreshButtonStateText();
    }

    private void Update()
    {
        if (currentTimerState != TimerState.Play)
            return;

        RefreshTimer();
    }

    private void RefreshTimer()
    {
        timer += Time.deltaTime;
        txt_timer.text = timer.ToString("0.0");
    }

    private void RefreshButtonStateText()
    {
        switch (currentTimerState)
        {
            case TimerState.Start:
                txt_buttonState.text = "O";
                break;

            case TimerState.Play:
                txt_buttonState.text = ">";
                break;

            case TimerState.Stop:
                txt_buttonState.text = "=";
                break;
        }
    }

    public void OnClickButton()
    {
        switch (currentTimerState)
        {
            case TimerState.Start:
            case TimerState.Stop:
                currentTimerState = TimerState.Play;
                break;

            case TimerState.Play:
                currentTimerState = TimerState.Stop;
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