﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MediumBossScript : PhysicsBlockScript {
    //public Text bossHealth;
    int increment;

    float nextSpawn;
	// Use this for initialization
	public override void Start () {
        base.Start();

        increment = GameControllerScript.scoreIncrement;
        currentTime = Time.time;
        spawnRate = GameControllerScript.spawnRate;
        nextSpawn = Time.time + spawnRate / 1.75f;

        health = 35;

        transform.localScale = new Vector3(150, 150, 0);
        maxSize = new Vector3(150, 150, 0);

        bossAttackTime = Time.time + bossAttack;

        //bossHealth = GetComponent<Text>();
    }
	
	// Update is called once per frame
	public override void Update () {
        if (health <= 0)
        {
            GameControllerScript.incrementScore(500);
        }
        base.Update();
        //base.addHealth();
        //base.regenerate();

        attack();

        spawnBlock();

        //bossHealth.text = "Boss Health: " + health.ToString();
	}

    public override void spawnBlock()
    {
        spawnRate = GameControllerScript.spawnRate;
        if (Time.time >= (nextSpawn + spawnRate) && !frozen && !GameControllerScript.dead)
        {
            //Instantiate(block, new Vector3(10,10,0), transform.rotation);

            //Instantiate(block, new (Random.Range(),Random.Range(),0), transform.rotation);
            nextSpawn = Time.time + spawnRate / 1.5f;

            float range = Random.Range(0f, 100f);

            if (range <= 25)
            {
                Instantiate(block, new Vector3(12, Random.Range(-10,10), 0), transform.rotation);
            }
            else if (range > 25 && range <= 50)
            {
                Instantiate(block, new Vector3(-12, Random.Range(-10,10), 0), transform.rotation);
            }
            else if (range > 50 && range <= 75)
            {
                Instantiate(block, new Vector3(Random.Range(-10,10), -12, 0), transform.rotation);
            }
            else
            {
                Instantiate(block, new Vector3(Random.Range(-10,10), 12, 0), transform.rotation);
            }

        }
    }

    public override void attack()
    {
            chargeRedBoss();
            if (Time.time >= bossAttackTime)
            {
                GameControllerScript.health = 0;
            }
    }

    
}
