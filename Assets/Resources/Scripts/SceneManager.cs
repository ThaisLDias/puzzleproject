using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour {
	
	private int currentScene;

	[SerializeField]
	/*private GameObject plane;
	private Texture snapshot;
	public bool allowToTurn = false;*/

	// Color Game Variables
	private int cg_count;
	private int cg_limit;
	private float cg_rand;
	private GameObject cg_square;
	private Sprite[] cg_sprites;

	void Start () {
		currentScene = Application.loadedLevel;
		if (currentScene == 0) {} // "Menu"
		if (currentScene == 1) {} // "Lobby"
		if (currentScene == 2) {} // "Credits"
		if (currentScene == 3) cg_init (); // "Color Game"
		if (currentScene == 4) {} // "Word Game"
		if (currentScene == 5) {} // "Puzzle Game"
	}
	
	void Update() {
		if (currentScene == 0) Menu ();
		if (currentScene == 1) Lobby ();
		if (currentScene == 2) Credits ();
		if (currentScene == 3) ColorGame ();
		if (currentScene == 4) WordGame ();
		if (currentScene == 5) PuzzleGame ();
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
		cg_sprites = new Sprite[6];
		cg_square.GetComponent<SpriteRenderer>().sprite = cg_sprites[Mathf.RoundToInt(Random.Range(0, 60) / 10)];
		cg_sprites [0] = Resources.Load<Sprite> ("Sprites/aviao");
		cg_sprites [1] = Resources.Load<Sprite> ("Sprites/carro");
		cg_sprites [2] = Resources.Load<Sprite> ("Sprites/casa");
		cg_sprites [3] = Resources.Load<Sprite> ("Sprites/bola");
		cg_sprites [4] = Resources.Load<Sprite> ("Sprites/boneca");
		cg_sprites [5] = Resources.Load<Sprite> ("Sprites/trem");
	}
	
	void ColorGame() {
		cg_square.GetComponent<SpriteRenderer>().sprite = cg_sprites[Mathf.RoundToInt(Random.Range(0, 30) / 10)];
		if (cg_count >= cg_limit) {
			cg_init ();
			GetComponent<PhpConnect>().UpdateHighScore(Application.loadedLevelName, PlayerPrefs.GetInt("acertos"), PlayerPrefs.GetInt("erros"));
			PlayerPrefs.DeleteKey ("acertos");
			PlayerPrefs.DeleteKey ("erros");
			Application.LoadLevel (1);
		} else {
			if (GameObject.Find ("Object") == null
			&& GameObject.Find ("Object(Clone)") == null) {
				cg_count++;
				cg_rand = Random.Range(0, 5);
				if (cg_rand <= 2) {
					cg_square.GetComponent<SpriteRenderer>().sprite = cg_sprites[Mathf.RoundToInt (Random.Range (0, 2))];
					cg_square.GetComponent<SpriteRenderer>().color = Color.white;
					cg_square.gameObject.tag = "Color1";
				} else {
					cg_square.GetComponent<SpriteRenderer>().sprite = cg_sprites[Mathf.RoundToInt (Random.Range (3, 5))];
					cg_square.GetComponent<SpriteRenderer>().color = Color.white;
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

	public void OpenURL() {
		Application.OpenURL("http://pupper.esy.es/pupper.php");
	}
}