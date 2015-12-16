using UnityEngine;
using System.Collections;

public class SmallBossScript : PhysicsBlockScript {
    public GameObject smallBoss2;
    public GameObject smallBoss3;
    public GameObject smallBoss4;

    float rightNow;
    private bool teleporting;
    private float nextTeleport;
    private float teleportingTime;
    private float teleportRate;
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
        //transform.localScale = new Vector3(100 , 100, 0);

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

    public void setSize(Vector3 vector)
    {
        transform.localScale = vector;
    }

    //public void teleport()
    //{
    //    if(Time.time >= nextTeleport && !teleporting)
    //    {
    //        rightNow = Time.time;
    //        teleporting = true;
    //    }
    //    if(teleporting)
    //    {
    //        transform.position = new Vector2(-100, -100);

    //        if (Time.time >= rightNow + .2)
    //        {
    //            transform.position = new Vector2(Random.Range(-25, 25), Random.Range(-15, 15));
    //            nextTeleport = Time.time + teleportRate;
    //            teleporting = false;
    //        }
    //    }
    //}

    public override void attack()
    {
        //if (!frozen)
        //{
        //    chargeRedBoss();
        //    if (Time.time >= bossAttackTime)
        //    {
        //        GameControllerScript.health = 0;
        //    }
        //}

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

        health--;

        if(health <= 0)
        {
            split();
            health = 1;
        }
        
    }

    //public void returnBoss()
    //{
    //    GameObject boss = Instantiate(smallBoss) as GameObject;
    //    SmallBossScript script = boss.GetComponent<SmallBossScript>();

    //    if (previousSize == new Vector3(size1, size1, 0))
    //    {
    //        script.setSize(new Vector3(size2, size2, 0));
    //        script.size = transform.localScale;
    //        script.maxSize = transform.localScale;

    //        script.launch();
    //    }
    //    else if (previousSize == new Vector3(size2, size2, 0))
    //    {
    //        script.transform.localScale = new Vector3(size2, size2, 0);
    //        script.size = transform.localScale;
    //        script.maxSize = transform.localScale;

    //        script.launch();
    //    }
    //    // smallBoss = Instantiate(smallBoss) as GameObject;
    //    // return boss;
    //}
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
            blockList.Remove(thisBlock);
            GameControllerScript.incrementScore(65 + GameControllerScript.scoreIncrement);
            deathAnim();
            Destroy(gameObject);
        }

        //if(size != new Vector3(size3,size3,0))
        //{
        //    //returnBoss();
        //    smallBoss = Instantiate(smallBoss) as GameObject;
        //    SmallBossScript script = smallBoss.GetComponent<SmallBossScript>();
        //
        //    if (previousSize == new Vector3(size1, size1, 0))
        //    {
        //        script.setSize(new Vector3(size2, size2, 0));
        //        script.size = transform.localScale;
        //        script.maxSize = transform.localScale;
        //
        //        script.launch();
        //    }
        //
        //    else if (previousSize == new Vector3(size2, size2, 0))
        //    {
        //        script.transform.localScale = new Vector3(size3, size3, 0);
        //        script.size = transform.localScale;
        //        script.maxSize = transform.localScale;
        //
        //        script.launch();
        //    }
        //}
    }

    public void deathAnim()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        GameObject floatingText = Instantiate(textPrefab) as GameObject;
        floatingText.transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 2);
    }
}

