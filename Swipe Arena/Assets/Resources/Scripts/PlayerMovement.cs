using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    string buttonPressed;

    public const string RIGHT = "right";
    public const string LEFT = "left";
    public const string UP = "up";
    public const string DOWN = "down";
    public const string TAP = "tap";

    PhotonView view;


    // Update is called once per frame
    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (view.IsMine)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                currentPosition = Input.GetTouch(0).position;
                Vector2 Distance = currentPosition - startTouchPosition;

                if (!stopTouch)
                {

                    if (Distance.x < -swipeRange) //left
                    {
                        buttonPressed = LEFT;
                        stopTouch = true;
                    }
                    else if (Distance.x > swipeRange) //right
                    {
                        buttonPressed = RIGHT;
                        stopTouch = true;
                    }
                    else if (Distance.y > swipeRange) //up
                    {
                        buttonPressed = UP;
                        stopTouch = true;
                    }
                    else if (Distance.y < -swipeRange) //down
                    {
                        buttonPressed = DOWN;
                        stopTouch = true;
                    }

                }

            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                stopTouch = false;

                endTouchPosition = Input.GetTouch(0).position;

                Vector2 Distance = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange)
                {
                    buttonPressed = TAP;
                }

            }
        }

        
    }

    void FixedUpdate()
    {

        if (view.IsMine)
        {
            if(buttonPressed == RIGHT)
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
            else if(buttonPressed == LEFT)
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
            }
            else if(buttonPressed == UP)
            {
                rb.velocity = new Vector2(0, moveSpeed);
            }
            else if(buttonPressed == DOWN)
            {
                rb.velocity = new Vector2(0, -moveSpeed);
            }
            else if(buttonPressed == TAP)
            {
                rb.velocity = new Vector2(0, 0);
            }
        }

        
    }
}

