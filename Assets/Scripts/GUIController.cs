using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	void Start () {
		UIHorizontalLayout[] patternHLayout = new UIHorizontalLayout[4];
		
		
		UIButton[,] patternButtons = new UIButton[4,4];
		for (int i = 0 ; i < 4 ; i++) {
			patternHLayout[i] = new UIHorizontalLayout(10);
			patternHLayout[i].pixelsFromTopLeft(100 + i * 64, 10);
			for (int j = 0 ; j < 4 ; j++) {
				patternButtons[i,j] = UIButton.create( "pattern_circle.png", "pattern_circle.png", 0, 0 );
				patternHLayout[i].addChild(patternButtons[i,j]);
			}
		}
				
	}
}
