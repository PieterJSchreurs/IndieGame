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
    public const float camSpeed = 0.25f;
    public const int maxLives = 5;
    public const int maxHealth = 100;
    public const int maxMana = 100;
    public const float respawnDelay = 3;
    public const float maxInvulnerableTime = 2;
    public const float playerSpeed = 20f;
    public const float jumpHeight = 1200f;
    public const float jumpHeightContinuous = 6700f;
    public const float jumpDoubleHeight = 1100f;
    public const float jumpDoubleHeightContinuous = 6000f;
    public const float jumpTimeContinuous = 0.8f;
    public const float jumpDoubleTimeContinuous = 0.8f;
    public const float spellOffset = 3f;
    public const float WaterwaterAliveTime = 0.7f;
    public const float FireWaterAliveTime = 5f;
    public const float EarthWaterAliveTime = 5f;
    public const float ManaIncreasePerSecond = 6f;
    public const float MistStayingTime = 3f;

    public static GameObject GetKnockback()
    {
        return Resources.Load<GameObject>("Spells/Knockback");
    }
    public static GameObject GetKnockback2()
    {
        return Resources.Load<GameObject>("Spells/Knockback2");
    }

    public static GameObject GetFogPrefab()
    {
        return Resources.Load<GameObject>("Fog");
    }

    public const string PlayerPrefab = "Player/Player";
    public const string Player1Prefab = "Player/PlayerOrange";
    public const string Player2Prefab = "Player/PlayerBlue";

    public static GameObject[] GetPlayerPrefabs()
    {
        GameObject[] players = new GameObject[2];
        players[0] = Resources.Load<GameObject>(Player1Prefab);
        players[1] = Resources.Load<GameObject>(Player2Prefab);
        return players;
    }
    public const string PlayerBannerPrefab = "UI/PlayerBanner/CharacterBanner";

    public const int PlayerWinLoseCount = 2;
    public const string PlayerWinBase = "UI/ResolutionScreen/PlayerWin";
    public const string PlayerLoseBase = "UI/ResolutionScreen/PlayerLose";
    public const string PlayerWinPrefab1 = "UI/ResolutionScreen/PlayerWin1";
    public const string PlayerLosePrefab1 = "UI/ResolutionScreen/PlayerLose1";
    public const string PlayerWinPrefab2 = "UI/ResolutionScreen/PlayerWin2";
    public const string PlayerLosePrefab2 = "UI/ResolutionScreen/PlayerLose2";

    public const string PlayerWinTitleBase = "UI/ResolutionScreen/PlayerWinTitle";
    public const string PlayerWinTitle1 = "UI/ResolutionScreen/PlayerWinTitle1";
    public const string PlayerWinTitle2 = "UI/ResolutionScreen/PlayerWinTitle2";

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
    public const string FireBackgroundGlow = "UI/PlayerBanner/SelectedGlowFire";
    public const string WaterBackgroundGlow = "UI/PlayerBanner/SelectedGlowWater";
    public const string EarthBackgroundGlow = "UI/PlayerBanner/SelectedGlowEarth";

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
    public const float maxPilarHeight = 3f;

    public const string RockPrefab = "Spells/Rock";
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
    public const int randomAttackSoundChance = 10;

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
        inputControllersP1.Add(Keytype.JumpButtonJoystick, "LeftJoystickPress");
        inputControllersP1.Add(Keytype.PauseButton, "PauseButton");

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
        inputControllersP2.Add(Keytype.JumpButtonJoystick, "LeftJoystickPress2");
        inputControllersP2.Add(Keytype.PauseButton, "PauseButton2");
    }

    //Player sounds
    public const string Player1JumpSound = "event:/Player1/Jump1";
    public const string Player2JumpSound = "event:/Player2/Jump2";
    public const string Player1HurtSound = "event:/Player1/Hurt1";
    public const string Player2HurtSound = "event:/Player2/Hurt2";
    public const string Player1AttackSound = "event:/Player1/Attack1";
    public const string Player2AttackSound = "event:/Player2/Attack2";
    public const string Player1FallSound = "event:/Player1/Falling1";
    public const string Player2FallSound = "event:/Player2/Falling2";
    public const string PlayerHitSound = "event:/Playerhit/Hit";
    public const string Player1WinSound = "event:/Player1/Celebrate1";
    public const string Player2WinSound = "event:/Player2/Celebrate2";

    //Spell sounds.
    public const string AvalancheChargeSound = "event:/Spells/Avalanchecharge";
    public const string AvalancheHitSound = "event:/Spells/Avalanchehit";
    public const string EarthPillarsSound = "event:/Spells/Earthpillars";
    public const string FireBeamSound = "event:/Spells/Firebeam";
    public const string FirerockImpactSound = "event:/Spells/Firerockimpact";
    public const string FirerockThrowSound = "event:/Spells/Firerockttrhow";
    public const string MeteordropSound = "event:/Spells/Meteordrop";
    public const string MeteorExplosionSound = "event:/Spells/Meteorexplosion";
    public const string MeteorThrowSound = "event:/Spells/Meteorthrow";
    public const string SnowballCastSound = "event:/Spells/SnowballCast";
    public const string SnowballImpactSound = "event:/Spells/Snowballimpact";
    public const string SnowballRollSound = "event:/Spells/Snowballroll";
    public const string SteamcircleSound = "event:/Spells/Steamcircle";
    public const string WaterballSound = "event:/Spells/Waterball";
    public const string WaterballHitSound = "event:/Spells/Waterballhit";
    public const string WaterblastSound = "event:/Spells/Waterblast";

    public const string FightSound = "event:/Announcer/321Fight";
    public const string FuckAroundSound = "event:/Announcer/Fuckaround";
    public const string GameOverSound = "event:/Announcer/Gameover";
    public const string IntroDialogueSound = "event/Introdialogue";
    public const string WelcomeSound = "event:/Announcer/Welcome";
    public const string YouWinSound = "event:/Announcer/Youwin";

    //UI & general
    public const string UIHoveringSound = "event:/UI/Hovering";
    public const string UISelectingSound = "event:/UI/Selecting";
    public const string EmptyManaSound = "event:/UI/Emptymana";

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
        SwitchButtonRight,
        JumpButtonJoystick,
        PauseButton
    }

    public const float FireFireSpeed = 150f;
    public const float FireWaterSpeed = 0f;
    public const float FireEarthSpeed = 20f;
    public const float WaterFireSpeed = 15f;
    public const float WaterWaterSpeed = 0f;
    public const float WaterEarthSpeed = 10f;
    public const float EarthFireSpeed = 25f;
    public const float EarthWaterSpeed = 0f;
    public const float EarthEarthSpeed = 10f;
    public const float FireHazardSpeed = 5;


    private const int numberCount = 3;
    public const string BaseNumberPrefab = "UI/CD";
    public const string NumberPrefab1 = "UI/CD1";
    public const string NumberPrefab2 = "UI/CD2";
    public const string NumberPrefab3 = "UI/CD3";

    public const string FightPrefab = "UI/CDFight";


}
