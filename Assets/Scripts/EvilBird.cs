using UnityEngine;
using System.Collections;

public class EvilBird : MonoBehaviour {

	private OneBird oneBird;
	public bool direction;
	public GameObject EvilBirdSound;

	public float CurveSpeed = 5;
	public float MoveSpeed = 1;
	
	float fTime = 0;


	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		GameObject psound = (GameObject)Instantiate (EvilBirdSound, new Vector3 (-11.16f, -1.94f, 3.17f), Quaternion.identity);
		Destroy (psound.gameObject, 1);


		if (transform.position.x < 0) {
			direction = true;
		} else {
			direction = false;
			Vector3 newScale = transform.localScale;
			newScale.x *=-1;
			transform.localScale = newScale;
		}


	}
	
	
	// Update is called once per frame
	void Update () {
		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
			if (direction == true) {
				SineMove();
				transform.Translate (0.06f, 0.1f, 0);
			} else {
				SineMove();
				transform.Translate (-0.06f, 0.1f, 0);
			}
		}
		
		
		CheckPosision ();
		
		
	}

	void SineMove(){

		fTime += Time.deltaTime * CurveSpeed;
		
		Vector3 vSin = new Vector3(Mathf.Sin(fTime), -Mathf.Sin(fTime), 0);
		Vector3 vLin = new Vector3(MoveSpeed, MoveSpeed, 0);
		
		transform.position += (vSin + vLin) * Time.deltaTime;
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
