using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextMovementScript : MonoBehaviour {
    Transform trans;
    int regular;
	// Use this for initialization
	void Start () {
        regular = GameControllerScript.scoreIncrement;

        GetComponent<TextMesh>().text = regular.ToString();

        trans = GetComponent<Transform>();
        Destroy(gameObject, 1);
    }
	
	// Update is called once per frame
	void Update () {
        trans.Translate(Vector3.up * Time.deltaTime * 5, Space.World);
    }
}
