﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gamemode : MonoBehaviour {

  public bool DisplayMouse = true;

  private bool gameOverOn = false;
  private int winningPlayerNumber = 0;
  private bool gameStart = false;
  private string readyTime = "";

  void Start() {
    Cursor.visible = DisplayMouse;
    StartCoroutine(displayCountDown());
  }

  // Update is called once per frame
  void Update() {
    if (gameOverOn) { return; }
    PlayerController[] players = FindObjectsOfType(typeof(PlayerController)) as PlayerController[];
    if (players.Length == 1) {
      gameOverOn = true;
      winningPlayerNumber = players[0].PlayerNumber;
    } else if (players.Length == 0) {
      gameOverOn = true;
    }
  }

  void OnGUI() {

    if (readyTime != "")
    {
      GUI.Box(new Rect((Screen.width / 2) - 25, (Screen.height / 2) - 25, 50, 50), readyTime);
    }
    if (gameOverOn) {
      displayGameOverGUI();
    }
  }

  // Display who won and a button to restart the level.
  private void displayGameOverGUI() {
    string winText = "Game Over. ";
    if (winningPlayerNumber != 0) {
      winText += "Player " + winningPlayerNumber + " wins!";
    } else {
      winText += "The game ended in a tie!";
    }
    winText += "\nPress \'Start\' to Restart";

    GUI.Box(new Rect(0, 0, Screen.width, Screen.height), winText);
    if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "Restart") || Input.GetAxis("KB_Pause") != 0 || Input.GetAxis("XBOX_Pause") != 0 || Input.GetAxis("PS4_Pause") != 0) {
      gameOverOn = false;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } else if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 250, 200, 100), "Level Select")) {
      gameOverOn = false;
      SceneManager.LoadScene("levelSelect");
    }
  }
  // Display a countdown timer before game start
  IEnumerator displayCountDown()
  {


    readyTime = "3";
    yield return new WaitForSeconds(1);
    readyTime = "2";
    yield return new WaitForSeconds(1);
    readyTime = "1";
    yield return new WaitForSeconds(1);
    readyTime = "GO!";
    yield return new WaitForSeconds(1);
    gameStart = true;
    readyTime = "";
    // display text, can be replaced by sprites for better visual

  }
}
