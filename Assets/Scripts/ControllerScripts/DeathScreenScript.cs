using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathScreenScript : MonoBehaviour {
    public Canvas screen;
    public Text score;
    public Button yes;
    public Button no;

	// Use this for initialization
	void Start () {
       // screen = GetComponent<Canvas>();
        yes = GetComponent<Button>();
        no = GetComponent<Button>();
	}

    void Update()
    {
        score.text = "Your Score: " + GameControllerScript.score.ToString();
    }
	
	public void pressYes()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void pressNo()
    {
        Application.LoadLevel("Main Menu");
    }
}
