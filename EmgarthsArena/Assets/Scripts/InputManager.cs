using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    private const bool keyboardControls = false;

    private string axisMoveHorizontal = "KeyboardMoveHorizontal";
    private string axisMoveVertical = "KeyboardMoveVertical";

    private string axisLookHorizontal = "MouseX";
    private string axisLookVertical = "MouseY";

    private string buttonJump = "XButton";

    private string buttonSpellCast1 = "JoyStickLeftBumper";
    private string buttonSpellCast2 = "JoyStickRightBumper";

    private string buttonElementChange1 = "JoyStickLeftTrigger";
    private string buttonElementChange2 = "JoyStickRightTrigger";

    public InputManager()
    {
        if (!keyboardControls)
        {
            //Initialize all the button layouts here.
            buttonSpellCast1 = "JoyStickLeftBumper";
            buttonSpellCast2 = "JoyStickRightBumper";
            axisMoveHorizontal = "LeftJoyStickHorizontal";
            axisMoveVertical = "LeftJoyStickVertical";
            axisLookHorizontal = "RightJoyStickHorizontal";
            axisLookVertical = "RightJoyStickVertical";
            buttonJump = "XButton";
        }
    }

    public float GetAxisMoveHorizontal()
    {
        return Input.GetAxis(axisMoveHorizontal);
    }
    public float GetAxisMoveVertical()
    {
        return Input.GetAxis(axisMoveVertical);
    }

    public float GetAxisLookHorizontal()
    {
        return Input.GetAxis(axisLookHorizontal);
    }
    public float GetAxisLookVertical()
    {
        return Input.GetAxis(axisLookVertical);
    }

    public bool GetButtonDownJump()
    {
        return Input.GetButtonDown(buttonJump);
        //return jumpButton;
    }
    public bool GetButtonJump()
    {
        return Input.GetButton(buttonJump);
    }

    public bool GetButtonDownSpellCast1()
    {
        return Input.GetButtonDown(buttonSpellCast1);
    }
    public bool GetButtonDownSpellCast2()
    {
        return Input.GetButtonDown(buttonSpellCast2);
    }

    public bool GetButtonDownElementChange1()
    {
        return Input.GetButtonDown(buttonElementChange1);
    }
    public bool GetButtonDownElementChange2()
    {
        return Input.GetButtonDown(buttonElementChange2);
    }
    public bool GetButtonUpElementChange1()
    {
        return Input.GetButtonUp(buttonElementChange1);
    }
    public bool GetButtonUpElementChange2()
    {
        return Input.GetButtonUp(buttonElementChange2);
    }
}
