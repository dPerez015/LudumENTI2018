using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    //other components
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    Camera cam;

    //states
    bool clicked;
    bool moving;

    //timebar
    public Timebar timeBar;
    float timeLeft;
    public float maxTime;

    //movement parameters
    public float MaxDistance;
    float dragAcceleration;
    public float velocity;


    //movement tracking
    float startMovementTime;

    //velocity control
    Vector2 actualVelocity;
    Vector2 initialVelocity;

    //mouse controls
    Vector3 mouseInitialPos;
    public float mouseMaxDrag;
    float mouseDragAmount;

    //alumnos
    int numOfAlumn;
    float dragAmount = 0.05f;

    //arrow 
    DirectionArrow arrow;
    

    private void Start()
    {
        //we get the componenets
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        //set states to false
        clicked = false;
        moving = false;

        //we get the times 
        //TODO(should be changed to getting them from singleton)
        maxTime = timeLeft = 100;

        numOfAlumn++;

        arrow = transform.GetChild(0).gameObject.GetComponent<DirectionArrow>();
        arrow.SetActive(false);
    }

    private void calculateDragAccel(float vel, float mDist)
    {
        dragAcceleration = -(vel * vel) / (2 * mDist);
    }

    void setVelocity(Vector2 v){
        rb2D.velocity = v;
        actualVelocity = v;
    }

    private void Update()
    {
        //clicked but not released
        if (clicked){
            //we check the direction and the percentage of the max amount a player
            Vector3 direction = mouseInitialPos - Input.mousePosition;
            mouseDragAmount = Mathf.Clamp(direction.magnitude/mouseMaxDrag,0,1);

            //rotate the gameobject
            transform.rotation = Quaternion.Euler(0,0,Mathf.Rad2Deg*Mathf.Atan2(direction.y, direction.x));

            //scale the arrow
            arrow.setScale(mouseDragAmount);

            //we show the player how much will that movement cost
            timeBar.showHealthLossPercent((timeLeft - (1*mouseDragAmount))/maxTime);

            //cuando deja ir el click
            if (Input.GetMouseButtonUp(0))
            {
                //cambiamos el color 
                //TODO activar animación
                spriteRenderer.color = new Color(1, 1, 1);
                //seteamos la velocidad
                setVelocity(direction.normalized * velocity);
                initialVelocity=actualVelocity;

                //calculamos el drag necesario para que se mueva solo esa distancia
                calculateDragAccel(velocity, MaxDistance * mouseDragAmount*(1-(numOfAlumn*dragAmount)));

                //cambiamos de estado
                clicked = false;
                moving = true;

                //seteamos el tiempo inicial del moviemiento, necesario para los calculos del mrua
                startMovementTime = Time.time;
                
                //we set the new time to the timebar
                //TODO get this value from singleton
                timeLeft-=1*mouseDragAmount;
                timeBar.setHealthPercent(timeLeft/maxTime);

                //desactivamos la flecha
                arrow.SetActive(false);
            }
        }
        //cuando se esta moviendo 
        else if (moving)
        {
            //seteamos la velocidad según el tiempo que ha transcurrido desde el inicio
            setVelocity(initialVelocity.normalized * (velocity + (dragAcceleration * (Time.time - startMovementTime))));
            if (checkMovementStop())
            {
                moving = false;
                setVelocity(new Vector2(0, 0));
            }
        }
    }

    private bool checkMovementStop()
    {
        if (actualVelocity.magnitude <= 0.01)
            return true;
        else if (Vector2.Dot(actualVelocity, initialVelocity) <0)
            return true;
        return false;
    }


    private void OnMouseDown()
    {
        if (!moving)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            mouseInitialPos = cam.WorldToScreenPoint(transform.position);
            clicked = true;
            arrow.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Collider")
        {
            //setVelocity(collision.gameObject.GetComponent<BounceOfCollider>().GetDirection(actualVelocity));
            setVelocity(Vector2.Reflect(actualVelocity, collision.GetContact(0).normal));
            initialVelocity = actualVelocity;
            //rotate the gameobject
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(actualVelocity.y, actualVelocity.x));
        }
        else if(collision.gameObject.tag == "Alumno"){
            

            float movementLeft = mouseDragAmount* MaxDistance*(1- (dragAmount * numOfAlumn ))- (initialVelocity.magnitude * startMovementTime + dragAcceleration * startMovementTime * startMovementTime / 2);
            Debug.Log((velocity * startMovementTime + dragAcceleration * startMovementTime * startMovementTime / 2));
            numOfAlumn++;
            
            calculateDragAccel(actualVelocity.magnitude, movementLeft);
            collision.gameObject.SetActive(false);
        }
     }
}

