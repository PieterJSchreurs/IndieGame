using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    private string axisMoveHorizontal = "Horizontal";
    private string axisMoveVertical = "Vertical";

    private string axisLookHorizontal = "MouseX";
    private string axisLookVertical = "MouseY";

    private string buttonJump = "Jump";

    private string buttonSpellCast1 = "Fire1";
    private string buttonSpellCast2 = "Fire2";

    public InputManager()
    {
        //Initialize all the button layouts here.
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

    public bool GetButtonDownSpellCast1()
    {
        return Input.GetButtonDown(buttonSpellCast1);
        //return spellCastButton1;
    }
    public bool GetButtonDownSpellCast2()
    {
        return Input.GetButtonDown(buttonSpellCast2);
        //return spellCastButton2;
    }
}
