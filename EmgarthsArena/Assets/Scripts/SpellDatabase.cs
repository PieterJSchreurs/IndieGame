using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase {
    private static SpellDatabase _instance;
    public static SpellDatabase GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SpellDatabase();
        }
        return _instance;
    }

    private SpellDatabase()
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

    public Dictionary<Element, Dictionary<Element, Spell>> allSpells = new Dictionary<Element, Dictionary<Element, Spell>>();

    public enum Element
    {
        Fire,
        Water,
        Earth
    }

    public Spell GetSpell(Element firstEle, Element secondEle)
    {
        if (!allSpells.ContainsKey(firstEle))
        {
            Debug.Log("Key does not exist in the spell database.");
            return null;
        }
        return allSpells[firstEle][secondEle];
    }
}
