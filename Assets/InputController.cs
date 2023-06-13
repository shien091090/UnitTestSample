using UnityEngine;

public class InputController : IInputController
{
    public InputController()
    {
    }

    public float GetAxis(string axisName)
    {
        return Input.GetAxis(axisName);
    }
}