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
			StartCoroutine(MyMethod());
		}
	}

	IEnumerator MyMethod() {
		yield return new WaitForSeconds(0.2f);
		Application.LoadLevel("InGame");
	}

}
