using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class GameControllerScript : MonoBehaviour {
	public static GameControllerScript control;

    public static PhysicsBlockScript boss;

    //Score System
    public int highScore;
    public static int score;
    public static int scoreIncrement;

    //Text
    public Canvas deathCanvas;
    public static bool paused;
    public Text highScoreText;
    public Text healthText;
    public Text bossHealth;
    public Text scoreText;

    //Spawn system
    float currentTime;
    float spawnTime;
    public static float spawnRate;

    //Round system
    public static bool bossRound;
    int round;
    public static bool changeRound;

    //Block prefabs
    public GameObject block;
    public GameObject bomb;
    public GameObject blackHole;
    public GameObject freeze;
    public GameObject mutliplier;
    public GameObject smallBoss;
    public GameObject mediumBoss;
    public GameObject largeBoss;
	public GameObject explosionParticles;

    //Health system
    public static bool dead;
    public static int blockHealth;
    public static float health;

    //Difficulty System
    public float nextDifficulty;
    float easyDifficulty;
    float mediumDifficulty;
    float hardDifficulty;
    bool easy;
    bool medium;
    bool hard;
    public int difficultyLevel;

    //Variables all blocks share
    PhysicsBlockScript blockScript;

    public static float growRate;
    public static float attackRate;

    public static int regularDamage = 1;

    public static bool smallBossActive = false;
    public static float smallBossAttack = 2;
    public static int smallBossDamage = 1;
    public static int smallBossCount = 0;

    public static bool mediumBossActive = false;
    public static float mediumBossAttack = 2.5f;
    public static int mediumBossDamage;

    public static bool largeBossActive = false;
    public static float bossAttack = 20;

    //Arraylist of all blocks currently in the game
   //public static List<PhysicsBlockScript> blockList;


    // Use this for initialization
    void Start () { 
		//set up scene control
		if (!control) {
			control = this;
		} else
			Destroy (this);

        //spawnSmallBoss();

        //spawnMediumBoss();

        //spawnLargeBoss();


        //Beginning health
        health = 5;
        dead = false;

        //Score system
        score = 0;
        scoreIncrement = 10;        

        //First round / difficulty
        bossRound = false;
        round = 1;
        difficultyLevel = 1;
        changeRound = false;

        //Difficulty 
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

        //Block variables
        attackRate =  3;
        growRate = .15f;
        regularDamage = 1;
        blockHealth = 1;

        //Boss variables 
        bossAttack = 20;

        //blockScript.newVariables(attackRate, growRate, regularDamage, blockHealth, bossAttack);

        //Text
        getHighScore();
        Debug.Log("Highscore is : " + highScore);

        healthText.text = "Health: " + health.ToString();
        bossHealth.text = "";

        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

        paused = false;

        //blockList = new List<PhysicsBlockScript>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        displayBoss();

        displayHealth();

        spawnSystem();

        ifBeatBoss();

        difficulty();

        displayScore();

        newRound();
    }

    //Endless spawn system
    void spawnSystem()
    {
        if (Time.time >= spawnTime && !dead)
        {
            if (!bossRound)
            {
                spawnTime = Time.time + spawnRate;
                spawnRegular();
            }

            if (round % 4 == 0 && easy)
            {
                spawnTime = Time.time + 100;
                bossSpawner();
            }
            else if (round % 6 == 0 && medium)
            {
                spawnTime = Time.time + 100;
                bossSpawner();
            }
            else if (round % 10 == 0 && hard)
            {
                spawnTime = Time.time + 100;
                bossSpawner();
            }
        }
    }

    void difficulty()
    {
        if(Time.time >= nextDifficulty && !bossRound)
        {
            difficultyLevel++;
            round++;

            difficultySetting();

            growRate += .03f;

            if (spawnRate > .5f)
            {
                spawnRate -= .1f;
            }
            else
            {
                spawnRate -= .02f;
            }

            if(bossAttack > 13f)
            {
                bossAttack -= .35f;
            }
            else
            {
                bossAttack -= .05f;
            }

            if (attackRate > 1.5f)
            {
                attackRate -= .1f;
            }
            else
            {
                attackRate -= .025f;
            }

            scoreIncrement += 5;

            //New variables are set for blocks
            //blockScript.newVariables(attackRate, growRate, regularDamage, blockHealth, bossAttack);
        }
    }

    void ifBeatBoss()
    {
        if(bossRound && smallBossActive)
        {
            if(smallBossCount == 8)
            {
                changeRound = true;
                bossRound = false;
                smallBossActive = false;
                smallBossCount = 0;
            }
            //if(blockList.Count == 0) 
            //{
            //    round++;
            //    bossRound = false;
            //    difficultySetting();
            //    spawnTime = Time.time;

            //    boss = null;
            //}
        }
    }

    public void newRound()
    {
        if(changeRound)
        {
            round++;
            spawnTime = Time.time;
            difficultySetting();
            changeRound = false;
        }
    }

    public void displayHealth()
    {
        if (health > 0)
        {
            deathCanvas.enabled = false;
            healthText.text = "Health: " + health.ToString();
        }
        else
        {
            dead = true;
            deathCanvas.enabled = true;
            healthText.text = "Deaderinio";
        }
    }

    public void displayBoss()
    {
        if (bossRound)
        {
            //for (int i = 0; i < blockList.Count; i++)
            //{
            //    if (blockList[i].tag == "Medium Boss")
            //    {
            //        boss = blockList[i];
            //    }
            //    if (blockList[i].tag == "Large Boss")
            //    {
            //        boss = blockList[i];
            //    }
            //}
        }
        if(boss != null)
        {
            bossHealth.text = "Boss Health: " + boss.health.ToString();
        }
        else
        {
            bossHealth.text = "";
        }
    }

    public void difficultySetting()
    {
        if (tag == "Easy Controller")
        {
            easy = true;
            nextDifficulty = Time.time + easyDifficulty;
        }
        else if (tag == "Medium Controller")
        {
            medium = true;
            nextDifficulty = Time.time + mediumDifficulty;
        }
        else if (tag == "Hard Controller")
        {
            hard = true;
            nextDifficulty = Time.time + hardDifficulty;
        }
    }

    public void setHighScore()
    {
        if (easy)
        {
            PlayerPrefs.SetInt("HighScore Easy", highScore);
        }
        else if (medium)
        {
            PlayerPrefs.SetInt("HighScore Medium", highScore);
        }
        else if (hard)
        {
            PlayerPrefs.SetInt("HighScore Hard", highScore);
        }
    }

    public void getHighScore()
    {
        if (easy)
        {
            highScore = PlayerPrefs.GetInt("HighScore Easy");
        }
        else if (medium)
        {
            highScore = PlayerPrefs.GetInt("HighScore Medium");
        }
        else if (hard)
        {
            highScore = PlayerPrefs.GetInt("HighScore Hard");
        }
    }

    public void displayScore()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

        if (score > highScore)
        {
            highScore = score;

            setHighScore();
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
        Instantiate(block, new Vector3(Random.Range(-25f, 25f), Random.Range(-15f, 15f), 0), Quaternion.identity);
    }

    void spawnSmallBoss()
    {
        Instantiate(smallBoss, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void spawnMediumBoss()
    {
        Instantiate(mediumBoss, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void spawnLargeBoss()
    {
        Instantiate(largeBoss, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void bossSpawner()
    {
            float randomNum = Random.Range(0, 100);
            if (randomNum >= 0 && randomNum <= 33)
            {
                spawnSmallBoss();
            }
            else if (randomNum > 33 && randomNum <= 67)
            {
                spawnMediumBoss();
            }
            else
            {
                spawnLargeBoss();
            }
    }

    void spawnBomb()
    {
        Instantiate(bomb, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnBlackHole()
    {
        Instantiate(blackHole, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnFreeze()
    {
        Instantiate(freeze, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }

    void spawnMultiplier()
    {
        Instantiate(mutliplier, new Vector3(Random.Range(-30f, 30f), Random.Range(-20f, 20f), 0), Quaternion.identity);
    }
}
