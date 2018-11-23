using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    private const bool keyboardControls = false;

    //private string axisMoveHorizontal = "KeyboardMoveHorizontal";
    //private string axisMoveVertical = "KeyboardMoveVertical";

    //private string axisLookHorizontal = "MouseX";
    //private string axisLookVertical = "MouseY";

    //private string buttonJump = "XButton";

    //private string buttonSpellCast1 = "JoyStickLeftBumper";
    //private string buttonSpellCast2 = "JoyStickRightBumper";

    //private string buttonElementChange1 = "JoyStickLeftTrigger";
    //private string buttonElementChange2 = "JoyStickRightTrigger";

    Dictionary<Glob.Keytype, string> inputDictionary;


    public InputManager(int pPlayerNumber)
    {
        inputDictionary = Glob.GetInputDictionary(pPlayerNumber);
        if (!keyboardControls)
        {
            //Initialize all the button layouts here.
            //buttonSpellCast1 = "JoyStickLeftBumper";
            //buttonSpellCast2 = "JoyStickRightBumper";
            //axisMoveHorizontal = "LeftJoyStickHorizontal";
            //axisMoveVertical = "LeftJoyStickVertical";
            //axisLookHorizontal = "RightJoyStickHorizontal";
            //axisLookVertical = "RightJoyStickVertical";
            //buttonJump = "XButton";
        }
    }

    public float GetAxisMoveHorizontal()
    {
        return Input.GetAxis(inputDictionary[Glob.Keytype.LeftJoystickHorizontal]);
    }
    public float GetAxisMoveVertical()
    {
        return Input.GetAxis(inputDictionary[Glob.Keytype.LeftJoystickVertical]);
    }

    public float GetAxisLookHorizontal()
    {
        return Input.GetAxis(inputDictionary[Glob.Keytype.RightJoystickHorizontal]);
    }
    public float GetAxisLookVertical()
    {
        return Input.GetAxis(inputDictionary[Glob.Keytype.RightJoystickVertical]);
    }

    public bool GetButtonDownJump()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.JumpButton]);
    }
    public bool GetButtonJump()
    {
        return Input.GetButton(inputDictionary[Glob.Keytype.JumpButton]);
    }

    public bool GetButtonDownSpellCast1()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.FireButtonLeft]);
    }
    public bool GetButtonDownSpellCast2()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.FireButtonRight]);
    }

    public bool GetButtonDownElementChange1()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.SwitchButtonLeft]);
    }
    public bool GetButtonDownElementChange2()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.SwitchButtonRight]);
    }
    public bool GetButtonUpElementChange1()
    {
        return Input.GetButtonUp(inputDictionary[Glob.Keytype.SwitchButtonLeft]);
    }
    public bool GetButtonUpElementChange2()
    {
        return Input.GetButtonUp(inputDictionary[Glob.Keytype.SwitchButtonRight]);
    }

    public string Test123()
    {
        return inputDictionary[Glob.Keytype.JumpButton];
    }
}
