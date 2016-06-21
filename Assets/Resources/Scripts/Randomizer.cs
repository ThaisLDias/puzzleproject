using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Randomizer : MonoBehaviour {

	public Sprite[] spriteList;

	string[] names = new string[5] {
        "d u c k",
        "f i s h",
        "b i r d",
        "l i o n",
        "w o r m"
	};

	GameObject letter;

	void Start () {
		PickWord ();
		spriteList = Resources.LoadAll<Sprite> ("Sprites");
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
			SpriteRenderer sr = GameObject.FindGameObjectWithTag("Image").GetComponent<SpriteRenderer>();
			sr.sprite = (Sprite)findSpriteByname(pick);

			string[] n = (Shuffle (pick.Split(' ')));
			for (int i = 0, l = n.Length; i < l; i++) {
				letter = GameObject.FindGameObjectWithTag("Letter" + (i+1));
				if (letter) {
					letter.GetComponent<Text>().text = n[i].ToUpper();
				} else {
					Debug.Log ("Woops. We're sorry for the inconvenience!");
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
}