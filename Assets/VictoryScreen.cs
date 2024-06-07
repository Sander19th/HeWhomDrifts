using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
  public void setup(){
	  gameObject.SetActive(true);
	  Debug.Log("Victory Screen is called and activated");
  }

  public void ReplayButton(){
	  SceneManager.LoadScene("TheGame");
  }

  public void ExitButton(){
	  Application.Quit();
  }
}
