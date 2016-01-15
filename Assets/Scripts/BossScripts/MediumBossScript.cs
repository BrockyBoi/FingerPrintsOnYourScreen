using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MediumBossScript : PhysicsBlockScript {

    float nextSpawn;
	// Use this for initialization
	public override void Start () {
        base.Start();

        GameControllerScript.bossRound = true;
        GameControllerScript.boss = this;
        currentTime = Time.time;
        spawnRate = GameControllerScript.spawnRate;
        nextSpawn = Time.time + spawnRate / 1.75f;

        health = 35;

        transform.localScale = new Vector3(150, 150, 0);
        maxSize = new Vector3(150, 150, 0);

        bossAttackTime = Time.time + bossAttack;
    }
	
	// Update is called once per frame
	public override void Update () {
        if (health <= 0)
        {
            GameControllerScript.changeRound = true; 
            GameControllerScript.bossRound = false;
            GameControllerScript.boss = null;
            GameControllerScript.incrementScore(500);
        }

        base.Update();

        //base.addHealth();
        //base.regenerate();

        attack();

        spawnBlock();
	}

    public override void spawnBlock()
    {
        if (!GameControllerScript.paused && !GameControllerScript.dead)
        {
            spawnRate = GameControllerScript.spawnRate;
            if (Time.time >= (nextSpawn + spawnRate) && !frozen && !GameControllerScript.dead)
            {
                nextSpawn = Time.time + spawnRate / 1.5f;

                float range = Random.Range(0f, 100f);

                if (range <= 25)
                {
                    Instantiate(block, new Vector3(12, Random.Range(-10, 10), 0), transform.rotation);
                }
                else if (range > 25 && range <= 50)
                {
                    Instantiate(block, new Vector3(-12, Random.Range(-10, 10), 0), transform.rotation);
                }
                else if (range > 50 && range <= 75)
                {
                    Instantiate(block, new Vector3(Random.Range(-10, 10), -12, 0), transform.rotation);
                }
                else
                {
                    Instantiate(block, new Vector3(Random.Range(-10, 10), 12, 0), transform.rotation);
                }

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
