using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
  public void setup(){
	  gameObject.SetActive(true);
	  Debug.Log("Game Over Screen is called and activated");
  }

  public void RetryButton(){
	  SceneManager.LoadScene("Version 1.0.2");
  }

  public void QuitButton(){
	  Application.Quit();
  }
}
