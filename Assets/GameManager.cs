using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimerView timerView;
    [SerializeField] private PlayerView playerView;

    private void Awake()
    {
        InputController inputController = new InputController();
        TimerModel timerModel = new TimerModel();
        PlayerModel playerModel = new PlayerModel(timerModel, inputController);
        
        timerView.Inject(timerModel);
        playerView.Inject(playerModel);
    }
}