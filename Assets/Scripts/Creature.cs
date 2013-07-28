using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creature
{
	public int hp;
	public int currentHp;
	public int mana;
	public int currentMana;
	public int spellPower;
	public int level=1;
	public bool isPlayer=false;
	public string type;
	public List<Spell> spellList = new List<Spell>();
	
	public Creature (){
		
	}
	
	public Creature (string type, int level){
		//GET CREATURES FROM AN EXTERNAL FILE
	}
}


