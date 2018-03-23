using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool active;
	public Arrow arrow;

	private Animator playerAnimator;

	private Rigidbody playerRB;

	private bool jumpReady;
	private bool isJumping;

	public float unit;

	// Use this for initialization
	void OnEnable () {
		playerRB = this.gameObject.GetComponent<Rigidbody>();
		playerAnimator = this.gameObject.GetComponent<Animator>();
		playerAnimator.SetBool("touch", false);
		playerAnimator.SetBool("jump", false);

		jumpReady = false;
		isJumping = false;

		unit = 2500f;
		active = true;

	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			if (!jumpReady && !isJumping) {
				if (Input.GetMouseButtonDown(0)) {
					playerAnimator.SetBool("touch", true);
					jumpReady = true;
					arrow.pauseArrow();
					Debug.Log("READY");

				}
			}
			if (jumpReady) {
				if (Input.GetMouseButtonUp(0)) {
					playerAnimator.SetBool("jump", true);
					isJumping = true;
					arrow.pauseBar();

					/* calculate power and add player motion */
					float rad = arrow.returnForceAngle();
					float tan = Mathf.Tan(rad);
					Debug.Log("Tan: " + tan.ToString());
					float power = arrow.returnForceAmt();

					Debug.Log("rad = " + rad.ToString() + " power: " +  power.ToString());

					float forceZ = power* unit;
					float forceY = tan * forceZ;

					Vector3 newForce = new Vector3(0, forceY, forceZ);
					Debug.Log("newForce: " + newForce.ToString());
					playerRB.AddForce(newForce);


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
					arrow.enable();
					Debug.Log("Animation Finished");

				}
			}
		}

	}
}
