using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Stats {

	public Dictionary<string, int> stats;
	
	public Stats() {
		stats = new Dictionary<string, int> {
			{"Monsters killed", 0},
			{"Quests completed", 0},
			{"Most damage done", 0},
			{"Most damage taken", 0},
			{"Gold spent", 0},
			{"Crystals spent", 0},
			{"Questions answered", 0}
		};
	}
	
	public void countingStat(string statName, int value) {
		stats[statName] += value;
	}
	
	public void highValueStat(string statName, int value) {
		if (value > stats[statName]) stats[statName] = value;
	}
	
	public void lowValueStat(string statName, int value) {
		if (value < stats[statName]) stats[statName] = value;
	}
	
	public string getStatsText() {
		string text = "";
		foreach (KeyValuePair<string, int> kvp in stats) {
			text += kvp.Key + ": " + kvp.Value + "\n";
		}
		return text;
	}
}
