using UnityEngine;
using System.Collections;

public class OneBird : MonoBehaviour {

	public Vector3 speed;
	private float _nitro = 0;
	private string _status = "stopped";
	private tk2dCamera mainCamera;
	public GameObject gameOver;
	public GameObject tryagain;

	private tk2dTextMesh scoreTxt;


	private AudioSource _inGameMusic;
	private AudioSource _gameOverMusic;
	private AudioSource _wingFlap;

	private mainLayer _uiManager;

	tk2dSpriteAnimator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<tk2dSpriteAnimator> ();
		mainCamera = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
		_status = "stopped";

		scoreTxt = GameObject.Find ("Score").GetComponent<tk2dTextMesh> ();

		_inGameMusic = GameObject.Find ("InGameMusic").GetComponent<AudioSource> ();
		_gameOverMusic = GameObject.Find ("GameOverMusic").GetComponent<AudioSource> ();
		_wingFlap = GameObject.Find ("WingFlap").GetComponent<AudioSource> ();

		_uiManager = GameObject.Find ("tk2dUIManager").GetComponent<mainLayer> ();
	}

	public string GetStatus(){
		return _status;
	}
	public void SetStatus(string status){
		_status = status;
	}

	
	// Update is called once per frame
	void Update () {

		if (_status == "flying") {
			_nitro = -3;
			animator.Play ("OneBirdFlying");
		} else if (_status == "nitro") {
			_nitro = 3;
			animator.Play ("OneBirdNitro");
		} else if (_status == "dead") {
			animator.Play ("OneBirdDead");
		}

		if ((_status != "stopped")&&(_status != "dead")) {
			transform.Translate (Input.acceleration.x, (speed.y + _nitro) * Time.deltaTime, 0);
			CheckPosition ();
		}
	}

	void OnCollisionEnter(Collision collision){
		if ((collision.gameObject.tag == "Enemy") && (_status != "dead")) {
			/*_status = "dead";
			animator.Play("OneBirdDead");

			_inGameMusic.audio.Stop();
			_gameOverMusic.audio.Play ();

			scoreTxt.transform.localScale = new Vector3(2.0f,2.0f,2.0f);
			scoreTxt.anchor = TextAnchor.MiddleCenter;
			scoreTxt.transform.position = new Vector3(0f, mainCamera.transform.position.y+1.3f, 0.33f);



			Instantiate(gameOver, new Vector3 (0, mainCamera.transform.position.y, 2), Quaternion.identity);

	
			StartCoroutine(ShowTryAgain());*/

			if(_wingFlap.audio.isPlaying){
				_wingFlap.audio.Stop ();
			}

			this.rigidbody.constraints =  RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
			collision.gameObject.rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

			_uiManager.SetGameOver();

		}

	}


	IEnumerator ShowTryAgain () {
		yield return new WaitForSeconds(5.0f);
		Instantiate (tryagain, new Vector3 (0, mainCamera.transform.position.y-1, 2), Quaternion.identity);
	}


	private void CheckPosition(){
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, mainCamera.transform.position.x-2.6f, mainCamera.transform.position.x+2.6f), Mathf.Clamp(transform.position.y, mainCamera.transform.position.y-8f, mainCamera.transform.position.y+4.6f), transform.position.z);
	}

}
