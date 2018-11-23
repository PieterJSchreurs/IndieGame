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
            playerCount = Input.GetJoystickNames().Length+1;
        }
        return playerCount;
    }

    public const float camYOffset = 3.25f;
    public const float camSpeed = 0.25f;
    public const int maxLives = 5;
    public const int maxHealth = 100;
    public const float playerSpeed = 20f;
    public const float jumpHeight = 800f;
    public const float jumpHeightContinuous = 6700f;
    public const float jumpDoubleHeight = 725f;
    public const float jumpTimeContinuous = 0.8f;
    public const float spellOffset = 2f;
    public static int amountOfPlayers = 2;

    public static Vector3[] spawningPoints = new Vector3[] { new Vector3(-5, 4.5f, 0), new Vector3(5, 4.5f, 0) };

    public const string PlayerPrefab = "Player/Player";
    public static GameObject GetPlayerPrefab()
    {
        return Resources.Load<GameObject>(PlayerPrefab);
    }
    public const string PlayerBannerPrefab = "PlayerBanner/CharacterBanner";

    public const string FireElementIcon = "PlayerBanner/ElementIcon/FireRune";
    public const string WaterElementIcon = "PlayerBanner/ElementIcon/WaterRune";
    public const string EarthElementIcon = "PlayerBanner/ElementIcon/EarthRune";

    private const int arenaCount = 1;
    public const string BaseArenaPrefab = "Arenas/Arena";
    public const string Arena1Prefab = "Arenas/Arena1";

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
    public const string EarthEarthPrefab = "Spells/EarthEarthPrefab";
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

    public static Dictionary<int, Dictionary<Keytype, string>> inputControllers = new Dictionary<int, Dictionary<Keytype, string>>();

    public static void FillInputDictionary()
    {
        inputControllers.Add(0, new Dictionary<Keytype, string>());
        inputControllers.Add(1, new Dictionary<Keytype, string>());
        inputControllers.Add(2, new Dictionary<Keytype, string>());
        inputControllers.Add(3, new Dictionary<Keytype, string>());

        inputControllers[0].Add(Keytype.JumpButton, "XButton");
        inputControllers[0].Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal");
        inputControllers[0].Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical");
        inputControllers[0].Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal");
        inputControllers[0].Add(Keytype.RightJoystickVertical, "RightJoyStickVertical");
        inputControllers[0].Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger");
        inputControllers[0].Add(Keytype.FireButtonRight, "JoyStickRightTrigger");
        inputControllers[0].Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper");
        inputControllers[0].Add(Keytype.SwitchButtonRight, "JoyStickRightBumper");

        inputControllers[1].Add(Keytype.JumpButton, "XButton2");
        inputControllers[1].Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal2");
        inputControllers[1].Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical2");
        inputControllers[1].Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal2");
        inputControllers[1].Add(Keytype.RightJoystickVertical, "RightJoyStickVertical2");
        inputControllers[1].Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger2");
        inputControllers[1].Add(Keytype.FireButtonRight, "JoyStickRightTrigger2");
        inputControllers[1].Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper2");
        inputControllers[1].Add(Keytype.SwitchButtonRight, "JoyStickRightBumper2");

        inputControllers[2].Add(Keytype.JumpButton, "XButton3");
        inputControllers[2].Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal3");
        inputControllers[2].Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical3");
        inputControllers[2].Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal3");
        inputControllers[2].Add(Keytype.RightJoystickVertical, "RightJoyStickVertical3");
        inputControllers[2].Add(Keytype.FireButtonLeft, "JoyStickLeftTrigger3");
        inputControllers[2].Add(Keytype.FireButtonRight, "JoyStickRightTrigger3");
        inputControllers[2].Add(Keytype.SwitchButtonLeft, "JoyStickLeftBumper3");
        inputControllers[2].Add(Keytype.SwitchButtonRight, "JoyStickRightBumper3");


    }

    public static Dictionary<int, Dictionary<Keytype, string>> GetInputDictionary()
    {
        return inputControllers;
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

    public const float FireFireSpeed = 10f;
    public const float FireWaterSpeed = 15f;
    public const float FireEarthSpeed = 10f;
    public const float WaterFireSpeed = 6f;
    public const float WaterWaterSpeed = 10f;
    public const float WaterEarthSpeed = 10f;
    public const float EarthFireSpeed = 10f;
    public const float EarthWaterSpeed = 10f;
    public const float EarthEarthSpeed = 10f;
}
