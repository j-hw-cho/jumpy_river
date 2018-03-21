using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
	public GameObject dolPf;	// Dol prefab
	public GameObject woodPf;	// Wood prefab (sinking dol)
	public GameObject moveDolPf;	// Moving Dol prefab

	private List<GameObject> dols;

	private int dolCount;
	private Vector3 lastDolPos;

	private int playerPos;

	private const float dolY = -1.5f;

	// Use this for initialization
	void OnEnable () {
		dols = new List<GameObject>();
		dolCount = 0;
		lastDolPos = Vector3.zero;
		playerPos = 0;


		// first, generate 3 dols 
		for (int i = 0; i < 3; i++) {
			GenerateDol (0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/* Generate new dol as player moves 
	 * add new dol at the end of the list 
	 */
	void GenerateDol(int option) {
		GameObject newDol;
		switch (option){
			case 1:
				newDol = (GameObject)Instantiate(woodPf);
				break;
			case 2:
				newDol = (GameObject)Instantiate(moveDolPf);
				break;
			default:
				newDol = (GameObject)Instantiate(dolPf);
				break;
		}

		if (newDol != null) {
			if (dolCount == 0) { // first dol
				newDol.transform.position = new Vector3(0f, dolY, 10f);
			} else {
				float newDolZ = lastDolPos.z + Random.Range(7f, 14f);
				newDol.transform.position = new Vector3(0f, dolY, newDolZ);


			}
			dols.Add(newDol);
			lastDolPos = newDol.transform.position;
			dolCount++;
		}
	}


	/* Destroys past dols 
	 * Destroys first dol in the list
	 */

	void DestroyDol() {
		GameObject doneDol = dols[0];
		dols.RemoveAt(0);

		Destroy(doneDol);

	}
}
