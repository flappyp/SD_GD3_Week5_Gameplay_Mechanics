using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private float fallThreshold = -30f; //if the player hits this threshold they will respawn

    public Vector3 respawnPosition; //setting the respawn position of the player

    

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallThreshold) 
        {
            respawn(); //if the position is greater than the fallThreshold than the player will respawn

        }
    }

    private void respawn() //respawn function

    {
        transform.position = respawnPosition; //setting  the player to the respawn position that can be set in the inspector

        Rigidbody rb = GetComponent<Rigidbody>(); // resetting the players rigidbody and the physics this will stop the player from moving after they have spawned.
        if (rb != null)
        {   
            rb.velocity = Vector3.zero;




        }
    }
}
