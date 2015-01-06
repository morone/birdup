using UnityEngine;
using System.Collections;

public class TryAgain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)){
			this.audio.Play();
			StartCoroutine(MyMethod());
		}
	}

	IEnumerator MyMethod() {
		yield return new WaitForSeconds(1.5f);
		Application.LoadLevel("InGame");
	}

}
