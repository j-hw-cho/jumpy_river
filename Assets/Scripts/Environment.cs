using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
	public int score;
	public UI ui;
	public Player player;

	public GameObject dolPf;	// Dol prefab
	public GameObject woodPf;	// Wood prefab (sinking dol)
	public GameObject moveDolPf;	// Moving Dol prefab

	private int nextDolId;

	private List<GameObject> dols;

	private int dolCount;
	private Vector3 lastDolPos;

	private int playerPos;

	private const float dolY = -1.5f;

	public enum dolType {
		dol,
		wood, 
		movedol
	};

	// Use this for initialization
	void OnEnable () {
		score = 0;
		ui = GameObject.Find("Canvas").GetComponent<UI>();
		dols = new List<GameObject>();
		dolCount = 0;
		lastDolPos = Vector3.zero;
		playerPos = 0;

		nextDolId = 1;

		// first, generate 3 dols 
		for (int i = 0; i < 3; i++) {
			GenerateDol (2);	// For testing
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
			newDol.GetComponent<Dol>().initialize(nextDolId);
			nextDolId++;
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


	public void UpdatePlayerPos(int dolId) {
		if (playerPos != dolId) {
			player.landOnDol();

			// Score calculation
			int dolDiff = dolId - playerPos;
			if (dolDiff == 1) {
				score += 10;
			} else {
				int scoreAdd = 10 * dolDiff * 2;
				score += scoreAdd;
			}

			playerPos = dolId;
			ui.updateScore(score);
			// Generate New Dol
			if (playerPos <= 10) {
				GenerateDol(0);
			
			} else if (playerPos <= 20) {
				// Todo: after first 20 dols, generate wood and moving dol randomly
				int range = Random.Range(0,2);
				Debug.Log("new Type == " + range);
				GenerateDol(range);

			} else {
				int range = Random.Range(0,3);
				Debug.Log("new Type == " + range);
				GenerateDol(range);
			}

			// Delete oldest dol if needed
			if (playerPos >= 3) {
				DestroyDol();
			}
		}
	}


	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Player") {
			//Debug.Log("Player sinked!");
			player.active = false;
			ui.GameOver();
		}
	}
}
