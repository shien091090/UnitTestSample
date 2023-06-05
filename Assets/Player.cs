using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Color movableColor;
    [SerializeField] private Color notMovableColor;
    [SerializeField] private TimerView timerView;
    
    private bool isMovable;
    private Image imageComponent;

    private Image GetImanage
    {
        get
        {
            if (imageComponent == null)
                imageComponent = GetComponent<Image>();

            return imageComponent;
        }
    }

    private void Start()
    {
        timerView.OnChangeTimerState -= SetMovable;
        timerView.OnChangeTimerState += SetMovable;
        SetMovable(false);
    }

    private void Update()
    {
        if (isMovable == false)
            return;

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis != 0)
            transform.Translate(Vector3.right * Time.deltaTime * horizontalAxis * speed);

        if (verticalAxis != 0)
            transform.Translate(Vector3.up * Time.deltaTime * verticalAxis * speed);
    }

    private void SetMovable(bool isMovable)
    {
        this.isMovable = isMovable;

        SetColor(isMovable ?
            movableColor :
            notMovableColor);
    }

    private void SetColor(Color color)
    {
        GetImanage.color = color;
    }
}