using System.Collections;
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

    public const float camXOffset = 0f;
    public const float camYOffset = 3.25f;
    public const float camZOffset = -15f;
    public const float camSpeed = 0.15f;
    public const int maxLives = 5;
    public const int maxHealth = 100;
    public const int maxMana = 100;
    public const float respawnDelay = 3;
    public const float maxInvulnerableTime = 2;
    public const float playerSpeed = 20f;
    public const float jumpHeight = 800f;
    public const float jumpHeightContinuous = 6700f;
    public const float jumpDoubleHeight = 700f;
    public const float jumpDoubleHeightContinuous = 6000f;
    public const float jumpTimeContinuous = 0.8f;
    public const float jumpDoubleTimeContinuous = 0.8f;
    public const float spellOffset = 2f;
    public static GameObject GetKnockback()
    {
        return Resources.Load<GameObject>("Spells/Knockback");
    }

    public const string PlayerPrefab = "Player/Player";
    public static GameObject GetPlayerPrefab()
    {
        return Resources.Load<GameObject>(PlayerPrefab);
    }
    public const string PlayerBannerPrefab = "UI/PlayerBanner/CharacterBanner";
    public const string ResolutionScreenStatsPrefab = "UI/ResolutionScreen/PlayerStats";

    public const string FullLifeCrystalBase = "UI/PlayerBanner/CrystalLive";
    public const string FullLifeCrystal1 = "UI/PlayerBanner/CrystalLive1";
    public const string FullLifeCrystal2 = "UI/PlayerBanner/CrystalLive2";
    public const string FullLifeCrystal3 = "UI/PlayerBanner/CrystalLive3";
    public const string FullLifeCrystal4 = "UI/PlayerBanner/CrystalLive4";
    public const string EmptyLifeCrystal = "UI/PlayerBanner/CrystalBlueDead";

    public const string PlayerIndicatorBase = "UI/PlayerIndicator";
    public const string PlayerIndicator1 = "UI/PlayerIndicator1";
    public const string PlayerIndicator2 = "UI/PlayerIndicator2";
    public const string PlayerIndicator3 = "UI/PlayerIndicator3";
    public const string PlayerIndicator4 = "UI/PlayerIndicator4";

    public const string FireElementIcon = "UI/PlayerBanner/ElementIcon/FireSymbol";
    public const string WaterElementIcon = "UI/PlayerBanner/ElementIcon/WaterSymbol";
    public const string EarthElementIcon = "UI/PlayerBanner/ElementIcon/EarthSymbol";
    public const string FireElementSelectedIcon = "UI/PlayerBanner/ElementIcon/FireSymbolSelected";
    public const string WaterElementSelectedIcon = "UI/PlayerBanner/ElementIcon/WaterSymbolSelected";
    public const string EarthElementSelectedIcon = "UI/PlayerBanner/ElementIcon/EarthSymbolSelected";

    private const int arenaCount = 2;
    public const string BaseArenaPrefab = "Arenas/Arena";
    public const string Arena1Prefab = "Arenas/Arena1";

    public const string DeathParticle = "Player/DeathParticle";

    public static Arena GetArenaPrefab(int id)
    {
        if (id >= arenaCount)
        {
            return null;
        }
        return Resources.Load<Arena>(BaseArenaPrefab + (id + 1));
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

    public const string RockPrefab = "Spells/EarthEarthRock";
    public const string FireHazardPrefab = "Spells/FireHazard";

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
    public static Dictionary<Keytype, string> inputControllersP1 = new Dictionary<Keytype, string>();
    public static Dictionary<Keytype, string> inputControllersP2 = new Dictionary<Keytype, string>();

    public static void FillInputDictionary()
    {
        keyboardControllers.Add(Keytype.JumpButton, "AButton");
        keyboardControllers.Add(Keytype.LeftJoystickHorizontal, "KeyboardMoveHorizontal");
        keyboardControllers.Add(Keytype.LeftJoystickVertical, "KeyboardMoveVertical");
        keyboardControllers.Add(Keytype.RightJoystickHorizontal, "MouseX");
        keyboardControllers.Add(Keytype.RightJoystickVertical, "MouseY");
        keyboardControllers.Add(Keytype.FireButtonLeft, "JoyStickLeftBumper");
        keyboardControllers.Add(Keytype.FireButtonRight, "JoyStickRightBumper");
        keyboardControllers.Add(Keytype.SwitchButtonLeft, "JoyStickLeftTrigger");
        keyboardControllers.Add(Keytype.SwitchButtonRight, "JoyStickRightTrigger");

        inputControllersP1.Add(Keytype.JumpButton, "AButton");
        inputControllersP1.Add(Keytype.FireElementButton, "BButton");
        inputControllersP1.Add(Keytype.WaterElementButton, "XButton");
        inputControllersP1.Add(Keytype.EarthElementButton, "YButton");
        inputControllersP1.Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal");
        inputControllersP1.Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical");
        inputControllersP1.Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal");
        inputControllersP1.Add(Keytype.RightJoystickVertical, "RightJoyStickVertical");
        inputControllersP1.Add(Keytype.FireButtonLeft, "JoyStickLeftBumper");
        inputControllersP1.Add(Keytype.FireButtonRight, "JoyStickRightBumper");
        inputControllersP1.Add(Keytype.SwitchButtonLeft, "JoyStickLeftTrigger");
        inputControllersP1.Add(Keytype.SwitchButtonRight, "JoyStickRightTrigger");

        inputControllersP2.Add(Keytype.JumpButton, "AButton2");
        inputControllersP2.Add(Keytype.FireElementButton, "BButton2");
        inputControllersP2.Add(Keytype.WaterElementButton, "XButton2");
        inputControllersP2.Add(Keytype.EarthElementButton, "YButton2");
        inputControllersP2.Add(Keytype.LeftJoystickHorizontal, "LeftJoyStickHorizontal2");
        inputControllersP2.Add(Keytype.LeftJoystickVertical, "LeftJoyStickVertical2");
        inputControllersP2.Add(Keytype.RightJoystickHorizontal, "RightJoyStickHorizontal2");
        inputControllersP2.Add(Keytype.RightJoystickVertical, "RightJoyStickVertical2");
        inputControllersP2.Add(Keytype.FireButtonLeft, "JoyStickLeftBumper2");
        inputControllersP2.Add(Keytype.FireButtonRight, "JoyStickRightBumper2");
        inputControllersP2.Add(Keytype.SwitchButtonLeft, "JoyStickLeftTrigger2");
        inputControllersP2.Add(Keytype.SwitchButtonRight, "JoyStickRightTrigger2");
    }

    public static Dictionary<Keytype, string> GetInputDictionary(int pPlayerNumber)
    {
        switch (pPlayerNumber)
        {
            case -1:
                return keyboardControllers;
            case 0:
                return inputControllersP1;
            case 1:
                return inputControllersP2;
        }
        Debug.Log("Something went wrong");
        return inputControllersP1;
    }


    public enum Keytype
    {
        JumpButton,
        FireElementButton,
        WaterElementButton,
        EarthElementButton,
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
    public const float FireEarthSpeed = 20f;
    public const float WaterFireSpeed = 6f;
    public const float WaterWaterSpeed = 10f;
    public const float WaterEarthSpeed = 10f;
    public const float EarthFireSpeed = 10f;
    public const float EarthWaterSpeed = 10f;
    public const float EarthEarthSpeed = 10f;

    public const float FireHazardSpeed = 5;
}
