using UnityEngine;
using System.Collections;

public class TownMenu : MonoBehaviour {

	public static TownMenu instance;
	
	public tk2dCamera camera;
	public float cameraSmooth = 100f;
	public Vector3 menuPosition = new Vector3(0, 0, -10);
	public Vector3 mapPosition = new Vector3(24, 0, -10);
	public Vector3 shopPosition = new Vector3(-24, 0, -10);
	
	private Vector3 startMarker;
	private Vector3 cameraTarget;
	private float startTime;
    private float journeyLength;
	private bool moveCamera = false;
	
	void Start() {
		instance = this;
		camera = Camera.main.GetComponent<tk2dCamera>();
		startMarker = transform.position;
		cameraTarget = transform.position;
		
		DontDestroyOnLoad(GameObject.Find("_LevelDescriptor"));
	}
	
	public void menuWindow() {
		startTime = Time.time;
		cameraTarget = menuPosition;
		startMarker = transform.position;
        journeyLength = Vector3.Distance(startMarker, cameraTarget);
		moveCamera = true;
	}
	
	public void mapWindow() {
		startTime = Time.time;
		cameraTarget = mapPosition;
		startMarker = transform.position;
        journeyLength = Vector3.Distance(startMarker, cameraTarget);
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