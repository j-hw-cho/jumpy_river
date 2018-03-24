using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
	public GameObject ggPanel;
	public GameObject gamePanel;
	public Text scoreTxt;


	public void OnEnable() {
		gamePanel.SetActive(true);
		ggPanel.SetActive(false);
		scoreTxt.text = "0";

	}

	public void GameOver() {
		gamePanel.SetActive(false);
		ggPanel.SetActive(true);
	}
	
	public void Replay() {
		Debug.Log("Replay!");
		Scene curScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(curScene.buildIndex);

	
	}

	public void updateScore(int score) {
		scoreTxt.text = score.ToString();

	}
}
