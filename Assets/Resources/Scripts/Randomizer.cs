using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Randomizer : MonoBehaviour {

	public Sprite[] spriteList = new Sprite[5];
	private int wg_count;
	private int wg_limit;
	
	GameObject[] letterGameObjects = new GameObject[4];
	GameObject[] newLetters = new GameObject[4];
	
	float[] positionsX = new float[4] {
		-257, -77, 110, 285
	};
	float positionY = -230f;

	string[] names = new string[5] {
        "p a t o",
		"l e a o",
		"g a t o",
		"s a p o",
		"f o c a"
	};

	void Start () {
		wg_count = 0;
		wg_limit = 7;
		spriteList = Resources.LoadAll<Sprite> ("Sprites/wg-sprites");
		for (int i = 0, l = letterGameObjects.Length; i < l; i++) {
			letterGameObjects[i] = GameObject.FindGameObjectWithTag("Letter" + (i+1));
		}
		PickWord ();
	}
	
	void Update () {
		if (wg_count >= wg_limit) {
			Application.LoadLevel (1);
		} else if (newLetters [0].transform.position.x < newLetters [1].transform.position.x
		 && newLetters [1].transform.position.x < newLetters [2].transform.position.x
		 && newLetters [2].transform.position.x < newLetters [3].transform.position.x
		 && newLetters [0].GetComponent<Draggable> ().snapped
		 && newLetters [1].GetComponent<Draggable> ().snapped
		 && newLetters [2].GetComponent<Draggable> ().snapped
		 && newLetters [3].GetComponent<Draggable> ().snapped
	     && !Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0)) {
			resetPositions();
			PickWord();
		}
	}

	Sprite findSpriteByname (string name) {
		for (int i = 0, l = spriteList.Length; i < l; i++) {
			if (spriteList[i].name == name) {
				return spriteList[i];
			}
		}
		return null;
	}

	void PickWord () {
		int ran = (int)Random.Range (0f, names.Length);
		string pick = names [ran];

		if (pick != null) {
			GameObject img = GameObject.FindGameObjectWithTag("Image");
			string pickJoined = string.Join("", pick.Split (' '));
			Sprite spr = findSpriteByname(pickJoined);
			img.GetComponent<SpriteRenderer>().sprite = spr;
			if (spr == null) Debug.LogError("Oh damn!");

			string[] n = (Shuffle (pick.Split(' ')));
			for (int i = 0, l = n.Length; i < l; i++) {
				GameObject letter = letterGameObjects[i];
				if (letter) {
					letter.GetComponent<Text>().text = n[i].ToUpper();
					int pos = -1;
					for (int j = 0, k = pickJoined.Length; j < k; j++) {
						if (pickJoined[j].ToString() == n[i]) {
							pos = j;
						}
					}
					newLetters[pos] = letter;
					letter.name = (pos).ToString();
				} else {
					Debug.LogError ("Woops. We're sorry for the inconvenience!");
				}
			}
		}
	}

	string[] Shuffle (string[] texts) {
		for (int t = 0, l = texts.Length; t < l; t++ ) {
			string tmp = texts[t];
			int r = Random.Range(t, texts.Length);
			texts[t] = texts[r];
			texts[r] = tmp;
		}
		return texts;
	}

	void resetPositions () {
		for (int i = 0, l = letterGameObjects.Length; i < l; i++) {
			letterGameObjects[i].transform.localPosition = new Vector2(positionsX[i], positionY);
		}
	}
}