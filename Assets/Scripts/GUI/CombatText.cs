using UnityEngine;
using System.Collections;

public class CombatText : MonoBehaviour {

	float speed;
	
	void Start () {
		speed = .15f;
		Invoke("slowdown", .2f);
		Invoke("destroy", 1.5f);
	}
	
	void FixedUpdate() {
		transform.localScale -= new Vector3(speed, speed, 0f);
	}

	void slowdown() {
		speed = .005f;
	}

	void destroy() {
		Destroy(gameObject);
	}
}
