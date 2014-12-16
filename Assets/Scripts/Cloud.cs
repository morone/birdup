using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	private tk2dCamera cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 camPos = cam.transform.position;

		if (transform.position.y < camPos.y - 7) {
			Destroy (this.gameObject);
		}
	
	}
}
