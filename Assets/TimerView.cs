using System;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    [SerializeField] private GameObject go_stopButton;
    private TimerModel timerModel;

    private void Start()
    {
        timerModel.Init();
    }

    private void Update()
    {
        timerModel.CheckUpdateTimer(Time.deltaTime);
    }

    public void Inject(TimerModel timerModel)
    {
        this.timerModel = timerModel;

        timerModel.OnRefreshTimerText -= SetTimerText;
        timerModel.OnRefreshTimerText += SetTimerText;

        timerModel.OnRefreshMainButtonStateText -= SetMainButtonStateText;
        timerModel.OnRefreshMainButtonStateText += SetMainButtonStateText;

        timerModel.OnRefreshStopButtonActive -= SetStopButtonActive;
        timerModel.OnRefreshStopButtonActive += SetStopButtonActive;
    }

    private void SetStopButtonActive(bool isActive)
    {
        go_stopButton.SetActive(isActive);
    }

    private void SetTimerText(string timerText)
    {
        txt_timer.text = timerText;
    }

    private void SetMainButtonStateText(string buttonStateText)
    {
        txt_buttonState.text = buttonStateText;
    }

    public void OnClickPauseButton()
    {
        timerModel.OnClickMainButton();
    }

    public void OnClickStopButton()
    {
        timerModel.OnClickStopButton();
    }
}