using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCam : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(0, 2.35f, player.gameObject.transform.position.z - 4);   // DEPRECATED CODE -- DELETE LATER
    }
}
