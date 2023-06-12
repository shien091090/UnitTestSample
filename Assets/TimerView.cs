using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private TimerModel timerModel;

    private void Update()
    {
        timerModel.CheckUpdateTimer(Time.deltaTime);
    }

    public void Inject(TimerModel timerModel)
    {
        this.timerModel = timerModel;

        timerModel.OnRefreshTimerText -= SetTimerText;
        timerModel.OnRefreshTimerText += SetTimerText;

        timerModel.OnRefreshButtonStateText -= SetButtonStateText;
        timerModel.OnRefreshButtonStateText += SetButtonStateText;
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
        timerModel.OnClickButton();
    }
}