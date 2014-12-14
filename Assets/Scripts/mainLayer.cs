using UnityEngine;
using System.Collections;

public class mainLayer : MonoBehaviour {

	private OneBird oneBird;
	private tk2dCamera cam;
	private tk2dTextMesh scoreTxt;
	private int score;

	public GameObject cloud;
	public GameObject parrot;
	public GameObject gameOver;
	public GameObject tryagain;

	
	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		scoreTxt = GameObject.Find ("Score").GetComponent<tk2dTextMesh> ();
		cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
	}


	private void GenerateCloud(){

		float chance = Random.Range (1.0f, 10.0f);

		if (chance >=9.88 ){
			float timeToLive;
			float z = Random.Range (0.3f, 7.5f);
			GameObject newCloud = (GameObject) Instantiate (cloud, new Vector3 (Random.Range(-3.3f, 3.3f), cam.transform.position.y + 20, z), Quaternion.identity);

			string Layer;

			if (z >= 0.3 && z <= 5.14) {
				timeToLive = 5.0f;
				Layer =  "Layer1";
			}else if( z> 5.14 && z <= 6.1){
				timeToLive = 10.0f;
				Layer =  "Layer2";
			}else{
				timeToLive = 40.0f;
				Layer = "Layer3";
			}

			newCloud.transform.parent = GameObject.Find (Layer).transform;	
			Destroy (newCloud.gameObject, timeToLive);
		}
	}

	private void GenerateParrot(){
		float chance = Random.Range (1.0f, 10.0f);
		if (chance >= 9.9) {//if (chance >= 9.97) {


			//The prefab is delete after T seconds.
			float timeToLive = 10.0f;

			//Instatiate a new parrot
			GameObject newEnemy;

			//Randomize the startup position.
			int direction = (int)Random.Range (0.0f,1.9f);

			//If direction is 1, then the parrot appears from the left. Else, from the Right.
			float x;

			if(direction==1){
				x = -9.5f;
			}else{
				x = 9.5f;
			}

			newEnemy = (GameObject) Instantiate (parrot, new Vector3 (x, cam.transform.position.y + Random.Range (-2.0f, 10.0f), 3), Quaternion.identity);
			newEnemy.transform.parent = GameObject.Find ("Enemies").transform;
			Destroy (newEnemy.gameObject, timeToLive);
		}

	}

	// Update is called once per frame
	void Update () {

		if (oneBird.GetStatus () != "dead") {
				score = (int)cam.transform.position.y;
				scoreTxt.text = score.ToString () + " pt";


				if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
						GenerateCloud ();
						GenerateParrot ();
						transform.Translate (oneBird.speed * Time.deltaTime);
				}


				if (Input.GetMouseButtonDown (0)) {
						if (oneBird.GetStatus () == "flying") {
								oneBird.SetStatus ("nitro");
						} else if (oneBird.GetStatus () == "stopped") {
								oneBird.SetStatus ("flying");
						}
				} else if (Input.GetMouseButtonUp (0)) {
						oneBird.SetStatus ("flying");
				}

				if (Input.GetKey ("left")) {
						if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
								oneBird.transform.Translate (-0.05f, 0, 0);
						}

				}
				if (Input.GetKey ("right")) {
						if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
								oneBird.transform.Translate (0.05f, 0, 0);
						}
				}


				if (oneBird.transform.position.y <= cam.transform.position.y - 7) {

					SetGameOver();	
				}
		}
	}

	void SetGameOver(){
		if (oneBird.GetStatus () != "dead") {
			Instantiate (gameOver, new Vector3 (0, cam.transform.position.y, 2), Quaternion.identity);
			Instantiate (tryagain, new Vector3 (0, cam.transform.position.y-1, 3), Quaternion.identity);
			oneBird.SetStatus ("dead");
		}
	}

	/*void OnGUI () {
		if (oneBird.GetStatus() == "dead") {
			Debug.Log("morto");
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if (GUI.Button (new Rect (20, 40, 80, 20), "Retry")) {
					Application.LoadLevel ("InGame");
			}
		}

	}*/
	

}
