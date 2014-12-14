using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {

	private OneBird oneBird;

	public float distance;

	private const float MAX_DIST = 80f;
	private float _parallaxFactor;

	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();

		_parallaxFactor = distance / MAX_DIST;

		if (_parallaxFactor > 1) {
			_parallaxFactor = 1f;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((oneBird.GetStatus() == "flying")||(oneBird.GetStatus() == "nitro")) {
			transform.Translate (oneBird.speed * _parallaxFactor * Time.deltaTime);
		}
	}
}
