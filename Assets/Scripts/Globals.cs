using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Globals {
	
	public static List<Spell> allSpells = new List<Spell>();
	
	public static List<Spell> getSpells(List<string> spellNamesList){
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
	
}
