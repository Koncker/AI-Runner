using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayController : MonoBehaviour
{

    public GameObject player;
    private float myDistanceToPlayer;                   // On Initialize, finds distance of gameObject to player in Z coordinate
    public bool targettedLethal = false;
    public bool leftLethal = false;
    public bool rightLethal = false;
    public bool frontLeftLethal = false;
    public bool frontRightLethal = false;
    public bool midLeftLethal = false;
    public bool midRightLethal = false;
    private bool detection;

    //private Vector3 frontal = new Vector3()
    //private Vector3 frontLeft 
    //private Vector

    // Use this for initialization
    void Start()
    {
        myDistanceToPlayer = (gameObject.transform.position.z - player.gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, gameObject.transform.position.y, player.gameObject.transform.position.z + myDistanceToPlayer);

        targettedLethal = rayCast(new Vector3(0.0f, -1.0f, 2.0f));
        leftLethal = rayCast(new Vector3(-1.0f, -4.0f, -0.25f));
        rightLethal = rayCast(new Vector3(1.0f, -4.0f, -0.25f));
        frontLeftLethal = rayCast(new Vector3(-1.0f, -4.0f, 6.0f));
        frontRightLethal = rayCast(new Vector3(1.0f, -4.0f, 6.0f));
        midLeftLethal = rayCast(new Vector3(-1.0f, -4.0f, 3f));
        midRightLethal = rayCast(new Vector3(1.0f, -4.0f, 3f));

        Debug.DrawRay(transform.position, new Vector3(0.0f, -1.0f, 2.0f) * 5, Color.red, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(-1.0f, -4.0f, -0.25f) * 2, Color.blue, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(1.0f, -4.0f, -0.25f) * 2, Color.blue, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(-1.0f, -4.0f, 6.0f) * 2, Color.green, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(1.0f, -4.0f, 6.0f) * 2, Color.green, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(-1.0f, -4.0f, 3f) * 2, Color.cyan, 0.5f);
        Debug.DrawRay(transform.position, new Vector3(1.0f, -4.0f, 3f) * 2, Color.cyan, 0.5f);
    }

    bool rayCast(Vector3 vector)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, vector, out hit, 100.0f) && hit.collider.tag == "lethal")
        {
            detection = true;
        }
        else
        {
            detection = false;
        }

        return detection;
    }
}
