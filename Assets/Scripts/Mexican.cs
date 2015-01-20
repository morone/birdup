using UnityEngine;
using System.Collections;

public class Mexican : MonoBehaviour {

	private OneBird oneBird;
	public bool direction;
	public GameObject MexicanSound;

	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();

		if (transform.position.x < 0) {
			direction = true;
		} else {
			direction = false;
			Vector3 newScale = transform.localScale;
			newScale.x *=-1;
			transform.localScale = newScale;
		}
		
		GameObject psound = (GameObject)Instantiate (MexicanSound, new Vector3 (-11.16f, -1.94f, 3.17f), Quaternion.identity);
		Destroy (psound.gameObject, 7.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
			if (direction == true) {
				transform.Translate (0.11f, 0.1f, 0);
			} else {
				transform.Translate (-0.11f, 0.1f, 0);
			}
		}
		
		
		CheckPosision ();
	
	}

	void CheckPosision(){
		if (direction == true) {
			if (transform.position.x > 7)
				Destroy (this.gameObject);
		} else {
			if (transform.position.x < -7)
				Destroy (this.gameObject);
		}
	}
}
