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
	public Vector3 statsPosition = new Vector3(-48, 0, -10);
	public Vector3 powersPosition = new Vector3(24, 40, -10);
	public Vector3 questsPosition = new Vector3(24, -40, -10);
	public Vector3 characterMenuPosition = new Vector3(-24, -40, -10);
	public Vector3 talentsPosition = new Vector3(-48, -40, -10);
	
	private Vector3 cameraTarget;
	private bool moveCamera = false;

	void Start() {
		instance = this;
		cam = Camera.main.GetComponent<tk2dCamera>();
		cameraTarget = transform.position;
	}
	
	public void menuWindow() {
		cameraTarget = menuPosition;
		fadeOut();
		//moveCamera = true;
	}
	
	public void mapWindow() {
		cameraTarget = mapPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void inventoryWindow() {
		cameraTarget = inventoryPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void spellsWindow() {
		cameraTarget = spellsPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void drawPatternWindow() {
		cameraTarget = drawPatternPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void statsWindow() {
		cameraTarget = statsPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void powersWindow() {
		cameraTarget = powersPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void questsWindow() {
		cameraTarget = questsPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void characterMenuWindow() {
		cameraTarget = characterMenuPosition;
		fadeOut();
		//moveCamera = true;
	}

	public void talentsWindow() {
		cameraTarget = talentsPosition;
		fadeOut();
		//moveCamera = true;
	}

	void fadeOut() {
		System.Action action = new System.Action(windowChange);
		CameraFade.StartAlphaFade( Color.black, false, .5f, 0, action );
	}

	void windowChange() {
		transform.position = cameraTarget;
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
