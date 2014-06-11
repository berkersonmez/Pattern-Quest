using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
	public static Globals instance;

	public int statPointsPerLevel = 3;
	public int xpPerCreatureLevel = 10;
	public int xpStartingReq = 100;
	public float xpReqMultiplier = 1.5f;
	public GameObject tutorialPrefab;
	
	void Awake() {
		instance = this;
	}
	
	public static List<Spell> allSpells = new List<Spell>();
	
	public static List<Spell> getSpellsFromAllSpells(List<string> spellNamesList){
		List<Spell> spells = new List<Spell>();
		foreach(string spellName in spellNamesList){
			foreach(Spell spell in allSpells)
				if(spell.name == spellName)
					spells.Add(spell);
		}
		if(spells.Count > 0)
			return spells;
		else
			return null;
	}

	public Spell getSpell(string spellName, Creature owner){
		System.Type spellObject = System.Type.GetType(spellName,true);
		Spell spell = (Spell)(System.Activator.CreateInstance(spellObject));
		spell.owner = owner;
		return spell;
	}

	public Power getPower(string spellName, Creature owner){
		System.Type spellObject = System.Type.GetType(spellName,true);
		Power spell = (Power)(System.Activator.CreateInstance(spellObject));
		spell.owner = owner;
		return spell;
	}
	
	public List<Spell> getSpells(List<string> spellNamesList, Creature owner){
		List<Spell> spells = new List<Spell>();
		foreach(string name in spellNamesList){
			if(name == "Basic Attack"){
				continue;
			}
			System.Type spellObject = System.Type.GetType(name,true);
			Spell spell = (Spell)(System.Activator.CreateInstance(spellObject));
			spell.owner = owner;
			spells.Add(spell);
		}
		return spells;
	}

	public Spell createSpell(string className, string allParameters, Creature owner){
		if(className == "Basic Attack"){
			return null;
		}
		Debug.Log("ALL PARAMETERS: " + allParameters);
		Debug.Log("CLASS NAME: " + className);
		System.Type spellObject = System.Type.GetType(className,true);
		Spell spell = (Spell)(System.Activator.CreateInstance(spellObject));
		spell.owner = owner;
		if(allParameters != null)
			spell.setValues(allParameters);
		return spell;
	}

	public List<Power> getPowers(List<string> spellNamesList, Creature owner){
		List<Power> spells = new List<Power>();
		foreach(string name in spellNamesList){
			if(name == "Basic Attack"){
				continue;
			}
			System.Type spellObject = System.Type.GetType(name,true);
			Power spell = (Power)(System.Activator.CreateInstance(spellObject));
			spell.owner = owner;
			spells.Add(spell);
		}
		return spells;
	}
}
