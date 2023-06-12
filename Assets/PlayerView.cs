using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Image img_player;
    [SerializeField] private Color movableColor;
    [SerializeField] private Color unmovableColor;
    private TimerModel timerModel;

    private void Start()
    {
        timerModel.OnRefreshTimerState -= RefreshColor;
        timerModel.OnRefreshTimerState += RefreshColor;
        RefreshColor(timerModel.CurrentTimerState);
    }

    private void Update()
    {
        if (timerModel.CurrentTimerState != TimerState.Play)
            return;

        float horizontalAxis = Input.GetAxis("Horizontal");
        if (horizontalAxis != 0)
            transform.Translate(Vector3.right * horizontalAxis * Time.deltaTime * speed);

        float VerticalAxis = Input.GetAxis("Vertical");
        if (VerticalAxis != 0)
            transform.Translate(Vector3.up * VerticalAxis * Time.deltaTime * speed);
    }

    public void Inject(TimerModel timerModel)
    {
        this.timerModel = timerModel;
    }

    private void RefreshColor(TimerState timerState)
    {
        img_player.color = timerState == TimerState.Play ?
            movableColor :
            unmovableColor;
    }
}