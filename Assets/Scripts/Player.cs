using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private Animator playerAnimator;

	// Use this for initialization
	void Start () {
		playerAnimator = this.gameObject.GetComponent<Animator>();
		playerAnimator.SetBool("touch", false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			playerAnimator.SetBool("touch", true);

		}
	}
}
