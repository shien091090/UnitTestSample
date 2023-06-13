using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Image img_player;
    [SerializeField] private Color movableColor;
    [SerializeField] private Color unmovableColor;
    private PlayerModel playerModel;

    private void Start()
    {
        playerModel.Init();
    }

    private void Update()
    {
        playerModel.CheckUpdateMoving(Time.deltaTime, speed);
    }

    public void Inject(PlayerModel playerModel)
    {
        this.playerModel = playerModel;

        playerModel.OnUpdateMoving -= Move;
        playerModel.OnUpdateMoving += Move;

        playerModel.OnRefreshMovableColor -= SetMovableColor;
        playerModel.OnRefreshMovableColor += SetMovableColor;

        playerModel.OnRefreshUnmovableColor -= SetUnmovableColor;
        playerModel.OnRefreshUnmovableColor += SetUnmovableColor;
    }

    private void SetMovableColor()
    {
        img_player.color = movableColor;
    }

    private void SetUnmovableColor()
    {
        img_player.color = unmovableColor;
    }

    private void Move(Vector3 moveVector)
    {
        transform.Translate(moveVector);
    }
}