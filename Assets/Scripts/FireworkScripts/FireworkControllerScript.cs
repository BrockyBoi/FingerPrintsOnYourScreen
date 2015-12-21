using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class FireworkControllerScript : MonoBehaviour {

    //Score System
    public int highScore;
    public static int score;
    public static int scoreIncrement;

    ////Text
    //public Canvas deathCanvas;
    //public Text highScoreText;
    //public Text healthText;
    //public Text bossHealth;
    //public Text scoreText;

    //Spawn system
    float currentTime;
    float spawnTime;
    public static float spawnRate;

    //Fireworks
    public GameObject standardFirework;

    //Health system
    public static bool dead;
    public static int fireworkHealth;
    public static float health;

    //Variables given to fireworks
    public static float flySpeed;
    public static float bossFlySpeed;

    //Arraylist of all fireworks currently in the game
    List<FireworkScript> fireworkList;

    //Round system
    bool bossRound;
    int round;

    //Difficulty System
    public float nextDifficulty;
    public int difficultyLevel;

    FireworkScript fireworkScript = new FireworkScript();


    // Use this for initialization
    void Start () {
        //Beginning health
        health = 5;
        dead = false;

        //Score system
        score = 0;
        scoreIncrement = 10;

        if (PlayerPrefs.GetInt("HighScore") != null)
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        //First round / difficulty
        bossRound = false;
        round = 1;
        difficultyLevel = 1;

        //Time when next difficulty curve happens
        nextDifficulty = Time.time + 15;

        //Spawn rate variables
        spawnRate = 1f;
        spawnTime = Time.time + spawnRate;

        //Firework variables
        flySpeed = 9;
        fireworkHealth = 1;

        //Boss variables
        bossFlySpeed = 1;

        ////Text
        //healthText.text = "Health: " + health.ToString();
        //bossHealth.text = "";

        //scoreText.text = "Score: " + score.ToString();
        //highScoreText.text = "High Score: " + highScore.ToString();
    }
	
	// Update is called once per frame
	void Update () {

        //displayHealth();

        //displayScore();

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
                nextDifficulty = Time.time + 15;
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
            Debug.Log("" + round + " = " + round * 15 + " seconds");
            //New variables are set for blocks
            fireworkScript.newVariables(flySpeed, bossFlySpeed, fireworkHealth);
        }
    }


    //public void displayHealth()
    //{
    //    if (health > 0)
    //    {
    //        deathCanvas.enabled = false;
    //        healthText.text = "Health: " + health.ToString();
    //    }
    //    else
    //    {
    //        dead = true;
    //        deathCanvas.enabled = true;
    //        healthText.text = "Deaderinio";
    //    }
    //}

    //public void displayScore()
    //{
    //    scoreText.text = "Score: " + score.ToString();
    //    highScoreText.text = "High Score: " + highScore.ToString();

    //    if (score > highScore)
    //    {
    //        highScore = score;
    //        PlayerPrefs.SetInt("HighScore", highScore);
    //    }
    //}

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
        float target;
        int spawnPosX = Random.Range(-25, 25);
        if(spawnPosX < 0)
        {
            target = Random.Range(0, 20);
            angle = Random.Range(-10, -50);
        }
        else
        {
            target = Random.Range(-20, 0);
            angle = Random.Range(10, 50);
        }
        GameObject obj = (GameObject)Instantiate(standardFirework, new Vector3(spawnPosX, -20), Quaternion.identity);
        obj.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, angle);
    }

}
