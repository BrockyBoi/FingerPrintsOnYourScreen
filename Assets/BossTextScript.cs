using UnityEngine;
using System.Collections;

public class BossTextScript : MonoBehaviour {
    Transform trans;
    int boss;
    // Use this for initialization
    void Start()
    {
        boss = GameControllerScript.scoreIncrement + 500;

        GetComponent<TextMesh>().text = boss.ToString();

        trans = GetComponent<Transform>();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        trans.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
    }
}
