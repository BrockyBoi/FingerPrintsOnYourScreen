using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LargeBossScript : PhysicsBlockScript {

    // Use this for initialization
    public override void Start()
    {
        base.Start();

        GameControllerScript.bossRound = true;
        GameControllerScript.boss = this;

        health = 60;

        transform.localScale = new Vector3(250, 250, 0);
        maxSize = new Vector3(250, 250, 0);

        bossAttackTime = Time.time + bossAttack;

        currentTime = Time.time;

    }
        // Update is called once per frame
        public override void Update () {
        if(health <= 0)
        {
            GameControllerScript.changeRound = true;
            GameControllerScript.bossRound = false;
            GameControllerScript.boss = null;
            GameControllerScript.incrementScore(500);
        }
        base.Update();
        attack();
        //bossHealth.text = "Boss Health: " + health.ToString();
	}

    public override void attack()
    {
        if (!frozen)
        {
            chargeRedBoss();
            if (Time.time >= bossAttackTime)
            {
                GameControllerScript.health = 0;
            }
        }
    }
}
