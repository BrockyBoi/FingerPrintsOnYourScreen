using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {
    public Canvas menu;

    public Button play;
    public Button fireworks;
    public Button baseGame;
    public Button easy;
    public Button medium;
    public Button hard;
    public Button back;

    //Gamemodes
    bool fire;
    bool reg;

    //Differnt possible pages
    bool mainMenu;
    bool gameMode;
    bool difficultyF;
    bool difficultyR;

	// Use this for initialization
	void Start () {
        menu = menu.GetComponent<Canvas>();
 
        play = play.GetComponent<Button>();
        easy = easy.GetComponent<Button>();
        medium = medium.GetComponent<Button>();
        hard = hard.GetComponent<Button>();
        fireworks = fireworks.GetComponent<Button>();
        baseGame = baseGame.GetComponent<Button>();
        back = back.GetComponent<Button>();

        //Initializes that there should only be a play button in beginning
        mainMenu = true;
        difficultyF = false;
        difficultyR = false;
        gameMode = false;

        play.enabled = true;
        easy.enabled = false;
        medium.enabled = false;
        hard.enabled = false;
        fireworks.enabled = false;
        baseGame.enabled = false;
        back.enabled = false;
        reg = false;
        fire = false;
    }

    void Update()
    {
        display();
    }

    public void display()
    {
        if(mainMenu)
        {
            difficultyF = false;
            difficultyR = false;
            gameMode = false;

            active(play);

            disable(easy);
            disable(medium);
            disable(hard);
            disable(baseGame);
            disable(fireworks);
            disable(back);


        }
        if(gameMode)
        {
            difficultyF = false;
            difficultyR = false;
            mainMenu = false;

            disable(play);
            disable(easy);
            disable(medium);
            disable(hard);

            active(baseGame);
            active(fireworks);
            active(back);

        }
        if(difficultyF)
        {
            fire = true;
            reg = false;

            active(easy);
            active(medium);
            active(hard);

            disable(baseGame);
            disable(fireworks);
        }
        if(difficultyR)
        {
            reg = true;
            fire = false;

            active(easy);
            active(medium);
            active(hard);

            disable(baseGame);
            disable(fireworks);
        }
    }

    public void pressBack()
    {
        if (difficultyF || difficultyR)
        {
            reg = false;
            fire = false;

            difficultyF = false;
            difficultyR = false;
            gameMode = true;
        }
        else if (gameMode)
        {
            gameMode = false;
            mainMenu = true;
        }
    }

    public void pressPlay()
    {
        mainMenu = false;
        gameMode = true;
        Debug.Log("pressed play");
    }

    public void pressReg()
    {
        gameMode = false;
        difficultyR = true;
        Debug.Log("pressed reg");
    }

    public void pressFire()
    {
        gameMode = false;
        difficultyF = true;
        Debug.Log("pressed fire");
    }

    public void active(Button b)
    {
        b.gameObject.SetActive(true);
        b.enabled = true;
    }

    public void disable(Button b)
    {
        b.gameObject.SetActive(false);
        b.enabled = false;
    }

    public void pressEasy()
    {
        if(reg)
        {
            Application.LoadLevel("E Base Game");
        }
        else
        {
            Application.LoadLevel("E Fireworks");
        }
    }

    public void pressMedium()
    {
        if (reg)
        {
            Application.LoadLevel("M Base Game");
        }
        else
        {
            Application.LoadLevel("M Fireworks");
        }
    }

    public void pressHard()
    {
        if (reg)
        {
            Application.LoadLevel("H Base Game");
        }
        else
        {
            Application.LoadLevel("H Fireworks");
        }
    }
}
