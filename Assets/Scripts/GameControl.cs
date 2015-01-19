using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameControl  {

	public static OneBird oneBird;
	public static AudioSource _inGameMusic;
	public static AudioSource _gameOverMusic;
	public static AudioSource _wingFlap;
	public static tk2dTextMesh scoreTxt;
	public static tk2dCamera cam;
	public static tk2dUIManager uimanager;

	public static GameObject GameOver;

	public static void SetGameOver(){
		oneBird = GameObject.Find ("OneBird").GetComponent<OneBird> ();
		_inGameMusic = GameObject.Find ("InGameMusic").GetComponent<AudioSource> ();
		_gameOverMusic = GameObject.Find ("GameOverMusic").GetComponent<AudioSource> ();
		_wingFlap = GameObject.Find ("WingFlap").GetComponent<AudioSource> ();
		scoreTxt = GameObject.Find ("Score").GetComponent<tk2dTextMesh> ();
		cam = GameObject.Find ("tk2dCamera").GetComponent<tk2dCamera> ();
		uimanager = GameObject.Find ("tk2dUIManager").GetComponent<tk2dUIManager> ();

		if (oneBird.GetStatus () != "dead") {
			
			oneBird.SetStatus ("dead");
			
			_inGameMusic.audio.Stop();
			_gameOverMusic.audio.Play ();
			
			if(_wingFlap.audio.isPlaying){
				_wingFlap.audio.Stop ();
			}


			scoreTxt.transform.localScale = new Vector3(2.0f,2.0f,2.0f);
			scoreTxt.anchor = TextAnchor.MiddleCenter;
			scoreTxt.transform.position = new Vector3(0f, cam.transform.position.y+1.3f, 0.33f);

			MonoBehaviour.Instantiate(Resources.Load ("GameOver", typeof(GameObject)), new Vector3 (0, cam.transform.position.y, 2), Quaternion.identity);
		}
	}


	public static IEnumerator ShowTryAgain () {
		yield return new WaitForSeconds(5.0f);
		MonoBehaviour.Instantiate(Resources.Load ("TryAgain", typeof(GameObject)), new Vector3 (0, cam.transform.position.y-1, 2), Quaternion.identity);
	}
}
