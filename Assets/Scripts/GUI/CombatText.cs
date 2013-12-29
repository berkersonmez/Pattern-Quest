using UnityEngine;
using System.Collections;

public class CombatText : MonoBehaviour {

	float speed;
	
	void Start () {
		speed = .65f;
		GameObject effectDamageObj = transform.Find("Damage").gameObject;
		tk2dTextMesh efectDamage = effectDamageObj.GetComponent<tk2dTextMesh>();
		efectDamage.scale /= efectDamage.text.Length > 10 ? 2.5f : 1;

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
