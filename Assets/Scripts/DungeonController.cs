using UnityEngine;
using System.Collections;

public class DungeonController : MonoBehaviour {
	public static DungeonController instance;
	public Creature currentCreature;
	public Battle battle;
	public Player player;
	
	private Healthbar mobHpBar;
	private Healthbar playerHpBar;
	private Manabar mobManaBar;
	private Manabar playerManaBar;
	
	void Start () {
		instance = this;
		Invoke ("enterDungeon", 1f);
		player = new Player();
		mobHpBar = GameObject.Find("Healthbar Mob").GetComponent<Healthbar>();
		playerHpBar = GameObject.Find("Healthbar Player").GetComponent<Healthbar>();
		mobManaBar = GameObject.Find("Manabar Mob").GetComponent<Manabar>();
		playerManaBar = GameObject.Find("Manabar Player").GetComponent<Manabar>();
		playerHpBar.owner = player;
		playerManaBar.owner = player;
	}
	
	public void enterDungeon() {
		currentCreature = LevelDescriptor.instance.getNextCreature();
		Debug.Log(currentCreature.name);
		startBattle();
	}
	
	public void startBattle() {
		battle = new Battle(ref player, ref currentCreature);
		mobHpBar.owner = currentCreature;
		mobManaBar.owner = currentCreature;
	}
}
