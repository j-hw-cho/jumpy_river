using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	MeshRenderer arrowMR;
	bool isEnabled;
	bool pause;
	bool minus;

	float unitAngle;
	Vector3 curAngle;

	// Use this for initialization
	void OnEnable () {
		arrowMR = this.gameObject.GetComponent<MeshRenderer>();
		isEnabled = true;
		pause = false;

		minus = true;
		this.gameObject.transform.eulerAngles = Vector3.zero;
		curAngle = this.gameObject.transform.eulerAngles;

		unitAngle = 1;
	}

	
	// Update is called once per frame
	void Update () {
		if (isEnabled && !pause) {
			Vector3 newAngle = Vector3.zero;
			if (minus) {
				newAngle = new Vector3(curAngle.x - unitAngle, 0, 0);
				if (newAngle.x <= -70f) {
					minus = false;
				}
			} else {
				newAngle = new Vector3(curAngle.x + unitAngle, 0, 0);
				if (newAngle.x >= 0) {
					minus = true;
				}
			}
			this.transform.eulerAngles = newAngle;
			curAngle = newAngle;
		}
	}

	void disable() {
		arrowMR.enabled = false;
		isEnabled = false;

	}


	void enable() {
		arrowMR.enabled = true;
		this.transform.eulerAngles = Vector3.zero;
		isEnabled = true;

	}

	void pauseArrow() {
		pause = true;
	}


}
