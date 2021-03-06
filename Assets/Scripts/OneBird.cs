﻿using UnityEngine;
using System.Collections;

public class OneBird : MonoBehaviour {

	public Vector3 speed;
	private float _nitro = 0;
	private string _status = "stopped";
	private tk2dCamera mainCamera;
	public GameObject gameOver;
	public GameObject tryagain;



	tk2dSpriteAnimator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator> ();
		mainCamera = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
		_status = "stopped";

	}

	public string GetStatus(){
		return _status;
	}
	public void SetStatus(string status){
		_status = status;
	}

	
	// Update is called once per frame
	void Update () {

		if (_status == "flying") {
			_nitro = -3;
			animator.Play ("OneBirdFlying");
		} else if (_status == "nitro") {
			_nitro = 3;
			animator.Play ("OneBirdNitro");
		} 

		if ((_status != "stopped")&&(_status != "dead")) {
			transform.Translate (Input.acceleration.x, (speed.y + _nitro) * Time.deltaTime, 0);
			CheckPosition ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if ((collision.gameObject.tag == "Enemy") && (_status != "dead")) {
			animator.Play ("OneBirdDead");
			this.rigidbody.constraints =  RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
			collision.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
			GameControl.SetGameOver();
			StartCoroutine(GameControl.ShowTryAgain());
		}

	}


	private void CheckPosition(){
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, mainCamera.transform.position.x-2.6f, mainCamera.transform.position.x+2.6f), Mathf.Clamp(transform.position.y, mainCamera.transform.position.y-8f, mainCamera.transform.position.y+4.6f), transform.position.z);
	}

}
