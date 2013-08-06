using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals : MonoBehaviour {
	public static Globals instance;
	
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
	
	public List<Spell> getSpells(List<string> spellNamesList){
		List<Spell> spells = new List<Spell>();
		foreach(string name in spellNamesList){
			if(name == "Basic Attack"){
				continue;
			}
			System.Type spellObject = System.Type.GetType(name,true);
			Spell spell = (Spell)(System.Activator.CreateInstance(spellObject));
			spells.Add(spell);
		}
		return spells;
	}
}
