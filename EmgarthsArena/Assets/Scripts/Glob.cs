﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob
{
    private static int playerCount = -1;
    public static int GetPlayerCount()
    {
        if (playerCount == -1)
        {
            //Testing purposes adding an empty player.
            playerCount = Input.GetJoystickNames().Length;
        }
        return playerCount;
    }

    public const float camYOffset = 3.25f;
    public const float camSpeed = 0.15f;
    public const int maxLives = 5;
    public const int maxHealth = 100;
    public const int maxMana = 100;
    public const float respawnDelay = 3;
    public const float maxInvulnerableTime = 2;
    public const float playerSpeed = 20f;
    public const float jumpHeight = 800f;
    public const float jumpHeightContinuous = 6700f;
    public const float jumpDoubleHeight = 725f;
    public const float jumpTimeContinuous = 0.8f;
    public const float spellOffset = 2f;

    public const string PlayerPrefab = "Player/Player";
    public static GameObject GetPlayerPrefab()
    {
        return Resources.Load<GameObject>(PlayerPrefab);
    }
    public const string PlayerBannerPrefab = "UI/PlayerBanner/CharacterBanner";
    public const string ResolutionScreenStatsPrefab = "UI/ResolutionScreen/PlayerStats";

    public const string FullLifeCrystal = "UI/PlayerBanner/CrystalBlueLive";
    public const string EmptyLifeCrystal = "UI/PlayerBanner/CrystalBlueDead";

    public const string FireElementIcon = "UI/PlayerBanner/ElementIcon/FireSymbol";
    public const string WaterElementIcon = "UI/PlayerBanner/ElementIcon/WaterSymbol";
    public const string EarthElementIcon = "UI/PlayerBanner/ElementIcon/EarthSymbol";

    private const int arenaCount = 1;
    public const string BaseArenaPrefab = "Arenas/Arena";
    public const string Arena1Prefab = "Arenas/Arena1";

    public const string DeathParticle = "Player/DeathParticle";

    public static Arena GetArenaPrefab(int id)
    {
        if (id >= arenaCount)
        {
            return null;
        }
        return Resources.Load<Arena>(BaseArenaPrefab + (id+1));
    }

    public const string FireFirePrefab = "Spells/FireFire";
    public const string FireWaterPrefab = "Spells/FireWater";
    public const string FireEarthPrefab = "Spells/FireEarth";
    public const string WaterFirePrefab = "Spells/WaterFire";
    public const string WaterWaterPrefab = "Spells/WaterWater";
    public const string WaterEarthPrefab = "Spells/WaterEarth";
    public const string EarthFirePrefab = "Spells/EarthFire";
    public const string EarthWaterPrefab = "Spells/EarthWater";
    public const string EarthEarthPrefab = "Spells/EarthEarth";
    public const int SpellCount = 9;

    public static Spell[] GetSpellPrefab()
    {
        Spell[] Spells = new Spell[SpellCount];
        Spells[0] = Resources.Load<FireFireSpell>(FireFirePrefab);
        Spells[1] = Resources.Load<FireWaterSpell>(FireWaterPrefab);
        Spells[2] = Resources.Load<FireEarthSpell>(FireEarthPrefab);
        Spells[3] = Resources.Load<WaterFireSpell>(WaterFirePrefab);
        Spells[4] = Resources.Load<WaterWaterSpell>(WaterWaterPrefab);
        Spells[5] = Resources.Load<WaterEarthSpell>(WaterEarthPrefab);
        Spells[6] = Resources.Load<EarthFireSpell>(EarthFirePrefab);
        Spells[7] = Resources.Load<EarthWaterSpell>(EarthWaterPrefab);
        Spells[8] = Resources.Load<EarthEarthSpell>(EarthEarthPrefab);

        return Spells;
    }

    public const string ExplosionPrefab = "Explosion";
    public static GameObject GetExplosionPrefab()
    {
        return Resources.Load<GameObject>(ExplosionPrefab);
    }

    public static Dictionary<Keytype, string> keyboardControllers = new Dictionary<Keytype, string>();
    public static Dictionary<Keytype, string> inputControllersP1 = new  Dictionary<Keytype, string>();
    public static Dictionary<Keytype, string> inputControllersP2 = new Dictionary<Keytype, string>();

    public static void FillInputDictionary()
    {
        keyboardControllers.Add(Keytype.JumpButton, "XButton");
        keyboardControllers.Add(Keytype.LeftJoystickHorizontal, "KeyboardMoveHorizontal");
        keyboardControllers.Add(Keytype.LeftJoystickVertical, "KeyboardMoveVertical");
        keyboardControllers.Add(Keytype.RightJoystickHorizontal, "MouseX");
        keyboardControllers.Add(Keytype.RightJoystickVertical, "MouseY");
        keyboardControllers.Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger");
        keyboardControllers.Add(Keytype.FireButtonRight, "JoyStickRightTrigger");
        keyboardControllers.Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper");
        keyboardControllers.Add(Keytype.SwitchButtonRight, "JoyStickRightBumper");

        inputControllersP1.Add(Keytype.JumpButton, "XButton");
        inputControllersP1.Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal");
        inputControllersP1.Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical");
        inputControllersP1.Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal");
        inputControllersP1.Add(Keytype.RightJoystickVertical, "RightJoyStickVertical");
        inputControllersP1.Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger");
        inputControllersP1.Add(Keytype.FireButtonRight, "JoyStickRightTrigger");
        inputControllersP1.Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper");
        inputControllersP1.Add(Keytype.SwitchButtonRight, "JoyStickRightBumper");

        inputControllersP2.Add(Keytype.JumpButton, "XButton2");
        inputControllersP2.Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal2");
        inputControllersP2.Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical2");
        inputControllersP2.Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal2");
        inputControllersP2.Add(Keytype.RightJoystickVertical, "RightJoyStickVertical2");
        inputControllersP2.Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger2");
        inputControllersP2.Add(Keytype.FireButtonRight, "JoyStickRightTrigger2");
        inputControllersP2.Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper2");
        inputControllersP2.Add(Keytype.SwitchButtonRight, "JoyStickRightBumper2");
    }

    public static Dictionary<Keytype, string> GetInputDictionary(int pPlayerNumber)
    {
        switch(pPlayerNumber)
        {
            case -1:
                return keyboardControllers;
                break;
            case 0:
                return inputControllersP1;
                break;
            case 1:
                return inputControllersP2;
                break;
        }
        Debug.Log("Something went wrong");
        return inputControllersP1;
    }


    public enum Keytype
    {
        JumpButton,
        LeftJoystickHorizontal,
        LeftJoystickVertical,
        RightJoystickHorizontal,
        RightJoystickVertical,
        FireButtonLeft,
        FireButtonRight,
        SwitchButtonLeft,
        SwitchButtonRight
    }

    public const float FireFireSpeed = 30f;
    public const float FireWaterSpeed = 15f;
    public const float FireEarthSpeed = 10f;
    public const float WaterFireSpeed = 6f;
    public const float WaterWaterSpeed = 10f;
    public const float WaterEarthSpeed = 10f;
    public const float EarthFireSpeed = 10f;
    public const float EarthWaterSpeed = 10f;
    public const float EarthEarthSpeed = 10f;
}
