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

    Dictionary<int, Dictionary<Glob.Keytype, string>> inputDictionary = Glob.GetInputDictionary();


    public InputManager()
    {
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

    public float GetAxisMoveHorizontal(int pPlayerNumber)
    {
        return Input.GetAxis(inputDictionary[pPlayerNumber][Glob.Keytype.LeftJoystickHorizontal]);
    }
    public float GetAxisMoveVertical(int pPlayerNumber)
    {
        return Input.GetAxis(inputDictionary[pPlayerNumber][Glob.Keytype.LeftJoystickVertical]);
    }

    public float GetAxisLookHorizontal(int pPlayerNumber)
    {
        return Input.GetAxis(inputDictionary[pPlayerNumber][Glob.Keytype.RightJoystickHorizontal]);
    }
    public float GetAxisLookVertical(int pPlayerNumber)
    {
        return Input.GetAxis(inputDictionary[pPlayerNumber][Glob.Keytype.RightJoystickVertical]);
    }

    public bool GetButtonDownJump(int pPlayerNumber)
    {
        return Input.GetButtonDown(inputDictionary[pPlayerNumber][Glob.Keytype.JumpButton]);
    }
    public bool GetButtonJump(int pPlayerNumber)
    {
        return Input.GetButton(inputDictionary[pPlayerNumber][Glob.Keytype.JumpButton]);
    }

    public bool GetButtonDownSpellCast1(int pPlayerNumber)
    {
        return Input.GetButtonDown(inputDictionary[pPlayerNumber][Glob.Keytype.FireButtonLeft]);
    }
    public bool GetButtonDownSpellCast2(int pPlayerNumber)
    {
        return Input.GetButtonDown(inputDictionary[pPlayerNumber][Glob.Keytype.FireButtonRight]);
    }

    public bool GetButtonDownElementChange1(int pPlayerNumber)
    {
        return Input.GetButtonDown(inputDictionary[pPlayerNumber][Glob.Keytype.SwitchButtonLeft]);
    }
    public bool GetButtonDownElementChange2(int pPlayerNumber)
    {
        return Input.GetButtonDown(inputDictionary[pPlayerNumber][Glob.Keytype.SwitchButtonRight]);
    }
    public bool GetButtonUpElementChange1(int pPlayerNumber)
    {
        return Input.GetButtonUp(inputDictionary[pPlayerNumber][Glob.Keytype.SwitchButtonLeft]);
    }
    public bool GetButtonUpElementChange2(int pPlayerNumber)
    {
        return Input.GetButtonUp(inputDictionary[pPlayerNumber][Glob.Keytype.SwitchButtonRight]);
    }
}
