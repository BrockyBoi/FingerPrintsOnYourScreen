using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseScript : MonoBehaviour {
    public Canvas pauseCanvas;
    public Canvas pauseScreen;
    public Button resume;
    public Button quit;

	// Use this for initialization
	void Start () {
        resume = resume.GetComponent<Button>();
        quit = quit.GetComponent<Button>();
        pauseScreen.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pressPause()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        GameControllerScript.paused = true;
        FireworkControllerScript.paused = true;
    }

    public void pressResume()
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
        GameControllerScript.paused = false;
        FireworkControllerScript.paused = false;
    }

    public void pressQuit()
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
        GameControllerScript.paused = false;
        FireworkControllerScript.paused = false;
        Application.LoadLevel("Main Menu");
    }

}
