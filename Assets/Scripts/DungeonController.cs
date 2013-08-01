using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	public static DungeonController instance;
	public Creature currentCreature;
	
	void Start () {
		instance = this;
		Invoke ("enterDungeon", 1f);
	}
	
	public void enterDungeon() {
		currentCreature = LevelDescriptor.instance.getNextCreature();
		Debug.Log(currentCreature.name);
	}
	
}
