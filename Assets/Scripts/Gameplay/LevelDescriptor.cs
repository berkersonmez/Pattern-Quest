using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDescriptor : MonoBehaviour {
	
	public static LevelDescriptor instance;
	public Queue<string> creatureNameList;
	public int currentDungeon;
	public bool goToMapOnLoad = false;
	
	
	void Start() {
		instance = this;
	}
	
	// Fills "creatureNameList" with creature names in the level
	public void describeLevel(int dungeonNo) {
		creatureNameList = XmlParse.instance.getMapCreatures(dungeonNo);
		currentDungeon = dungeonNo;
	}
	
	public void resetLevel() {
		creatureNameList = XmlParse.instance.getMapCreatures(currentDungeon);
	}
	
	public Creature getNextCreature() {
		if (creatureNameList.Count != 0) {
			Creature crtre = XmlParse.instance.getCreature(creatureNameList.Dequeue());
			return crtre;
		}
		return null;
	}
}
