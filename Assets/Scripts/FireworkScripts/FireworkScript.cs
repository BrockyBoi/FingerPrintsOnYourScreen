using UnityEngine;
using System.Collections;

public class FireworkScript : MonoBehaviour {

    public Transform trans;
    public Rigidbody2D rb2d;
    public GameObject firework;
    public GameObject explosionPrefab;
    Vector3 startPosition;
    int health;
    float flySpeed;
    float bossFlySpeed;


	// Use this for initialization
	public virtual void Start () {
        trans = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();

        newVariables(FireworkControllerScript.flySpeed, FireworkControllerScript.bossFlySpeed, 
                     FireworkControllerScript.fireworkHealth);
	}
	
	// Update is called once per frame
	public virtual void Update () {

        fly();

        healthSystem();
    }

    public virtual void OnMouseDown()
    {
        if(!FireworkControllerScript.paused)
        {
            health--;
        }
    }

    public virtual void healthSystem()
    {
        if(health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (trans.localPosition.y >= 22)
        {
            Destroy(gameObject);
            FireworkControllerScript.health--;
        }
    }

    public virtual void fly()
    {
        if (!FireworkControllerScript.paused)
        {
            if (tag == "Firework")
            {
                rb2d.velocity = transform.up * flySpeed;
            }
            else if (tag == "Boss Firework")
            {
                rb2d.velocity = transform.up * bossFlySpeed;
            }
        }
    }

    public void newVariables(float myFlySpeed, float myBossSpeed, int myHealth)
    {
        flySpeed = myFlySpeed;
        health = myHealth;
        bossFlySpeed = myBossSpeed;
    }
}
