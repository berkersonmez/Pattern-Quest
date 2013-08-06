using UnityEngine;
using System.Collections;

public class DungeonCamera : MonoBehaviour {
	enum Window {BATTLE = 0, WIN, LOSE};

	public static DungeonCamera instance;
	public tk2dCamera cam;
	public float cameraSmooth = 10f;
	public Vector3 battlePosition = new Vector3(0, 0, -10);
	public Vector3 winPosition = new Vector3(0, 40, -10);
	public Vector3 losePosition = new Vector3(0, -40, -10);
	
	
	private Vector3 cameraTarget;
	private bool moveCamera = false;
	private int windowMode = 0;
	void Start() {
		instance = this;
		cam = Camera.main.GetComponent<tk2dCamera>();
		cameraTarget = transform.position;
	}
	
	public void battleWindow() {
		cameraTarget = battlePosition;
		moveCamera = true;
		windowMode = (int)Window.BATTLE;
	}
	
	public void winWindow() {
		cameraTarget = winPosition;
		moveCamera = true;
		windowMode = (int)Window.WIN;
	}
	
	public void loseWindow() {
		cameraTarget = losePosition;
		moveCamera = true;
		windowMode = (int)Window.LOSE;
	}
	
	void Update() {
		if (moveCamera) {
	        transform.position = Vector3.Lerp(transform.position, cameraTarget, cameraSmooth * Time.deltaTime);
			if (Vector3.Distance(transform.position, cameraTarget) < .1f) {
				moveCamera = false;
			}
		}
    }
}
