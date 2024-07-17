using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Vector2 velocity;
    public float speed;

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            //rigidbody.MovePosition(rigidbody.position - velocity * Time.deltaTime);
            playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            //rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
            playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
        }
    }
}
