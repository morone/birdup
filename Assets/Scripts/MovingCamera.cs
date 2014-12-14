using UnityEngine;
using System.Collections;

public class MovingCamera : MonoBehaviour {

	private OneBird oneBird;

	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
	}
	
	// Update is called once per frame
	void Update () {

		if ((oneBird.GetStatus() == "flying")||(oneBird.GetStatus() == "nitro")) {
			transform.Translate (oneBird.speed * Time.deltaTime);
		}
	}
}
