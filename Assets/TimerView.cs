using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private TimerManager timerManager;

    private void Update()
    {
        timerManager.CheckUpdateTimer(Time.deltaTime);
    }

    public void Inject(TimerManager timerManager)
    {
        this.timerManager = timerManager;

        timerManager.OnRefreshTimerText -= SetTimerText;
        timerManager.OnRefreshTimerText += SetTimerText;

        timerManager.OnRefreshButtonStateText -= SetButtonStateText;
        timerManager.OnRefreshButtonStateText += SetButtonStateText;
    }

    private void SetTimerText(string timerText)
    {
        txt_timer.text = timerText;
    }

    private void SetButtonStateText(string buttonStateText)
    {
        txt_buttonState.text = buttonStateText;
    }

    public void OnClickButton()
    {
        timerManager.OnClickButton();
    }
}