using UnityEngine;
using System.Collections;

public class TownMenu : MonoBehaviour {

	public static TownMenu instance;
	
	public tk2dCamera cam;
	public float cameraSmooth = 100f;
	public Vector3 menuPosition = new Vector3(0, 0, -10);
	public Vector3 mapPosition = new Vector3(72, 0, -10);
	public Vector3 shopPosition = new Vector3(-24, 0, -10);
	public Vector3 inventoryPosition = new Vector3(-24, 0, -10);
	public Vector3 spellsPosition = new Vector3(0, 40, -10);
	public Vector3 drawPatternPosition = new Vector3(0, 80, -10);
	public Vector3 statsPosition = new Vector3(-48, 0, -10);
	public Vector3 powersPosition = new Vector3(24, 40, -10);
	public Vector3 questsPosition = new Vector3(24, -40, -10);
	public Vector3 characterMenuPosition = new Vector3(-24, -40, -10);
	public Vector3 talentsPosition = new Vector3(-48, -40, -10);
	public Vector3 quizPosition = new Vector3(0, -80, -10);
	public Vector3 quizCategoriesPosition = new Vector3(0, -120, -10);
	public Vector3 storePosition = new Vector3(72, 80, -10);
	public Vector3 storeCombosPosition = new Vector3(144, 80, -10);
	public Vector3 storeItemsPosition = new Vector3(96, 80, -10);
	public Vector3 storePowersPosition = new Vector3(168, 80, -10);
	public Vector3 storeSpellsPosition = new Vector3(120, 80, -10);
	public Vector3 storeCrystalPacksPosition = new Vector3(192, 80, -10);
	
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
		moveCamera = false;
	}
	
	public void mapWindow() {
		cameraTarget = mapPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void mapWindowRight() {
		cameraTarget = transform.position + new Vector3(24, 0, 0);
		moveCamera = true;
	}
	
	public void mapWindowLeft() {
		cameraTarget = transform.position - new Vector3(24, 0, 0);
		moveCamera = true;
	}

	public void inventoryWindow() {
		cameraTarget = inventoryPosition;
		fadeOut();
		moveCamera = false;
	}

	public void spellsWindow() {
		cameraTarget = spellsPosition;
		fadeOut();
		moveCamera = false;
	}

	public void drawPatternWindow() {
		cameraTarget = drawPatternPosition;
		fadeOut();
		moveCamera = false;
	}

	public void statsWindow() {
		cameraTarget = statsPosition;
		fadeOut();
		moveCamera = false;
	}

	public void powersWindow() {
		cameraTarget = powersPosition;
		fadeOut();
		moveCamera = false;
	}

	public void questsWindow() {
		cameraTarget = questsPosition;
		fadeOut();
		moveCamera = false;
	}

	public void characterMenuWindow() {
		cameraTarget = characterMenuPosition;
		fadeOut();
		moveCamera = false;
	}

	public void talentsWindow() {
		cameraTarget = talentsPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void quizWindow() {
		cameraTarget = quizPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void quizCategoriesWindow() {
		cameraTarget = quizCategoriesPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storeWindow() {
		cameraTarget = storePosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storeCombosWindow() {
		cameraTarget = storeCombosPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storeItemsWindow() {
		cameraTarget = storeItemsPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storePowersWindow() {
		cameraTarget = storePowersPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storeSpellsWindow() {
		cameraTarget = storeSpellsPosition;
		fadeOut();
		moveCamera = false;
	}
	
	public void storeCrystalPacksWindow() {
		cameraTarget = storeCrystalPacksPosition;
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
		if (moveCamera) {
	        transform.position = Vector3.Lerp(transform.position, cameraTarget, cameraSmooth * Time.deltaTime);
			if (transform.position == cameraTarget) {
				moveCamera = false;
			}
		}
    }
}
