using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
	public GameObject ggPanel;

	public void GameOver() {
		ggPanel.SetActive(true);
	}
	
	public void Replay() {
		Debug.Log("Replay!");
		Scene curScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(curScene.buildIndex);

	
	}
}
