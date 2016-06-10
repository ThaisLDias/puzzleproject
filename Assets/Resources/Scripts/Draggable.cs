﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Draggable : MonoBehaviour {

	private Transform t;
    private Camera mainCam;
    private Vector3 offset;
    private bool isTouching;
    private bool canDie;

	void Start()
	{
        isTouching = false;
        canDie = true;
		t = this.transform;
		mainCam = Camera.main;
	}

    void Update() {
        isTouching = Input.GetMouseButton(0);
    }

	void OnMouseDown()
	{
		Vector2 mousePos = Input.mousePosition;
		float distance = mainCam.WorldToScreenPoint(t.position).z;
		Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
		offset = t.position - worldPos;
	}

	void OnMouseDrag()
	{
		Vector2 mousePos = Input.mousePosition;
		float distance = mainCam.WorldToScreenPoint(t.position).z;
		t.position = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance)) + offset;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.gameObject.tag == col.tag && !isTouching) {
            StartCoroutine(DieAnim());
            //Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (this.gameObject.tag == col.tag && !isTouching) {
            StartCoroutine(DieAnim());
            //Destroy(this.gameObject);
        }
    }

    IEnumerator DieAnim() {
		if (Application.loadedLevel != 5) {
			if (canDie) transform.DOBlendableScaleBy (new Vector3 (-2.5f, -2.5f, -2.5f), 0.5f);
			canDie = false;
			yield return new WaitForSeconds (0.5f);
			Destroy (this.gameObject);
		}
    }
}
