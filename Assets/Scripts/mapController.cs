using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mapController : MonoBehaviour {

    public float zScenePos = 68;        // default 68
    private int sceneRNG = 0;           // default 0
    private int obstRNG = 0;            // default 0
    private int colCounter = 0;         // default 0
    private int gameMaxLength = 100000; // default 100'000 -- Limits the game to 100'000 block lengths in Z axis

    private System.Random rnd = new System.Random();    // Random Number Generator

    public Transform bbNoPit;       // Map Block without pitfall
    public Transform bbPitCenter;   // Map Block with Center pitfall
    public Transform bbPitLeft;     // Map Block with Left pitfall
    public Transform bbPitRight;    // Map Block with Right pitfall

    public Transform obstacleCenter1;   // Center Lane Obstacle with Length 1
    public Transform obstacleCenter2;   // Center Lane Obstacle with Length 2
    public Transform obstacleCenter3;   // Center Lane Obstacle with Length 3
    public Transform obstacleLeft1;     // Left Lane Obstacle with Length 1
    public Transform obstacleLeft2;     // Left Lane Obstacle with Length 2
    public Transform obstacleLeft3;     // Left Lane Obstacle with Length 3
    public Transform obstacleRight1;    // Right Lane Obstacle with Length 1
    public Transform obstacleRight2;    // Right Lane Obstacle with Length 2
    public Transform obstacleRight3;    // Right Lane Obstacle with Length 3

    private Transform[] obstacleArray;


    // Use this for initialization
    void Start () {
        // Instantiates the intial 4 Blocks. Distances default = 36, 44, 52, 60
        Instantiate(bbNoPit, new Vector3(0, 0, 36), bbNoPit.rotation);
        Instantiate(bbPitCenter, new Vector3(0, 0, 44), bbPitCenter.rotation);
        Instantiate(bbNoPit, new Vector3(0, 0, 52), bbNoPit.rotation);
        Instantiate(bbPitCenter, new Vector3(0, 0, 60), bbPitCenter.rotation);
        //obstacleArray = new Transform[]{
        //                obstacleCenter1,
        //                obstacleCenter2,
        //                obstacleCenter3,
        //                obstacleLeft1,
        //                obstacleLeft2,
        //                obstacleLeft3,
        //                obstacleRight1,
        //                obstacleRight2,
        //                obstacleRight3};
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")   // Counts the number of floors the collider has collided with
        {
            colCounter += 1;        // Increment by 1 for every collision
        }

        if (zScenePos < gameMaxLength && colCounter == 4)   // Limits the game to gameMaxLength set above, and checks if 4 floor blocks have been destroyed. (any more causes issues)
        {
            sceneRNG = rnd.Next(1, 10);     // Generates a random number to ensure that the game is fair.

            switch (sceneRNG)
            {
                case 2:     // if the sceneRNG is set to 2 - Instantiates a block with a center pitfall
                    //Debug.Log("Instantiating Pit Center");
                    Instantiate(bbPitCenter, new Vector3(0, 0, zScenePos), bbPitCenter.rotation);   // Instantiate center pit block
                    obstRNG = rnd.Next(1, 11);   // Generates a random number to create a random obstacle for the above instantiated map block.

                    switch (obstRNG)    // Instantiates a random obstacle based on case number, (7-10 = no obstacle generated)
                    {
                        case 1:
                            Instantiate(obstacleLeft1, new Vector3(obstacleLeft1.position.x, 1, zScenePos), obstacleLeft1.rotation);
                            break;
                        case 2:
                            Instantiate(obstacleLeft2, new Vector3(obstacleLeft2.position.x, 1, zScenePos), obstacleLeft2.rotation);
                            break;
                        case 3:
                            Instantiate(obstacleLeft3, new Vector3(obstacleLeft3.position.x, 1, zScenePos), obstacleLeft3.rotation);
                            break;
                        case 4:
                            Instantiate(obstacleRight1, new Vector3(obstacleRight1.position.x, 1, zScenePos), obstacleRight1.rotation);
                            break;
                        case 5:
                            Instantiate(obstacleRight2, new Vector3(obstacleRight2.position.x, 1, zScenePos), obstacleRight2.rotation);
                            break;
                        case 6:
                            Instantiate(obstacleRight3, new Vector3(obstacleRight3.position.x, 1, zScenePos), obstacleRight3.rotation);
                            break;
                        default:
                            break;
                    }
                    break;

                case 4:
                    //Debug.Log("Instantiating Pit Left");
                    Instantiate(bbPitLeft, new Vector3(0, 0, zScenePos), bbPitLeft.rotation);       // Instantiate left pit block
                    obstRNG = rnd.Next(1, 11);  // Generates a random number to create a random obstacle for the above instantiated map block.

                    switch (obstRNG)    // Instantiates a random obstacle based on case number, (7-10 = no obstacle generated)
                    {
                        case 1:
                            Instantiate(obstacleCenter1, new Vector3(obstacleCenter1.position.x, 1, zScenePos), obstacleCenter1.rotation);
                            break;
                        case 2:
                            Instantiate(obstacleCenter2, new Vector3(obstacleCenter2.position.x, 1, zScenePos), obstacleCenter2.rotation);
                            break;
                        case 3:
                            Instantiate(obstacleCenter3, new Vector3(obstacleCenter3.position.x, 1, zScenePos), obstacleCenter3.rotation);
                            break;
                        case 4:
                            Instantiate(obstacleRight1, new Vector3(obstacleRight1.position.x, 1, zScenePos), obstacleRight1.rotation);
                            break;
                        case 5:
                            Instantiate(obstacleRight2, new Vector3(obstacleRight2.position.x, 1, zScenePos), obstacleRight2.rotation);
                            break;
                        case 6:
                            Instantiate(obstacleRight3, new Vector3(obstacleRight3.position.x, 1, zScenePos), obstacleRight3.rotation);
                            break;
                        default:
                            break;
                    }
                    break;

                case 6:
                    //Debug.Log("Instantiating Pit Right");
                    Instantiate(bbPitRight, new Vector3(0, 0, zScenePos), bbPitRight.rotation);     // Instantiate right pit block
                    obstRNG = rnd.Next(1, 11);  // Generates a random number to create a random obstacle for the above instantiated map block.

                    switch (obstRNG)    // Instantiates a random obstacle based on case number, (7-10 = no obstacle generated)
                    {
                        case 1:
                            Instantiate(obstacleCenter1, new Vector3(obstacleCenter1.position.x, 1, zScenePos), obstacleCenter1.rotation);
                            break;
                        case 2:
                            Instantiate(obstacleCenter2, new Vector3(obstacleCenter2.position.x, 1, zScenePos), obstacleCenter2.rotation);
                            break;
                        case 3:
                            Instantiate(obstacleCenter3, new Vector3(obstacleCenter3.position.x, 1, zScenePos), obstacleCenter3.rotation);
                            break;
                        case 4:
                            Instantiate(obstacleLeft1, new Vector3(obstacleLeft1.position.x, 1, zScenePos), obstacleLeft1.rotation);
                            break;
                        case 5:
                            Instantiate(obstacleLeft2, new Vector3(obstacleLeft2.position.x, 1, zScenePos), obstacleLeft2.rotation);
                            break;
                        case 6:
                            Instantiate(obstacleLeft3, new Vector3(obstacleLeft3.position.x, 1, zScenePos), obstacleLeft3.rotation);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    //Debug.Log("Instantiating no Pit");
                    Instantiate(bbNoPit, new Vector3(0, 0, zScenePos), bbNoPit.rotation);           // Instantiate no pit block
                    obstRNG = rnd.Next(1, 11);  // Generates a random number to create a random obstacle for the above instantiated map block.

                    switch (obstRNG)    // Instantiates a random obstacle based on case number, (this map block will always generate an obstacle, except on case 10)
                    {
                        case 1:
                            Instantiate(obstacleCenter1, new Vector3(obstacleCenter1.position.x, 1, zScenePos), obstacleCenter1.rotation);
                            break;
                        case 2:
                            Instantiate(obstacleCenter2, new Vector3(obstacleCenter2.position.x, 1, zScenePos), obstacleCenter2.rotation);
                            break;
                        case 3:
                            Instantiate(obstacleCenter3, new Vector3(obstacleCenter3.position.x, 1, zScenePos), obstacleCenter3.rotation);
                            break;
                        case 4:
                            Instantiate(obstacleLeft1, new Vector3(obstacleLeft1.position.x, 1, zScenePos), obstacleLeft1.rotation);
                            break;
                        case 5:
                            Instantiate(obstacleLeft2, new Vector3(obstacleLeft2.position.x, 1, zScenePos), obstacleLeft2.rotation);
                            break;
                        case 6:
                            Instantiate(obstacleLeft3, new Vector3(obstacleLeft3.position.x, 1, zScenePos), obstacleLeft3.rotation);
                            break;
                        case 7:
                            Instantiate(obstacleRight1, new Vector3(obstacleRight1.position.x, 1, zScenePos), obstacleRight1.rotation);
                            break;
                        case 8:
                            Instantiate(obstacleRight2, new Vector3(obstacleRight2.position.x, 1, zScenePos), obstacleRight2.rotation);
                            break;
                        case 9:
                            Instantiate(obstacleRight3, new Vector3(obstacleRight3.position.x, 1, zScenePos), obstacleRight3.rotation);
                            break;
                        default:
                            break;
                    }
                    break;
            }

            zScenePos += 8;     // Increments z position by 8 (block size).
            colCounter = 0;     // Resets the collision counter.
        }
        Destroy(other.gameObject);  // Clears the map with the object it collides, so we do not have too many objects loaded.
    }
}
