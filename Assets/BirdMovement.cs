using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour {

//	Vector3 velocity = Vector3.zero;
	public float flapSpeed    = 150f;
	float forwardSpeed = 1f;

	bool didFlap = false;
	Rigidbody2D player;
	Animator animator;

	public bool dead = false;
	float deathCooldown;
	string sceneName;

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		player = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponentInChildren<Animator> ();
		sceneName = SceneManager.GetActiveScene ().name;
	}

	// Do Graphic & Input updates here
	void Update() {
		if (dead) {
			deathCooldown -= Time.deltaTime;

			if (deathCooldown <= 0) {
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
					SceneManager.LoadScene (sceneName, LoadSceneMode.Single);
				}
			}
		} else {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) {
				didFlap = true;
			}
		}
	}
	
	// Do physics engine updates here
	void FixedUpdate () {

		if (dead)
			return;
		
		player.AddForce ( Vector2.right * forwardSpeed );

		if (didFlap) {
			player.AddForce ( Vector2.up * flapSpeed );

			animator.SetTrigger ("DoFlap");
			didFlap = false;
		}

		if (player.velocity.y > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}
		else {
			float angle = Mathf.Lerp (0, -90, (-player.velocity.y / 3f));
			transform.rotation = Quaternion.Euler (0, 0, angle);

		}

	}

	void OnCollisionEnter2D(Collision2D collision){
		if (godMode)
			return;
		
		animator.SetTrigger ("Death");
		dead = true;
		deathCooldown = 0.5f;
	}
}
