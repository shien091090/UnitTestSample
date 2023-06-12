using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimerView timerView;

    private void Awake()
    {
        TimerModel timerModel = new TimerModel();
        timerView.Inject(timerModel);
    }
}