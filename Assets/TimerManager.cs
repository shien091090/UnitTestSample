using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private TimerState currentTimerState = TimerState.Start;
    private float timer = 0;
    private Coroutine updateCoroutine;

    private void Start()
    {
        RefreshButtonStateText();
    }

    private IEnumerator Cor_UpdateTimer()
    {
        while (currentTimerState == TimerState.Play)
        {
            yield return new WaitForEndOfFrame();
            RefreshTimer();
        }
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
                updateCoroutine = StartCoroutine(Cor_UpdateTimer());
                break;

            case TimerState.Play:
                currentTimerState = TimerState.Stop;
                if (updateCoroutine != null)
                    StopCoroutine(updateCoroutine);
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