using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class FireworkControllerScript : MonoBehaviour {
    public static FireworkControllerScript control;

    //Score System
    public int highScore;
    public static int score;
    public static int scoreIncrement;

    //Text
    public Canvas deathCanvas;
    public Text highScoreText;
    public Text healthText;
    public Text bossHealth;
    public Text scoreText;

    //Spawn system
    float currentTime;
    float spawnTime;
    public static float spawnRate;

    //Fireworks
    public GameObject standardFirework;
    public GameObject fireworkExplosion;

    //Health system
    public static bool dead;
    public static int fireworkHealth;
    public static float health;

    //Variables given to fireworks
    public static float flySpeed;
    public static float bossFlySpeed;
    public static bool paused;

    //Arraylist of all fireworks currently in the game
    List<FireworkScript> fireworkList;

    //Round system
    bool bossRound;
    int round;

    //Difficulty System
    public float nextDifficulty;
    float easyDifficulty;
    float mediumDifficulty;
    float hardDifficulty;
    bool easy;
    bool medium;
    bool hard;
    public int difficultyLevel;

    FireworkScript fireworkScript;


    // Use this for initialization
    void Start () {
        //set up static controller
        if (control == null)
            control = this;
        else
            Destroy(this);

        //Beginning health
        health = 5;
        dead = false;

        //Score system
        score = 0;
        scoreIncrement = 10;

        getHighScore();

        //First round / difficulty
        bossRound = false;
        round = 1;
        difficultyLevel = 1;

        //Difficulty settings
        easyDifficulty = 15;
        mediumDifficulty = 10;
        hardDifficulty = 6;

        easy = false;
        medium = false;
        hard = false;

        difficultySetting();

        //Spawn rate variables
        spawnRate = 1f;
        spawnTime = Time.time + spawnRate;

        //Firework variables
        flySpeed = 9;
        fireworkHealth = 1;
        paused = false;

        //Boss variables
        bossFlySpeed = 1;

        //Text
        healthText.text = "Health: " + health.ToString();
        bossHealth.text = "";

        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

        paused = false;
    }
	
	// Update is called once per frame
	void Update () {

        displayHealth();

        displayScore();

        spawnSystem();

        difficulty();
	}

    void spawnSystem()
    {
        if (Time.time >= spawnTime && !dead)
        {
            if (!bossRound)
            {
                spawnTime = Time.time + spawnRate;
                spawnRegular();
            }
            //if (round % 4 == 0)
            //{
            //    bossRound = true;
            //    //bossSpawner();
            //}
        }
        //ifBeatBoss();
    }

    void difficulty()
    {
        if (Time.time >= nextDifficulty && !bossRound)
        {
            difficultyLevel++;
            round++;
            //if (round % 4 == 0)
            //{
            //    spawnTime = Time.time + 60;
            //    nextDifficulty = Time.time + 60;
            //    bossRound = true;
            //    //bossSpawner();
            //}
            //else
            //{
                difficultySetting();
            //}

            //How long it takes for controller to create new firework
            if (spawnRate > .5f)
            {
                spawnRate -= .1f;
            }
            else
            {
                spawnRate -= .02f;
            }

            if(flySpeed > 25f)
            {
                flySpeed += .5f;
            }
            else
            {
                flySpeed += 1.75f;
            }

            scoreIncrement += 5;

            //New variables are set for blocks
            fireworkScript.newVariables(flySpeed, bossFlySpeed, fireworkHealth);
        }
    }


    public void displayHealth()
    {
        if (health > 0)
        {
            deathCanvas.gameObject.SetActive(false);
            healthText.text = "Health: " + health.ToString();
        }
        else
        {
            dead = true;
            deathCanvas.gameObject.SetActive(true);
            healthText.text = "Deaderinio";
        }
    }

    public void displayScore()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

        setHighScore();
    }

    public void getHighScore()
    {
        if (easy)
        {
            highScore = PlayerPrefs.GetInt("F HighScore Easy");
        }
        else if (medium)
        {
            highScore = PlayerPrefs.GetInt("F HighScore Medium");
        }
        else if (hard)
        {
            highScore = PlayerPrefs.GetInt("F HighScore Hard");
        }
    }

    public void setHighScore()
    {
        if (easy)
        {
            PlayerPrefs.SetInt("F HighScore Easy", highScore);
        }
        else if (medium)
        {
            PlayerPrefs.SetInt("F HighScore Medium", highScore);
        }
        else if (hard)
        {
            PlayerPrefs.SetInt("F HighScore Hard", highScore);
        }
    }

    public void difficultySetting()
    {

        if (tag == "F Easy Controller")
        {
            easy = true;
            nextDifficulty = Time.time + easyDifficulty;
        }
        else if (tag == "F Medium Controller")
        {
            medium = true;
            nextDifficulty = Time.time + mediumDifficulty;
        }
        else if (tag == "F Hard Controller")
        {
            hard = true;
            nextDifficulty = Time.time + hardDifficulty;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public static void incrementScore(int num)
    {
        score += num;
    }

    //List of spawning methods
    void spawnRegular()
    {
        float angle;
        int spawnPosX = Random.Range(-25, 25);
        if(spawnPosX < 0)
        {
            angle = Random.Range(-10, -50);
        }
        else
        {
            angle = Random.Range(10, 50);
        }
        GameObject obj = (GameObject)Instantiate(standardFirework, new Vector3(spawnPosX, -20), Quaternion.identity);
        obj.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, angle);
    }

}
