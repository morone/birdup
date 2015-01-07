﻿using UnityEngine;
using System.Collections;

public class TryAgain : MonoBehaviour {

	private AudioSource _confirm;

	// Use this for initialization
	void Start () {
		_confirm = GameObject.Find ("Confirm").GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)){
			_confirm.audio.Play();
			StartCoroutine(MyMethod());
		}
	}

	IEnumerator MyMethod() {
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel("InGame");
	}

}
