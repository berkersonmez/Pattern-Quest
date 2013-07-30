using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	
	public string name;
	public int damage;
	public int spellPower;
	public string type;
	public int armor;
	public int hp;
	public int mana;
	public int level;
	
	public Item(){
		damage = 0;
		spellPower = 0;
		type = "common";
		armor = 0;
		hp = 0;
		mana = 0;
		level = 1;		
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
