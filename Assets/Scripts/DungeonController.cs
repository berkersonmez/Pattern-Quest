using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	public static DungeonController instance;
	public Creature currentCreature;
	public Battle battle;
	public Player player;
	
	void Start () {
		instance = this;
		Invoke ("enterDungeon", 1f);
		player = new Player();
	}
	
	public void enterDungeon() {
		currentCreature = LevelDescriptor.instance.getNextCreature();
		Debug.Log(currentCreature.name);
		startBattle();
	}
	
	public void startBattle() {
		battle = new Battle(player, currentCreature);
	}
}
