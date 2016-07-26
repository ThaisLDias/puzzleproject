using UnityEngine;
using System.Collections;

public class PG : MonoBehaviour {

	private GameObject worm;
	private GameObject bird;
	private GameObject duck;

	private bool WormWorked;
	private bool BirdWorked;
	private bool DuckWorked;

	GameObject[] bodyGameObjects = new GameObject[3];

	float bird_x = 1.63f;
	float resto_x = 4.94f;

	float[] bird_y = new float[3] {
		2.3f, 0.13f, -1.55f
	};
	float[] worm_y = new float[3] {
		2.06f, 0.13f, -1.55f
	};
	float[] duck_y = new float[3] {
		1.77f, 0.13f, -1.55f
	};

	void Start () {
		worm = GameObject.Find("Worm");
		bird = GameObject.Find("Bird");
		duck = GameObject.Find("Duck");
		WormWorked = false;
		BirdWorked = false;
		DuckWorked = false;
		bird.SetActive (false);
		duck.SetActive (false);
	}

	void Update () {
		if (WormWorked == true && BirdWorked == true && DuckWorked == true) {
			Application.LoadLevel (1);
		} else if (WormWorked == false && BirdWorked == false && DuckWorked == false) {
			Worm ();
		} else if (WormWorked == true && BirdWorked == false && DuckWorked == false) {
			Bird ();
		} else if (WormWorked == true && BirdWorked == true && DuckWorked == false) {
			Duck();
		}
	}

	void Worm() {
		for (int i = 0, l = bodyGameObjects.Length; i < l; i++) {
			bodyGameObjects[i] = GameObject.FindGameObjectWithTag("Body" + (i+1));
		}
		if (bodyGameObjects [0].transform.localPosition.x == resto_x && bodyGameObjects [0].transform.localPosition.y == worm_y [0]
		    && bodyGameObjects [1].transform.localPosition.x == resto_x && bodyGameObjects [1].transform.localPosition.y == worm_y [1]
		    && bodyGameObjects [2].transform.localPosition.x == resto_x && bodyGameObjects [2].transform.localPosition.y == worm_y [2]
		    && !Input.GetMouseButton (0) && !Input.GetMouseButtonDown (0)) {
			WormWorked = true;
			worm.SetActive(false);
			bird.SetActive(true);
		}
	}

	void Bird() {
		for (int i = 0, l = bodyGameObjects.Length; i < l; i++) {
			bodyGameObjects[i] = GameObject.FindGameObjectWithTag("Body" + (i+1));
		}
		if (bodyGameObjects [0].transform.localPosition.x == bird_x && bodyGameObjects [0].transform.localPosition.y == bird_y [0]
		    && bodyGameObjects [1].transform.localPosition.x == bird_x && bodyGameObjects [1].transform.localPosition.y == bird_y [1]
		    && bodyGameObjects [2].transform.localPosition.x == bird_x && bodyGameObjects [2].transform.localPosition.y == bird_y [2]
		    && !Input.GetMouseButton (0) && !Input.GetMouseButtonDown (0)) {
			BirdWorked = true;
			bird.SetActive(false);
			duck.SetActive(true);
		}
	}

	void Duck(){
		for (int i = 0, l = bodyGameObjects.Length; i < l; i++) {
			bodyGameObjects[i] = GameObject.FindGameObjectWithTag("Body" + (i+1));
		}
		if (bodyGameObjects [0].transform.localPosition.x == resto_x && bodyGameObjects [0].transform.localPosition.y == duck_y [0]
		    && bodyGameObjects [1].transform.localPosition.x == resto_x && bodyGameObjects [1].transform.localPosition.y == duck_y [1]
		    && bodyGameObjects [2].transform.localPosition.x == resto_x && bodyGameObjects [2].transform.localPosition.y == duck_y [2]
		    && !Input.GetMouseButton (0) && !Input.GetMouseButtonDown (0)) {
			DuckWorked = true;
		}
	}
}