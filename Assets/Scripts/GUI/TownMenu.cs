using UnityEngine;
using System.Collections;

public class TownMenu : MonoBehaviour {

	public static TownMenu instance;
	
	public tk2dCamera cam;
	public float cameraSmooth = 100f;
	public Vector3 menuPosition = new Vector3(0, 0, -10);
	public Vector3 mapPosition = new Vector3(24, 0, -10);
	public Vector3 shopPosition = new Vector3(-24, 0, -10);
	public Vector3 inventoryPosition = new Vector3(-24, 0, -10);
	public Vector3 spellsPosition = new Vector3(0, 40, -10);
	public Vector3 drawPatternPosition = new Vector3(0, 80, -10);
	
	private Vector3 cameraTarget;
	private bool moveCamera = false;

	void Start() {
		instance = this;
		cam = Camera.main.GetComponent<tk2dCamera>();
		cameraTarget = transform.position;
	}
	
	public void menuWindow() {
		cameraTarget = menuPosition;
		moveCamera = true;
	}
	
	public void mapWindow() {
		cameraTarget = mapPosition;
		moveCamera = true;
	}

	public void inventoryWindow() {
		cameraTarget = inventoryPosition;
		moveCamera = true;
	}

	public void spellsWindow() {
		cameraTarget = spellsPosition;
		moveCamera = true;
	}

	public void drawPatternWindow() {
		cameraTarget = drawPatternPosition;
		moveCamera = true;
	}
	
	void Update() {
		if (moveCamera) {
	        transform.position = Vector3.Lerp(transform.position, cameraTarget, cameraSmooth * Time.deltaTime);
			if (transform.position == cameraTarget) {
				moveCamera = false;
			}
		}
    }
}
