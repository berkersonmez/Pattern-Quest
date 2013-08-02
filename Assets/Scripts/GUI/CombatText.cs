using UnityEngine;
using System.Collections;

public class CombatText : MonoBehaviour {

	Vector3 speed;
	
	void Start () {
		speed = new Vector3(Random.Range(-.2f, .2f), .5f, 0);
		Invoke("destroy", 1.5f);
	}
	
	public void setText(string text) {
		GetComponent<tk2dTextMesh>().text = text;
		GetComponent<tk2dTextMesh>().Commit();
	}
	
	void FixedUpdate() {
		transform.position += speed;
		speed.y -= .05f;
	}
	
	void destroy() {
		Destroy(gameObject);
	}
}
