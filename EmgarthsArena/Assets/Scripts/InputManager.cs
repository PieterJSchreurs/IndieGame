using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    private const bool keyboardControls = false;
    
    Dictionary<Glob.Keytype, string> inputDictionary;


    public InputManager(int pPlayerNumber)
    {
        inputDictionary = Glob.GetInputDictionary(pPlayerNumber);
        if (!keyboardControls)
        {

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

    public bool GetButtonFireElement()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.FireElementButton]);
    }

    public bool GetButtonWaterElement()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.WaterElementButton]);
    }

    public bool GetButtonEarthElement()
    {
        return Input.GetButtonDown(inputDictionary[Glob.Keytype.EarthElementButton]);
    }
}
