using System;
using UnityEngine;

public class PlayerModel
{
    private readonly ITimerModel timerModel;
    private readonly IInputController inputController;

    public event Action OnRefreshUnmovableColor;
    public event Action OnRefreshMovableColor;
    public event Action<Vector3> OnUpdateMoving;

    public PlayerModel(ITimerModel timerModel, IInputController inputController)
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
        Vector3 horizontalMoveVector = Vector3.zero;
        if (horizontalAxis != 0)
            horizontalMoveVector = Vector3.right * horizontalAxis * deltaTime * speed;

        float VerticalAxis = inputController.GetAxis("Vertical");
        Vector3 verticalMoveVector = Vector3.zero;
        if (VerticalAxis != 0)
            verticalMoveVector = Vector3.up * VerticalAxis * deltaTime * speed;

        OnUpdateMoving?.Invoke(horizontalMoveVector + verticalMoveVector);
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