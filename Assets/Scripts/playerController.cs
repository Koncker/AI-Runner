using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    Rigidbody rb;

    // Variables to keep tack of Score
    public static float maxScore;
    public Text scoreText;
    public Text maxScoreText;
    private float timer;

    public rayController RayController;     // Access to RayController Script, i.e. sensors

    private System.Random rnd = new System.Random();    // Random Number Generator
    private int movementRNG = 0;           // default 0

    public KeyCode moveL;   // Movement KeyCode for Player Controller -- Left    -- SET TO A
    public KeyCode moveR;   // Movement KeyCode for Player Controller -- Right   -- SET TO D
    public KeyCode moveUp;  // Movement KeyCode for Player Controller -- Up      -- SET TO W or SPACE

    public static float verticalVel = 0f;   // Setting for Y Axis Velocity -- Default value 0.0f
    public static float forwVel = 7.0f;     // Setting for Z Axis Velocity -- Default value 7.0f

    private float horizVel = 0;             // Setting for X Axis Velocity -- Default value 0.0f
    private float maxVel = 10f;             // Maximum velocity permitted by the game -- Default value 10.0f
    private float timeCounter = 0;                  // Use of timer in Update
    private float jumpHeight = 300.0f;      // Default value 300.0f
    private bool isJumping = false;         // Boolean to check if player is Jumping -- Default false
    private bool controlLocked = false;     // Controls player input availability. false = Player can move.

    private int laneNum = 0;                // Controls lane number for movement -- 0 = Center, -1 = Left, 1 = Right
    private int midLethalCounter;           // Checks when in mid lane how many obstacles exist around it

    public bool AIMode;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        scoreText.text = "Time Survived: " + timer.ToString();
        maxScoreText.text = "Max Time Survived: " + maxScore.ToString();

        rb.velocity = new Vector3(horizVel, rb.velocity.y, forwVel);    // Controls the full movement of the player.

        if (!AIMode)
        {
            //Movement to the Left - Ensures that player does not leave the map, and that they do not rapidfire the key.
            if (Input.GetKeyDown(moveL) && laneNum > -1 && controlLocked == false)
            {
                moveLeft();
            }
            //Movement to the Right - Ensures that player does not leave the map, and that they do not rapidfire the key.
            if (Input.GetKeyDown(moveR) && laneNum < 1 && controlLocked == false)
            {
                moveRight();
            }
            // Jump - Ensures that the player cannot rapidfire jump.
            if (Input.GetKeyDown(moveUp) && controlLocked == false)
            {
                jump();
            }
        }

        if (AIMode) // Behaviour Tree Setup
        {
            // Direct collision decision
            if (RayController.directFrontLethal && !controlLocked)
            {
                jump();
            }

            // Center lane decision making
            if (RayController.targettedLethal && laneNum == 0 && !controlLocked) //targettedLethal == true)
            {
                // Checks to see if object is detected anywhere on the left, and moves right
                if(RayController.midLeftLethal)
                {
                    midLethalCounter += 1;
                }

                if (RayController.midRightLethal)
                {
                    midLethalCounter += 1;
                }

                if (midLethalCounter == 2)
                {
                    jump();
                }

                else if (RayController.frontLeftLethal || RayController.leftLethal || RayController.midLeftLethal)
                {
                    if (RayController.leftLethal && (RayController.frontRightLethal || RayController.rightLethal || RayController.midRightLethal))
                        moveLeft();
                    else
                        moveRight();
                    //Debug.Break();
                }
                // Checks to see if object is detected anywhere on the right, and moves left
                else if (RayController.frontRightLethal || RayController.rightLethal || RayController.midRightLethal)
                {
                    if (RayController.rightLethal && (RayController.frontLeftLethal || RayController.leftLethal || RayController.midLeftLethal))
                        moveRight();
                    else
                        moveLeft();
                    //Debug.Break();
                }
                // If no objects are found, moves either left or right.
                else
                {
                    movementRNG = rnd.Next(1, 3);   // Random between 1 (inclusive) and 3 (exclusive), i.e. 1-2
                    switch (movementRNG)
                    {
                        case 1:
                            moveRight();
                            break;
                        case 2:
                            moveLeft();
                            break;
                    }
                }
                midLethalCounter = 0;
            }

            // Left lane decision making
            if (RayController.targettedLethal && laneNum == -1 && !controlLocked)
            {
                // Checks to see if object is detected in front right, or mid right
                if (RayController.midRightLethal || RayController.frontRightLethal)
                {
                    // If an object is also detected directly to the right, jump
                    if((RayController.rightLethal || RayController.midRightLethal) && !RayController.veryFarFrontLethal)
                    {
                        jump();
                        //Debug.Break();
                    }
                }
                // otherwise simply move right, given that no object is detected immediately to the right
                else
                {
                    moveRight();
                    //Debug.Break();
                }
            }

            // Right lane decision making
            if (RayController.targettedLethal && laneNum == 1 && !controlLocked)
            {
                // Checks to see if object is detected in front left, or mid left
                if (RayController.midLeftLethal || RayController.frontLeftLethal)
                {
                    // If an object is also detected directly to the left, jump
                    if ((RayController.leftLethal || RayController.midLeftLethal) && !RayController.veryFarFrontLethal)
                    {
                        jump();
                        //Debug.Break();
                    }
                }
                // otherwise simply move left, given that no object is detected immediately to the right
                else
                {
                    moveLeft();
                    //Debug.Break();
                }
            }
        }

        timeCounter += Time.deltaTime;

        // Used to increase the game speed every x seconds.
        if (timeCounter >= 1)          // Default value 10.
        {
            incrementVel();             //Increase the speed by 0.2f
            timeCounter = 0;            //RESET timeCounter back to 0
        }

        // Ensures that the players position does not waver from the center of the lane using force.
        if (laneNum == 0)               // Only runs if the player is in the center lane.
        {
            if(rb.position.x > 0)       // If the player is too much to the right of the center lane, move left.
            {
                rb.AddForce(new Vector3(-1f, 0, 0));
            }

            if (rb.position.x < 0)      // If the player is too much to the left of the center lane, move right.
            {
                rb.AddForce(new Vector3(1f, 0, 0));
            }
        }
    }

    // Movement Function - Left
    public void moveLeft()
    {
        horizVel -= 2.0f;               // Alters the horizontal velocity to -2.0f.
        StartCoroutine(stopMoveX());    // Starts coroutine to ensure player cannot make another movement for 0.5s
        laneNum -= 1;                   // Ensures that the lane number decreases to -1. This also ensures that the if condition can be met to move left.
        controlLocked = true;           // Locks control so player cannot move.
    }

    // Movement Function - Right
    public void moveRight()
    {
        horizVel += 2.0f;               // Alters the horizontal velocity to 2.0f.
        StartCoroutine(stopMoveX());    // Starts coroutine to ensure player cannot make another movement for 0.5s
        laneNum += 1;                   // Ensures that the lane number increases to 1. This also ensures that the if condition can be met to move right.
        controlLocked = true;           // Locks control so player cannot move.
    }

    // Movement Function - Jump
    void jump()
    {
        Debug.Log("Jumping");
        rb.AddForce(new Vector3(0.0f, jumpHeight, 0.0f));   // Default Value for y-axis force = 250.0f --> Most realistic jump height
        isJumping = true;
        controlLocked = true;
    }

    //Collision detection for player
    void OnCollisionEnter(Collision other)
    {
        // Check to see if player is hitting a lethal object. All lethal objects must use the "Lethal" tag.
        if (other.gameObject.tag == "lethal")   
        {
            Debug.Break();
            Destroy(gameObject);    // Destroys player
            if (timer > maxScore)
            {
                maxScore = timer;
            }
            GMScript.resetGame();   // Reloads the game
        }

        // Used to remove the control lock from jumping, with gravity. isJumping allows us to ensure that the player cannot dobule jump.
        if (other.gameObject.tag == "floor" && isJumping == true)   
        {
            Debug.Log("Collided with floor");   
            controlLocked = false;      // Unlocks the control. Player can make movements again.
            isJumping = false;          // Resets isJumping so player can jump again.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "powerup"){
            Destroy(other.gameObject);
        }
    }

    private void incrementVel() // Function to increase game velocity.
    {
        if (forwVel < maxVel)   // Does not allow the speed to increase over maxVel
        {
            forwVel += 0.2f;    // Increments velocity. Default value 0.2f.
        }
        else
        {
            forwVel = maxVel;      // If the velocity exceeds maxVel, limit it to maxVel.
        }
    }

    //Here we stop the horizontal movement of the player after 0.5 seconds.
    IEnumerator stopMoveX()
    {
        yield return new WaitForSeconds(0.5f);   // Wait 0.5s
        horizVel = 0;                           // Reset horizontalVel to 0.
        if (laneNum == -1)                      // Check if player is in left lane
        {
            rb.position = new Vector3(-1f, rb.position.y, rb.position.z);   // Force position to center of left lane.
        }
        if (laneNum == 1)                       // Check if player is in right lane
        {
            rb.position = new Vector3(1f, rb.position.y, rb.position.z);    // Force position to center of right lane.
        }
        controlLocked = false;                  // Removes player control Lock.
        if (laneNum == 0)                       // Check if player is in right lane
        {
            rb.position = new Vector3(0f, rb.position.y, rb.position.z);    // Force position to center of right lane.
        }
        controlLocked = false;                  // Removes player control Lock.
    }
}
