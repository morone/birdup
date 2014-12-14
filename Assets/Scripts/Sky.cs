using UnityEngine;
using System.Collections;

public class Sky : MonoBehaviour {

	private OneBird oneBird;

	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (oneBird.GetStatus() == "flying") {
			transform.Translate (oneBird.speed * Time.deltaTime);
		}
	}
}
