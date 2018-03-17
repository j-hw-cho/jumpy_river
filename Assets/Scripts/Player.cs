using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Animator playerAnimator;

	private bool jumpReady;
	private bool isJumping;

	// Use this for initialization
	void OnEnable () {
		playerAnimator = this.gameObject.GetComponent<Animator>();
		playerAnimator.SetBool("touch", false);
		playerAnimator.SetBool("jump", false);

		jumpReady = false;
		isJumping = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			playerAnimator.SetBool("touch", true);
			jumpReady = true;
			Debug.Log("READY");
		}

		if (jumpReady) {
			if (Input.GetMouseButtonUp(0)) {
				playerAnimator.SetBool("jump", true);
				isJumping = true;
				Debug.Log("JUMP");
			}
		} 

		if (isJumping) {
			if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("basic")) {
				// Jump animation finished
				playerAnimator.SetBool("jump", false);
				playerAnimator.SetBool("touch", false);
				isJumping = false;
				jumpReady = false;
				Debug.Log("Animation Finished");

			}
		}

	}
}
