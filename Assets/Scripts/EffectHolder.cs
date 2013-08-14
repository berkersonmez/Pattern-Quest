using UnityEngine;
using System.Collections;

public class EffectHolder : MonoBehaviour {
	
	public static EffectHolder instance;
	
	public GameObject normalAttack;
	public GameObject fireballProjectile;
	public GameObject fireballExplosion;
	
	public Vector3 playerPos = new Vector3(-5.6f, -8.1f, -2.3f);
	public Vector3 creaturePos = new Vector3(5.6f, -8.1f, -2.3f);
	
	void Awake() {
		instance = this;
	}
}
