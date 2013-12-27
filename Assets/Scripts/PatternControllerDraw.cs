using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatternControllerDraw : PatternController {
	public new static PatternControllerDraw instance;

	protected int index;
	public List<int[]> patternToDraw;

	void Start () {
		instance = this;
		createButtons();
		index = 0;
	}

	public void setPattern(List<int> dirRep) {
		patternToDraw = convertToCoordinational(dirRep);
	}

	public void playDraw() {
		InvokeRepeating("drawNode", .5f, .2f);
	}

	public void drawNode() {
		if (index < patternToDraw.Count) {
			addToPattern(patternToDraw[index][0], patternToDraw[index][1]);
			index++;
		} else {
			CancelInvoke("drawNode");
		}
	}

	protected override void createButtons() {
		for (int i = 0 ; i < 4 ; i++) {
			for (int j = 0 ; j < 4 ; j++) {
				GameObject obj = Instantiate(buttonPrefab) as GameObject;
				obj.transform.parent = this.transform;
				obj.transform.localPosition = new Vector3(j * 3.5f - 5.25f, 2.5f + i * 3.5f, 0);
				buttons[i,j] = obj.GetComponent<PatternButton>();
				buttons[i,j].x = i;
				buttons[i,j].y = j;
			}
		}
	}

	public override void drawBridgeBetween(int[] n1, int[] n2, int bridgeCount) {
		GameObject obj = Instantiate(bridgePrefab) as GameObject;
		obj.transform.parent = this.transform;
		Vector3 n1Pos = buttons[n1[0], n1[1]].transform.localPosition;
		Vector3 n2Pos = buttons[n2[0], n2[1]].transform.localPosition;
		obj.transform.localPosition = (n1Pos + n2Pos) / 2;
		obj.transform.localPosition += new Vector3(0, 0, 1f - bridgeCount * .1f);
		string bridgeColorStr = (bridgeCount + 1) + "_";
		obj.GetComponent<tk2dSprite>().SetSprite("pattern_bridge_" + bridgeColorStr + bridgeType(n1, n2));
		bridges.Add(obj);
	}

	public void cleanPattern() {
		foreach(int[] node in pattern) {
			buttons[node[0], node[1]].unhighlight();
		}
		pattern.Clear();
		if (patternToDraw != null)
			patternToDraw.Clear();
		foreach(GameObject bridge in bridges) {
			Destroy(bridge);
		}
		bridges.Clear();
		index = 0;
	}
}
