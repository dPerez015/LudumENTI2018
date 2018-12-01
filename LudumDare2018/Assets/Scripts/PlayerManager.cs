using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;

    public Timebar timeBar;
    float timeLeft;
    float maxTime;

    Vector2 actualVelocity;

    bool clicked;
    bool moving;

    public float MaxDistance;
    float dragAcceleration;

    float startTime;
    Vector3 intialPos;
    Vector3 mouseInitialPos;
    public float mouseMaxDrag;

    public float velocity;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        clicked = false;
        moving = false;

        maxTime = timeLeft = 100;

        dragAcceleration = -(velocity * velocity) / (2 * MaxDistance);
    }

    void setVelocity(Vector2 v){
        rb2D.velocity = v;
        actualVelocity = v;
    }

    private void Update()
    {
        if (clicked){
            Vector3 direction = mouseInitialPos - Input.mousePosition;
            float mouseDragAmount = Mathf.Clamp(direction.magnitude/mouseMaxDrag,0,1);
            timeBar.showHealthLossPercent((timeLeft - (1*mouseDragAmount))/maxTime);

            if (Input.GetMouseButtonUp(0))
            {  
                setVelocity(direction.normalized * velocity);

                clicked = false;
                moving = true;
                startTime = Time.time;
                timeLeft-=1*mouseDragAmount;
                timeBar.setHealthPercent(timeLeft/maxTime);

            }
        }
        if (moving)
        {
            setVelocity(actualVelocity.normalized * (velocity + (dragAcceleration * (Time.time - startTime))));
            if (rb2D.velocity.magnitude <= 0.1)
            {
                moving = false;
                setVelocity(new Vector2(0, 0));
            }
        }
    }


    private void OnMouseDown()
    {
       spriteRenderer.color = new Color(0.5f,0.5f,0.5f);
        mouseInitialPos = Input.mousePosition;
        clicked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collider")
        {
            //setVelocity(collision.gameObject.GetComponent<BounceOfCollider>().GetDirection(actualVelocity));
            setVelocity(Vector2.Reflect(actualVelocity, collision.GetContact(0).normal));
        }
    }
}

