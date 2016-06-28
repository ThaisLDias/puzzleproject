using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour {
	
	private int currentScene;

	// Color Game Variables
	private int cg_count;
	private int cg_limit;
	private float cg_rand;
	private GameObject cg_square;

	/*//Puzzle Game Variables
	private GameObject worm;
	private GameObject fish;
	private GameObject bird;
	private static bool next;*/
	
	void Start () {
		currentScene = Application.loadedLevel;
		if (currentScene == 0) {} // "Menu"
		if (currentScene == 1) {} // "Lobby"
		if (currentScene == 2) {} // "Credits"
		if (currentScene == 3) cg_init (); // "Color Game"
		if (currentScene == 4) {} // "Word Game"
		if (currentScene == 5) {} //pg_init (); // "Puzzle Game"
	}
	
	void Update() {
		if (currentScene == 0) Menu ();
		if (currentScene == 1) Lobby ();
		if (currentScene == 2) Credits ();
		if (currentScene == 3) ColorGame ();
		if (currentScene == 4) WordGame ();
		if (currentScene == 5) /*next = OtherSnap.completed;*/ PuzzleGame ();
	}
	
	void Menu() {

	}

	void Lobby() {
		
	}

	void Credits() {
		
	}

	void cg_init() {
		cg_count = 0;
		cg_limit = 7;
		cg_square = Resources.Load("Prefabs/Object") as GameObject;
	}
	/*
	void pg_init(){
		fish = GameObject.Find("Fish");
		worm = GameObject.Find("Worm");
		bird = GameObject.Find("Bird");
	}*/
	
	void ColorGame() {
		if (cg_count >= cg_limit) {
			cg_init ();
			Application.LoadLevel (1);
		} else {
			if (GameObject.Find ("Object") == null
			&& GameObject.Find ("Object(Clone)") == null) {
				cg_count++;
				cg_rand = Random.Range(1f, 20f);
				if (cg_rand <= 10) {
					cg_square.GetComponent<SpriteRenderer>().color = Color.red;
					cg_square.gameObject.tag = "Color1";
				} else {
					cg_square.GetComponent<SpriteRenderer>().color = Color.green;
					cg_square.gameObject.tag = "Color2";
				}
				if (cg_count < cg_limit) Instantiate(cg_square, new Vector3(-5,0,0), Quaternion.identity);
			}
		}
	}

	void WordGame() {

	}

	void PuzzleGame() {
	}


// Utils

	public void ChangeScene (int level) {
		Application.LoadLevel (level);
	}
	
	public void ChangeScene (string level) {
		Application.LoadLevel (level);
	}
}
