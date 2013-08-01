using UnityEngine;
using System.Collections;

public class PatternButton : MonoBehaviour {
	
	public int x;
	public int y;
	public bool highlighted = false;
	
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
	
	public void onHover() {
		PatternController.instance.addToPattern(x,y);
	}
}
