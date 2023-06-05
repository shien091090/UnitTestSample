using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour, ITimerView
{
    [SerializeField] private Text txt_buttonState;
    [SerializeField] private Text txt_timer;
    private Coroutine updateCoroutine;
    private TimerManager timerManager;

    public void SetTimerText(string timerText)
    {
        txt_timer.text = timerText;
    }

    public void SetButtonStateText(string buttonStateText)
    {
        txt_buttonState.text = buttonStateText;
    }

    public void StopUpdateTimer()
    {
        if (updateCoroutine != null)
            StopCoroutine(updateCoroutine);
    }

    public void StartUpdateTimer()
    {
        updateCoroutine = StartCoroutine(Cor_UpdateTimer());
    }

    private void Start()
    {
        timerManager = new TimerManager(this);
    }

    private IEnumerator Cor_UpdateTimer()
    {
        while (timerManager.CurrentTimerState == TimerState.Play)
        {
            yield return new WaitForEndOfFrame();
            timerManager.RefreshTimer(Time.deltaTime);
        }
    }
}