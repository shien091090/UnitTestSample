using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimerView timerView;
    [SerializeField] private PlayerView playerView;

    private void Awake()
    {
        TimerModel timerModel = new TimerModel();
        timerView.Inject(timerModel);
        playerView.Inject(timerModel);
    }
}