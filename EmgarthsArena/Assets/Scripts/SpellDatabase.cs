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
        allSpells[Element.Fire].Add(Element.Fire,Glob.GetSpellPrefab()[0]);
        allSpells[Element.Fire].Add(Element.Water, Glob.GetSpellPrefab()[1]);
        allSpells[Element.Fire].Add(Element.Earth, Glob.GetSpellPrefab()[2]);

        allSpells[Element.Water].Add(Element.Fire, Glob.GetSpellPrefab()[3]);
        allSpells[Element.Water].Add(Element.Water, Glob.GetSpellPrefab()[4]);
        allSpells[Element.Water].Add(Element.Earth, Glob.GetSpellPrefab()[5]);

        allSpells[Element.Earth].Add(Element.Fire, Glob.GetSpellPrefab()[6]);
        allSpells[Element.Earth].Add(Element.Water, Glob.GetSpellPrefab()[7]);
        allSpells[Element.Earth].Add(Element.Earth, Glob.GetSpellPrefab()[8]);
    }

    public Dictionary<Element, Dictionary<Element, Spell>> allSpells = new Dictionary<Element, Dictionary<Element, Spell>>();

    public enum Element
    {
        Fire,
        Water,
        Earth,
        Null
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
