using UnityEngine;
using System.Collections;

public class PatternButton : MonoBehaviour {
	
	public int x;
	public int y;
	public bool highlighted = false;
	public bool critPoint = false;
	
	private tk2dSprite sprite;
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<tk2dSprite>();
	}
	
	public void highlight() {
		highlighted = true;
		sprite.SetSprite("pattern_circle_h");
	}
	
	public void unhighlight() {
		highlighted = false;
		sprite.SetSprite("pattern_circle");
	}

	public void makeCritPoint() {
		critPoint = true;
		sprite.color = Color.red;
	}

	public void clearCritPoint() {
		critPoint = false;
		sprite.color = Color.white;
	}
	
	public void onHover() {
		PatternController.instance.addToPattern(x,y);
	}
}
