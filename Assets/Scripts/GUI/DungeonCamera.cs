using UnityEngine;
using System.Collections;

public class DungeonCamera : MonoBehaviour {

	public static DungeonCamera instance;
	public tk2dCamera cam;
	public float cameraSmooth = 10f;
	public Vector3 battlePosition = new Vector3(0, 0, -10);
	public Vector3 winPosition = new Vector3(0, 40, -10);
	public Vector3 losePosition = new Vector3(0, -40, -10);

	public Vector3 originPos;
	public bool shakingCamera;
	public int shakeCounter;
	
	private Vector3 cameraTarget;
	private bool moveCamera = false;

	void Start() {
		instance = this;
		Screen.SetResolution(480, 800, true);
		cam = Camera.main.GetComponent<tk2dCamera>();
		cameraTarget = transform.position;
	}

	public void shakeCamera(int shakeCount) {
		originPos = transform.position;
		shakingCamera = true;
		shakeCounter = shakeCount;
		InvokeRepeating("shakeNow", .01f, .01f);
	}

	void shakeNow() {
		Vector2 randomXY = (Random.insideUnitCircle * shakeCounter) / 10;
		transform.position = originPos + new Vector3(randomXY.x , randomXY.y, originPos.z);
		
		shakeCounter--;
		if (shakeCounter == 0) {
			shakingCamera = false;
			transform.position = originPos;
			CancelInvoke("shakeNow");
		}
	}
	
	public void battleWindow() {
		cameraTarget = battlePosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void winWindow() {
		cameraTarget = winPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void loseWindow() {
		cameraTarget = losePosition;
		fadeOut();
		moveCamera = false;
	}
	
	void fadeOut() {
		System.Action action = new System.Action(windowChange);
		CameraFade.StartAlphaFade( Color.black, false, .5f, 0, action );
	}
	
	void windowChange() {
		transform.position = cameraTarget;
	}
	
	void Update() {
		if (moveCamera && !shakingCamera) {
	        transform.position = Vector3.Lerp(transform.position, cameraTarget, cameraSmooth * Time.deltaTime);
			if (Vector3.Distance(transform.position, cameraTarget) < .01f) {
				moveCamera = false;
			}
		}
    }
}
