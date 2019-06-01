using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayController : MonoBehaviour
{

    public GameObject player;
    private float myDistanceToPlayer;                   // On Initialize, finds distance of gameObject to player in Z coordinate
    public bool targettedLethal = false;
    public bool veryFarFrontLethal = false;
    public bool directFrontLethal = false;
    public bool leftLethal = false;
    public bool rightLethal = false;
    public bool frontLeftLethal = false;
    public bool frontRightLethal = false;
    public bool midLeftLethal = false;
    public bool midRightLethal = false;
    public bool farFrontLeftLethal = false;
    public bool farFrontRightLethal = false;
    private bool detection;

    private Vector3 farFront = new Vector3(0.0f, -1.0f, 2.0f);
    private Vector3 veryFarFront = new Vector3(0.0f, -1.0f, 4.0f);
    private Vector3 nearFront = new Vector3(0.0f, -1.0f, 1.0f);
    private Vector3 farFrontLeft = new Vector3(-1.0f, -4.0f, 10.0f);
    private Vector3 farFrontRight = new Vector3(1.0f, -4.0f, 10.0f);
    private Vector3 frontLeft = new Vector3(-1.0f, -4.0f, 6.0f);
    private Vector3 frontRight = new Vector3(1.0f, -4.0f, 6.0f);
    private Vector3 midLeft = new Vector3(-1.0f, -4.0f, 3f);
    private Vector3 midRight = new Vector3(1.0f, -4.0f, 3f);
    private Vector3 nearLeft = new Vector3(-1.0f, -4.0f, -0.25f);
    private Vector3 nearRight = new Vector3(1.0f, -4.0f, -0.25f);

    // Use this for initialization
    void Start()
    {
        myDistanceToPlayer = (gameObject.transform.position.z - player.gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, gameObject.transform.position.y, player.gameObject.transform.position.z + myDistanceToPlayer);

        targettedLethal = rayCast(farFront);
        veryFarFrontLethal = rayCast(veryFarFront);
        directFrontLethal = rayCast(nearFront);
        farFrontLeftLethal = rayCast(farFrontLeft);
        farFrontRightLethal = rayCast(farFrontRight);
        frontLeftLethal = rayCast(frontLeft);
        frontRightLethal = rayCast(frontRight);
        midLeftLethal = rayCast(midLeft);
        midRightLethal = rayCast(midRight);
        leftLethal = rayCast(nearLeft);
        rightLethal = rayCast(nearRight);

        Debug.DrawRay(transform.position, farFront * 5, Color.red, 0.5f);           // Distant front
        Debug.DrawRay(transform.position, veryFarFront * 5, Color.magenta, 0.5f);   // Very Far front
        Debug.DrawRay(transform.position, nearFront * 5, Color.white, 0.5f);        // Near front
        Debug.DrawRay(transform.position, farFrontLeft * 2, Color.black, 0.5f);     // Far Front Left
        Debug.DrawRay(transform.position, farFrontRight * 2, Color.black, 0.5f);    // Far Front Right
        Debug.DrawRay(transform.position, frontLeft * 2, Color.green, 0.5f);        // Front Left
        Debug.DrawRay(transform.position, frontRight * 2, Color.green, 0.5f);       // Front Right
        Debug.DrawRay(transform.position, midLeft * 2, Color.cyan, 0.5f);           // Mid Left
        Debug.DrawRay(transform.position, midRight * 2, Color.cyan, 0.5f);          // Mid Right
        Debug.DrawRay(transform.position, nearLeft * 2, Color.blue, 0.5f);          // Rear Left
        Debug.DrawRay(transform.position, nearRight * 2, Color.blue, 0.5f);         // Rear Right
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
