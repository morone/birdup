using UnityEngine;
using System.Collections;

public class Parrot : MonoBehaviour {

	private OneBird oneBird;
	private bool _direction;
	//private tk2dCamera cam;


	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		//cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();

		if (transform.position.x < 0) {
			_direction = true;
		} else {
			_direction = false;
			Vector3 newScale = transform.localScale;
			newScale.x *=-1;
			transform.localScale = newScale;
		}
	}


	
	// Update is called once per frame
	void Update () {
		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
			if (_direction == true) {
					transform.Translate (0.11f, 0.1f, 0);
			} else {
					transform.Translate (-0.11f, 0.1f, 0);
			}
		}
	}

	/*void CheckPosition(){
		if ((transform.position.x < cam.transform.position.x - 2.6f)||(transform.position.x > cam.transform.position.x + 2.6f)) {
			Destroy(this.gameObject);
		}
	}*/
}
