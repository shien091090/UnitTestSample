using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimerView timerView;

    private void Awake()
    {
        TimerManager timerManager = new TimerManager();
        timerView.Inject(timerManager);
    }
}