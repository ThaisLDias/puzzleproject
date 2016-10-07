using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Draggable : MonoBehaviour {

	private Transform t;
    private Camera mainCam;
    private Vector3 offset;
    private bool isTouching;
    private bool canDie;

	private int acertos;
	private int erros;

	public bool snapped;
	public GameObject sceneManager;

	void Start() {
		isTouching = snapped = false;
        canDie = true;
		t = this.transform;
		mainCam = Camera.main;
	}

    void Update() {
        isTouching = Input.GetMouseButton(0);
    }

	void OnMouseDown() {
		Vector2 mousePos = Input.mousePosition;
		float distance = mainCam.WorldToScreenPoint(t.position).z;
		Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
		offset = t.position - worldPos;
		//this.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 2;
	}

	void OnMouseDrag() {
		Vector2 mousePos = Input.mousePosition;
		float distance = mainCam.WorldToScreenPoint(t.position).z;
		t.position = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)) + offset;
	}

	void OnMouseUp()
	{
		//this.gameObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (this.gameObject.tag == col.tag && isTouching) {
			PlayerPrefs.SetInt ("acertos", PlayerPrefs.GetInt("acertos") + 1);
		} else
			PlayerPrefs.SetInt ("erros", PlayerPrefs.GetInt("erros") + 1);
    }

    void OnTriggerStay2D(Collider2D col) {
        if (this.gameObject.tag == col.tag && !isTouching) {
			StartCoroutine (DieAnim ());

		}
    }

    IEnumerator DieAnim() {
		if (Application.loadedLevel != 5) {
			if (canDie) transform.DOBlendableScaleBy (new Vector3 (-1f, -1f, -1f), 0.5f);
			canDie = false;
			yield return new WaitForSeconds (0.5f);
			Destroy (this.gameObject);

		}
		if(Application.loadedLevel == 6) {
			GameObject.Find("Main Camera").GetComponent<RandomSquad>().eg_Count += 1;
			Debug.Log(GameObject.Find("Main Camera").GetComponent<RandomSquad>().eg_Count);
		}


    }
}
