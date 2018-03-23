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

	public const float moveBound = 3.0f;
	public const float moveUnit = 0.2f;

	
	// Update is called once per frame
	void Update () {
		if (myType == Environment.dolType.movedol) {
			if (isPlus) {
				this.gameObject.transform.position = new Vector3(myInitPos.x, myInitPos.y, myInitPos.z + moveUnit);
				if (this.gameObject.transform.position.z >= myMaxVal) 
					isPlus = false;

			} else {
				this.gameObject.transform.position = new Vector3 (myInitPos.x, myInitPos.y, myInitPos.z - moveUnit);
				if (this.gameObject.transform.position.z <= myMinVal) 
					isPlus = true;
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
		}

		isPlus = true;
	}

	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Player") {
			//Debug.Log("[Dol " + dolId + "] PLAYER CONTACTED");
			if (col.collider.GetType() == typeof(BoxCollider)) {
				Debug.Log("Player Landed on dol " + dolId.ToString());
				GameObject.Find("water").GetComponent<Environment>().UpdatePlayerPos(dolId);

			}
		}

	}
}
