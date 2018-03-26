using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dol : MonoBehaviour {
	public int dolId;
	public Environment.dolType myType;
	public Vector3 myInitPos;

	/* boundary values for moving rocks */
	public float myMinVal;
	public float myMaxVal;
	bool isPlus;

	bool falling;

	bool isPlayerOn;

	public const float moveBound = 3.0f;
	public const float moveUnit = 0.05f;

	
	// Update is called once per frame
	void Update () {
		if (myType == Environment.dolType.movedol) {
			Vector3 curPos = this.gameObject.transform.position;
			if (isPlus) {
				this.gameObject.transform.position = new Vector3(curPos.x, curPos.y, curPos.z + moveUnit);
				if (this.gameObject.transform.position.z >= myMaxVal) 
					isPlus = false;

			} else {
				this.gameObject.transform.position = new Vector3 (curPos.x, curPos.y, curPos.z - moveUnit);
				if (this.gameObject.transform.position.z <= myMinVal) 
					isPlus = true;
			}
		}

		if (isPlayerOn && myType == Environment.dolType.wood) {
			if (falling) {
				Vector3 curPos = this.gameObject.transform.position;
				if (curPos.y > myMinVal) {
					this.gameObject.transform.position = new Vector3(curPos.x, curPos.y - moveUnit, curPos.z);
				}
			} else {
				IEnumerator wait = waitBeforeFall();
				StartCoroutine(wait);
			}
		}
	}

	// To be called from Environment when created
	public void initialize(int id) {
		dolId = id;
		myInitPos = this.gameObject.transform.position;
		if (myType == Environment.dolType.movedol) {
			myMinVal = myInitPos.z - moveBound;
			myMaxVal = myInitPos.z + moveBound;
		} else if (myType == Environment.dolType.wood) {
			myMaxVal = myInitPos.y;
			myMinVal = -10f;

		}

		isPlus = true;
		isPlayerOn = false;
		falling = false;
	}

	IEnumerator waitBeforeFall() {
		yield return new WaitForSeconds(3f);

		falling = true;
	}


	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			//Debug.Log("[Dol " + dolId + "] PLAYER CONTACTED");
			if (col.collider.GetType() == typeof(BoxCollider)) {
				isPlayerOn = true;
				Debug.Log("Player Landed on dol " + dolId.ToString());
				GameObject.Find("water").GetComponent<Environment>().UpdatePlayerPos(dolId);

			}
		} 

	}

	public void OnCollisionExit(Collision col) {
		if (col.gameObject.tag == "Player") {
			if (col.collider.GetType() == typeof(BoxCollider)) {
				isPlayerOn = false;

			}
		}

	}


}
