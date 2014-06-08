using UnityEngine;
using System.Collections;

public class TutorialWindow : MonoBehaviour {
	
	void Start() {
		transform.Find("CloseButton").GetComponent<tk2dUIItem>().OnClick += OnClose;
		initialize();
	}
	
	public void initialize() {
		this.transform.parent = GameObject.Find("TutorialAnchor").transform;
		this.transform.localPosition = new Vector3(0f, 0f, 0f);
	}
	
	void OnClose() {
		Destroy(this.gameObject);
	}
}
