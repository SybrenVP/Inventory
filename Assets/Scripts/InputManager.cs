using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void Axis(float value);
    
    //Quick implementation of the required inputs
    public GameEvent ButtonUp_Clear;
    public GameEvent ButtonUp_PickUp;

    public GameEvent ButtonUp_ResUp;
    public GameEvent ButtonUp_ResDown;

    public Axis X_Axis;
    public Axis Y_Axis;

    protected float _staleHorizontal = 0f;
    protected bool _waitingForNoInput_Horizontal = false;
    protected bool _waitingForNoInput_Vertical = false;
    protected float _staleVertical = 0f;

    protected void Update()
    {
        if (Input.GetButtonUp("Clear"))
        {
            ButtonUp_Clear.Raise();
        }

        if (Input.GetButtonUp("PickUp"))
        {
            ButtonUp_PickUp.Raise();
        }

        if (Input.GetButtonUp("ResolutionUp"))
        {
            ButtonUp_ResUp.Raise();
        }

        if (Input.GetButtonUp("ResolutionDown"))
        {
            ButtonUp_ResDown.Raise();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(_staleHorizontal - horizontal) > Mathf.Epsilon && !_waitingForNoInput_Horizontal)
        {
            X_Axis?.Invoke(horizontal);
            _waitingForNoInput_Horizontal = true;
        }
        else if (_waitingForNoInput_Horizontal && Mathf.Abs(horizontal) < Mathf.Epsilon)
        {
            _waitingForNoInput_Horizontal = false;
        }

        if (Mathf.Abs(_staleVertical - vertical) > Mathf.Epsilon && !_waitingForNoInput_Vertical)
        {
            Y_Axis?.Invoke(vertical);
            _waitingForNoInput_Vertical = true;
        }
        else if (_waitingForNoInput_Vertical && Mathf.Abs(vertical) < Mathf.Epsilon)
        {
            _waitingForNoInput_Vertical = false;
        }
    }
}
