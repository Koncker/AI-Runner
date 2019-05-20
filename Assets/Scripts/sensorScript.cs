using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorScript : MonoBehaviour {

    public GameObject player;
    private float myDistanceToPlayer;

    // Use this for initialization
    void Start()
    {
        myDistanceToPlayer = (gameObject.transform.position.z - player.gameObject.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, player.gameObject.transform.position.z + myDistanceToPlayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "maxCenterSensor")
        {
            playerController.maxMidSensor = true;
        }
    }
}
