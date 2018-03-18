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

	/** power bar **/
	public GameObject powerBar;
	public GameObject powerSlider;
	bool isPowerBarEnabled;
	bool addPower;
	bool stopPower;

	float scaleUnit;



	// Use this for initialization
	void OnEnable () {
		arrowMR = this.gameObject.GetComponent<MeshRenderer>();
		isEnabled = true;
		pause = false;

		minus = true;
		this.gameObject.transform.eulerAngles = Vector3.zero;
		curAngle = this.gameObject.transform.eulerAngles;

		unitAngle = 1;

		powerBar.SetActive(false);
		isPowerBarEnabled = false;
		addPower = true;
		stopPower = false;
		scaleUnit = 2;
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

		if (isPowerBarEnabled && !stopPower) {
			
			Vector3 sliderScale = powerSlider.transform.localScale;
			Vector3 newScale = Vector3.zero;
			if (addPower) {
				newScale = new Vector3(sliderScale.x, sliderScale.y, sliderScale.z + 0.01f * scaleUnit);
				if (newScale.z >= 0.99f) {
					addPower = false;
				}
			} else {
				newScale = new Vector3(sliderScale.x, sliderScale.y, sliderScale.z - 0.01f * scaleUnit);
				if (newScale.z <= 0.01f) {
					addPower = true;
				}

			}
			powerSlider.transform.localScale = newScale;
		}
	}

	public void disable() {
		arrowMR.enabled = false;
		isPowerBarEnabled = false;
		powerBar.SetActive(false);
		isEnabled = false;

	}


	public void enable() {
		this.transform.eulerAngles = new Vector3(0, 0, 0);
		arrowMR.enabled = true;
		pause = false;
		isEnabled = true;

	}

	public void pauseArrow() {
		pause = true;
		powerBar.SetActive(true);
		stopPower = false;
		Vector3 sc = powerSlider.transform.localScale;
		powerSlider.transform.localScale = new Vector3(sc.x, sc.y, 0.01f);
		addPower = true;
		isPowerBarEnabled = true;
	}

	public void pauseBar() {
		stopPower = true;

		IEnumerator coroutine = waitAndDisable();
		StartCoroutine(coroutine);
	}

	IEnumerator waitAndDisable() {
		yield return new WaitForSeconds(0.2f);

		disable();
	}
}
