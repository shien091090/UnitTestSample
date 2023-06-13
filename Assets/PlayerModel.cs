using System;
using UnityEngine;

public class PlayerModel
{
    private readonly TimerModel timerModel;
    private readonly IInputController inputController;

    public event Action OnRefreshUnmovableColor;
    public event Action OnRefreshMovableColor;
    public event Action<Vector3> OnUpdateMoving;

    public PlayerModel(TimerModel timerModel, IInputController inputController)
    {
        this.timerModel = timerModel;
        this.inputController = inputController;

        timerModel.OnRefreshTimerState -= RefreshColor;
        timerModel.OnRefreshTimerState += RefreshColor;
    }

    public void CheckUpdateMoving(float deltaTime, float speed)
    {
        if (timerModel.CurrentTimerState != TimerState.Play)
            return;

        float horizontalAxis = inputController.GetAxis("Horizontal");
        if (horizontalAxis != 0)
        {
            Vector3 horizontalMoveVector = Vector3.right * horizontalAxis * deltaTime * speed;
            OnUpdateMoving?.Invoke(horizontalMoveVector);
        }

        float VerticalAxis = inputController.GetAxis("Vertical");
        if (VerticalAxis != 0)
        {
            Vector3 verticalMoveVector = Vector3.up * VerticalAxis * deltaTime * speed;
            OnUpdateMoving?.Invoke(verticalMoveVector);
        }
    }

    public void Init()
    {
        RefreshColor(timerModel.CurrentTimerState);
    }

    private void RefreshColor(TimerState timerState)
    {
        if (timerState == TimerState.Play)
            OnRefreshMovableColor?.Invoke();
        else
            OnRefreshUnmovableColor?.Invoke();
    }
}