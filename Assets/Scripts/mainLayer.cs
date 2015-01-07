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
	public GameObject wind;

	private AudioSource _inGameMusic;
	private AudioSource _gameOverMusic;
	private AudioSource _windMusic;
	private AudioSource _wingFlap;
	
	// Use this for initialization
	void Start () {
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		scoreTxt = GameObject.Find ("Score").GetComponent<tk2dTextMesh> ();
		cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
		_inGameMusic = GameObject.Find ("InGameMusic").GetComponent<AudioSource> ();
		_gameOverMusic = GameObject.Find ("GameOverMusic").GetComponent<AudioSource> ();
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

	private void GenerateParrot(){
		float chance = Random.Range (1.0f, 10.0f);
		if (chance >= 9.9) {//if (chance >= 9.97) {


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

		}

	}

	// Update is called once per frame
	void Update () {



		if (oneBird.GetStatus () != "dead") {
				score = (int)cam.transform.position.y;
				scoreTxt.text = score.ToString () + " pt";
				GenerateWind ();

				if ((oneBird.GetStatus () == "flying") || (oneBird.GetStatus () == "nitro")) {
						GenerateCloud ();
						//GenerateParrot ();
						transform.Translate (oneBird.speed * Time.deltaTime);
				}


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
						oneBird.SetStatus ("flying");
						_wingFlap.audio.Stop ();
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

			_inGameMusic.audio.Stop();
			_gameOverMusic.audio.Play ();

			if(_wingFlap.audio.isPlaying){
				_wingFlap.audio.Stop ();
			}

			scoreTxt.transform.localScale = new Vector3(2.0f,2.0f,2.0f);
			scoreTxt.anchor = TextAnchor.MiddleCenter;
			scoreTxt.transform.position = new Vector3(0f, cam.transform.position.y+1.3f, 0.33f);

			Instantiate (gameOver, new Vector3 (0, cam.transform.position.y, 2), Quaternion.identity);
			oneBird.SetStatus ("dead");

	
			StartCoroutine(ShowTryAgain());

		}
	}
	
	IEnumerator ShowTryAgain () {
		yield return new WaitForSeconds(5.0f);
		Instantiate (tryagain, new Vector3 (0, cam.transform.position.y-1, 2), Quaternion.identity);
	}

}
