using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase {
    private static SpellDatabase Instance;
    public static SpellDatabase GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SpellDatabase();
        }
        return Instance;
    }

    public Dictionary<Element, Dictionary<Element, Spell>> allSpells = new Dictionary<Element, Dictionary<Element, Spell>>();

    public enum Element
    {
        Fire,
        Water,
        Earth
    }

    void Start()
    {
        allSpells.Add(Element.Fire, new Dictionary<Element, Spell>());
        allSpells.Add(Element.Water, new Dictionary<Element, Spell>());
        allSpells.Add(Element.Earth, new Dictionary<Element, Spell>());
        allSpells[Element.Fire].Add(Element.Fire, new FireFireSpell());
        allSpells[Element.Fire].Add(Element.Water, new FireWaterSpell());
        allSpells[Element.Fire].Add(Element.Earth, new FireEarthSpell());

        allSpells[Element.Water].Add(Element.Fire, new WaterFireSpell());
        allSpells[Element.Water].Add(Element.Water, new WaterWaterSpell());
        allSpells[Element.Water].Add(Element.Earth, new WaterEarthSpell());

        allSpells[Element.Earth].Add(Element.Fire, new EarthFireSpell());
        allSpells[Element.Earth].Add(Element.Water, new EarthWaterSpell());
        allSpells[Element.Earth].Add(Element.Earth, new EarthEarthSpell());
    }

    public Spell GetSpell(Element firstEle, Element secondEle)
    {
        return allSpells[firstEle][secondEle];
    }
}
