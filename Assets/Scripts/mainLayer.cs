using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mainLayer : MonoBehaviour {

	private OneBird oneBird;
	private tk2dCamera cam;
	private tk2dTextMesh scoreTxt;
	private int score;

	public GameObject cloud;
	public GameObject parrot;
	public GameObject evilbird;
	public GameObject mexican;

	public GameObject gameOver;
	public GameObject tryagain;
	public GameObject wind;

	
	private int TotalEnemyAmount = 3;

	private AudioSource _inGameMusic;

	private AudioSource _windMusic;
	private AudioSource _wingFlap;
	
	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		scoreTxt = GameObject.Find ("Score").GetComponent<tk2dTextMesh> ();
		cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
		_inGameMusic = GameObject.Find ("InGameMusic").GetComponent<AudioSource> ();

		_wingFlap = GameObject.Find ("WingFlap").GetComponent<AudioSource> ();
	}
	

	private void GenerateCloud(){

		float chance = Random.Range (1.0f, 10.0f);

		if (chance >=9.88 ){
			float z = Random.Range (0.3f, 7.5f);
			GameObject newCloud = (GameObject) Instantiate (cloud, new Vector3 (Random.Range(-3.3f, 3.3f), cam.transform.position.y + 20, z), Quaternion.identity);

			string Layer;

			if (z >= 0.3 && z <= 5.14) {
				Layer =  "Layer1";
			}else if( z> 5.14 && z <= 6.1){
				Layer =  "Layer2";
			}else{
				Layer = "Layer3";
			}

			newCloud.transform.parent = GameObject.Find (Layer).transform;	
		}
	}

	private void GenerateWind(){
		float chance = Random.Range (1.0f, 10.0f);
		if ((chance >= 9.9)&&(!GameObject.Find ("WindSound(Clone)"))) {
			GameObject newWind = (GameObject) Instantiate (wind, new Vector3 (-11.16f, -1.94f, 3.17f), Quaternion.identity);
			newWind.audio.Play();
			Destroy (newWind.gameObject, 10);
		}
	}



	private void GenerateEnemy(GameObject prefab, float chance){
		float range = Random.Range (1.0f, 10.0f);
		
		if (GameObject.FindGameObjectsWithTag("Enemy").Length < TotalEnemyAmount) {
			if (range >= chance) {

				//Instatiate a new enemy
				GameObject newEnemy;
				
				//Randomize the startup position.
				int direction = (int)Random.Range (0.0f, 1.9f);
				
				//If direction is 1, then the enemy appears from the left. Else, from the Right.
				float x;
				
				if (direction == 1) {
					x = -9.5f;
				} else {
					x = 9.5f;
				}


				newEnemy = (GameObject)Instantiate (prefab, new Vector3 (x, cam.transform.position.y + Random.Range (-2.0f, 4.0f), 3), Quaternion.identity);
				newEnemy.transform.parent = GameObject.Find ("Enemies").transform;
			}
		}

	
	}


	// Update is called once per frame
	void Update () {


		if (oneBird.GetStatus () != "dead") {
				score = (int)cam.transform.position.y;
				scoreTxt.text = score.ToString () + " pt";
				GenerateWind ();

				//Management of the enemy appear and frequency.
				EnemyManagement();
				
				if (Input.GetMouseButtonDown (0)) {
						if (oneBird.GetStatus () == "flying") {
								oneBird.SetStatus ("nitro");
								_wingFlap.audio.Play ();
						} else if (oneBird.GetStatus () == "stopped") {
								oneBird.SetStatus ("nitro");
								_wingFlap.audio.Play ();
								_inGameMusic.audio.Play();
						}
				} else if (Input.GetMouseButtonUp (0)) {
						if (oneBird.GetStatus () != "stopped") {
								oneBird.SetStatus ("flying");
								_wingFlap.audio.Stop ();
						}
				}

				
				/*** THIS IS FOR PC TEST ONLY <BEGIN> ***/
				/***/if (Input.GetKey ("left")) {
				/***/		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
				/***/				oneBird.transform.Translate (-0.05f, 0, 0);
				/***/		}
				/***/
				/***/}
				/***/if (Input.GetKey ("right")) {
				/***/		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
				/***/				oneBird.transform.Translate (0.05f, 0, 0);
				/***/		}
				/***/}
				/*** THIS IS FOR PC TEST ONLY <END> ***/


				if (oneBird.transform.position.y <= cam.transform.position.y - 7) {
					//GameControl.SetGameOver();
					//StartCoroutine(GameControl.ShowTryAgain());
				}
		}
	}



	private void EnemyManagement(){
		if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
			GenerateCloud ();
			GenerateEnemy(parrot, 9.9f);
			
			transform.Translate (oneBird.speed * Time.deltaTime);
			
			
			if (score >=101)
				GenerateEnemy(evilbird, 9.95f);
			
			if (score >=250)
				if(!GameObject.Find ("MexicanSound(Clone)"))
					GenerateEnemy(mexican, 9.9f);
				TotalEnemyAmount = 7;
		}
	}


}
