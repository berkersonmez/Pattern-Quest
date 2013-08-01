using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDescriptor : MonoBehaviour {
	
	public static LevelDescriptor instance;
	public Queue<string> creatureNameList;
	
	
	void Start() {
		instance = this;
	}
	
	// Fills "creatureNameList" with creature names in the level
	public void describeLevel(int dungeonNo) {
		creatureNameList = XmlParse.instance.getMapCreatures(dungeonNo);
	}
	
	public Creature getNextCreature() {
		if (creatureNameList.Count != 0) {
			return XmlParse.instance.getCreature(creatureNameList.Dequeue());
		}
		return null;
	}
}
