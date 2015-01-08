using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject _confirm;
	private tk2dUIManager manager;

	// Use this for initialization
	void Start () {
		manager = GameObject.Find ("manager").GetComponent<tk2dUIManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)){
			GameObject confirmation = (GameObject) Instantiate (_confirm, new Vector3 (0, 0, 0), Quaternion.identity);
			confirmation.audio.Play();
			Destroy (confirmation.gameObject, 2);

			StartCoroutine(MyMethod());
		}
	}
	
	IEnumerator MyMethod() {
		yield return new WaitForSeconds(1.5f);
		Destroy (manager.gameObject, 0);
		Application.LoadLevel("InGame");
	}
}
