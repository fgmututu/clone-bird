using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	static int score = 0;
	static int highScore = 0;

	static Score instance;

	GUIText text;

	BirdMovement bird;

	void Start(){
		instance = this;

		text = GetComponent<GUIText> ();

		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");
		if (player_go == null) {
			Debug.LogError ("Could not find an object with tag 'Player'.");
		}
		bird = player_go.GetComponent<BirdMovement> ();
	
		score = 0;

		highScore = PlayerPrefs.GetInt ("highScore", 0);
	}

	static public void AddPoint() {
		if (instance.bird.dead)
			return;

		score++;

		if (score > highScore) {
			highScore = score;
		}
	}

	void OnDestroy() {
		instance = null;
		PlayerPrefs.SetInt ("highScore", highScore);
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Score: " + score + "\n High Score: " + highScore;
	}
}
