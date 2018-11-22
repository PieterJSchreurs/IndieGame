using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Glob
{
    public const float camYOffset = 3.25f;
    public const float camSpeed = 0.25f;
    public const int maxLives = 5;
    public const int maxHealth = 100;
    public const float playerSpeed = 20f;
    public const float jumpHeight = 800f;
    public const float jumpHeightContinuous = 6700f;
    public const float jumpDoubleHeight = 725f;
    public const float jumpTimeContinuous = 0.8f;


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

    public static Dictionary<int, Dictionary<int, string>> inputControllers = new Dictionary<int, Dictionary<int, string>>()
    {

        //{  inputControllers.Add(0, new Dictionary<int, string>());
    };

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
