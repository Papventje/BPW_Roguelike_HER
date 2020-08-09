using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Normal, Charging, Dashing, Entering }

public class PlayerMovement : MonoBehaviour
{
    public float chargeSpeed;
    public float normalSpeed;

    public float dashDistance;

    Rigidbody2D rb;

    Vector2 movement;
    Vector2 mousePos;
    Vector2 lastDir = new Vector2(1,0);

    private bool dash;

    Camera cam;

    Animator anim;

    public PlayerState currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement != new Vector2(0, 0))
        {
            lastDir = movement;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentState == PlayerState.Normal)
        {
            dash = true;
        }
    }

    private void FixedUpdate()
    {
        switch (currentState) 
        {
            case PlayerState.Normal:
                {
                    rb.MovePosition(rb.position + movement * normalSpeed * Time.deltaTime);

                    Vector2 lookDir = mousePos - rb.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

                    rb.rotation = angle;

                    if (dash)
                    {
                        currentState = PlayerState.Dashing;
                    }
                }
                break;

            case PlayerState.Charging:
                {
                    rb.MovePosition(rb.position + movement * chargeSpeed * Time.deltaTime);

                    Vector2 lookDir = mousePos - rb.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

                    rb.rotation = angle;
                }
                break;

            case PlayerState.Dashing:
                {
                    anim.Play("Player_DashAnim", -1, 0);

                    RaycastHit2D hit = Physics2D.Raycast(transform.position, lastDir, dashDistance + 1);
                    if (hit.collider == null)
                    {
                        transform.position = new Vector2(transform.position.x + lastDir.x * dashDistance, transform.position.y + lastDir.y * dashDistance);
                    }
                    else
                    {
                        transform.position = hit.point;
                    }

                    dash = false;
                    currentState = PlayerState.Normal;
                }
                break;

            case PlayerState.Entering:
                break;
        }
    }
}
