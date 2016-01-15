using UnityEngine;
using System.Collections;

public class SmallBossScript : PhysicsBlockScript {
    public GameObject smallBoss2;
    public GameObject smallBoss3;
    public GameObject smallBoss4;

    //float rightNow;
    //private bool teleporting;
    //private float nextTeleport;
    //private float teleportingTime;
    //private float teleportRate;
    public static int divide;

    int size1;
    int size2;
    int size3;
    int size4;

    private Vector3 previousSize;
    public Vector3 size;

	// Use this for initialization
	public override void Start () {
        base.Start();
        launch();
        transform.localScale = size;
        health = 1;

        GameControllerScript.smallBossActive = true;
        GameControllerScript.bossRound = true;

        size1 = 100;
        size2 = 75;
        size3 = 50;
        size4 = 33;

        size = transform.localScale;
        maxSize = size;

        attackTime = Time.time + attackRate + 2;
    }

	
	// Update is called once per frame
	public override void Update () {
        base.Update();


        //teleport();
        attack();
	}

    public override void destroyBlock()
    {
        // Do nothing
    }

    public void setSize(Vector3 vector)
    {
        transform.localScale = vector;
    }

    public override void attack()
    {
        if (!frozen)
        {
            chargeRedNormal();
            if (Time.time >= attackTime)
            {
                GameControllerScript.health -= damage;
                currentTime = Time.time;
                attackTime = currentTime + attackRate + 3;
            }
        }
    }

    public override void OnMouseDown()
    {
        if (!GameControllerScript.dead)
        {
            health--;

            if (health <= 0)
            {
                split();
                health = 1;
            }
        } 
    }

    public void split()
    {
        if (size == new Vector3(size1, size1, 0))
        {
            previousSize = size;
            transform.localScale = new Vector3(size2, size2, 0);
            size = transform.localScale;
            maxSize = transform.localScale;
            Instantiate(smallBoss2, new Vector2(transform.localPosition.x, transform.localPosition.y), Quaternion.identity);
            currentTime = Time.time;
            attackTime = Time.time + attackRate + 3;
            base.launch();
        }
        else if (size == new Vector3(size2, size2, 0))
        {
            previousSize = size;
            transform.localScale = new Vector3(size3, size3, 0);
            size = transform.localScale;
            maxSize = transform.localScale;
            Instantiate(smallBoss3, new Vector2(transform.localPosition.x, transform.localPosition.y), Quaternion.identity);
            currentTime = Time.time;
            attackTime = Time.time + attackRate + 3;
            base.launch();
        }
        else if (size == new Vector3(size3, size3, 0))
        {
            previousSize = size;
            transform.localScale = new Vector3(size4, size4, 0);
            size = transform.localScale;
            maxSize = transform.localScale;
            Instantiate(smallBoss4, new Vector2(transform.localPosition.x, transform.localPosition.y), Quaternion.identity);
            currentTime = Time.time;
            attackTime = Time.time + attackRate + 3;
            base.launch();
        }
        else if (size == new Vector3(size4, size4, 0))
        {
            //GameControllerScript.blockList.Remove(thisBlock);
            GameControllerScript.incrementScore(65 + GameControllerScript.scoreIncrement);
            deathAnim();
            GameControllerScript.smallBossCount++;
            Destroy(gameObject);
        }
    }

    public void deathAnim()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject floatingText = Instantiate(textPrefab) as GameObject;
        floatingText.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 2);
    }
}

