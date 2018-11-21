using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    private string moveAxisHorizontal;
    private string moveAxisVertical;

    private string lookAxisHorizontal;
    private string lookAxisVertical;

    private string jumpButton;

    private string spellCastButton1;
    private string spellCastButton2;

    public InputManager()
    {
        //Initialize all the button layouts here.
        spellCastButton1 = "JoyStickLeftBumper";
        spellCastButton2 = "JoyStickRightBumper";
        moveAxisHorizontal = "LeftJoyStickHorizontal";
        moveAxisVertical = "LeftJoyStickVertical";
        lookAxisHorizontal = "RightJoyStickHorizontal";
        lookAxisVertical = "RightJoyStickVertical";
        jumpButton = "XButton";
    }

    public string GetMoveAxisHorizontal()
    {
        return moveAxisHorizontal;
    }
    public string GetMoveAxisVertical()
    {
        return moveAxisVertical;
    }

    public string GetLookAxisHorizontal()
    {
        return lookAxisHorizontal;
    }
    public string GetLookAxisVertical()
    {
        return lookAxisVertical;
    }

    public string GetJumpButton()
    {
        return jumpButton;
    }

    public string GetSpellCastButton1()
    {
        return spellCastButton1;
    }
    public string GetSpellCastButton2()
    {
        return spellCastButton2;
    }
}
