using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Creature {
	
	public string name;
	public Item weapon;
	public Item head;
	public Item necklace;
	public Item wrist;
	public Item waist;
	public Item shoulder;
	public Item gloves;
	public Item boots;
	public Item leg;
	public Item chest;
	
	public Player(){
		isPlayer = true;
	}
}
