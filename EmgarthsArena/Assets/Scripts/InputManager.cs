using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
    private string moveAxisHorizontal;
    private string moveAxisVertical;

    private string lookAxisHorizontal;
    private string lookAxisVertical;

    private string spellCastButton1;
    private string spellCastButton2;

    public string GetMoveAxisHorizontal()
    {
        return moveAxisHorizontal;
    }
    public string GetMoveAxisVertical()
    {
        return moveAxisVertical;
    }
}
