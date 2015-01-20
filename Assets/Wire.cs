using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour {

	private OneBird oneBird;
	tk2dSpriteAnimator animator;
	private bool flag = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator> ();
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro"))&&(flag==false)) {
			animator.Play("WireAnimate");
			flag = true;
		}
	}
}
