using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollower : MonoBehaviour
{
    /// <RainFollowScript>
    /// The point of this script is to have the rain follow the player at a slight offset
    /// this makes it so that as the player runs the rain doesn't match u perfectly, makes the rain more believeable as u run past some particles
    /// <>

    //rainSmoothSpeed is how fast the rain will smoothdamp to the player pos, .2 seconds right now
    private float rainSmoothSpeed = .2f;
    //player
    public Transform player;
    //player pos
    Vector3 currentPlayerPos;
    private Vector3 velocity = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        //player pos but I'm adding -20 to the X because it seems like the particle zone does not use the center, offset to keep player near the center of the particle zone
        //also setting y to 15 to keep it above player (its rain)
        currentPlayerPos = new Vector3(player.localPosition.x - 1, 30, player.localPosition.z);

        //do da smoothdamp to player pos
        transform.position =  Vector3.SmoothDamp(transform.position, currentPlayerPos, ref velocity, rainSmoothSpeed);

    }
}