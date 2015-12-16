using UnityEngine;
using System.Collections;

public class SBTextScript : MonoBehaviour {
    Transform trans;
    int sB;
    // Use this for initialization
    void Start()
    {
        sB = GameControllerScript.scoreIncrement + 65;

        GetComponent<TextMesh>().text = sB.ToString();

        trans = GetComponent<Transform>();
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        trans.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
    }
}
