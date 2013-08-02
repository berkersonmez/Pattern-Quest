﻿using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour {
	public Creature owner;
	private tk2dUIProgressBar progressBar;
	
	void Start() {
		progressBar = GetComponent<tk2dUIProgressBar>();
	}
	
	void Update () {
		if (owner != null) {
			progressBar.Value = (float)owner.currentHp / owner.hp;
		}
	}
}
