using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    //other components
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;
    Camera cam;
    LevelManager manager;
    Animator anim;

    //states
    bool clicked;
    bool moving;
    public bool started;

    [Header("Timebar")]
    public TimebarArray timeBar;
    float timeLeft;
    public float maxTime;

    [Header("Movement parameters")]
    public float MaxDistance;
    float dragAcceleration;
    public float velocity;
    [System.NonSerialized]
    public float dragMultiplier;
    public float dragMultiplierSpeed;
    public float dragMultiplierSlow;


    //movement tracking
    float startMovementTime;
    Vector3 position;

    //velocity control
    Vector2 actualVelocity;
    Vector2 initialVelocity;

    [Header("Controls")]
    [Tooltip("Distancia maxima en pixeles que puedes mover el raton para ganar potencia de tiro, apartir de aqui ya no aumentara mas")]
    public float mouseMaxDrag;
    Vector3 mouseInitialPos;
    
    float mouseDragAmount;

    [Header("Alumnos")]
    [Tooltip("Porcentage de la distancia maxima que pierde por alumno recogido")]
    public float dragAmount = 0.05f;
    public int numOfAlumn;

    //arrow 
    DirectionArrow arrow;
    

    private void Start()
    {
        //we get the componenets
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        manager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        anim = GetComponent<Animator>();
        //set states to false
        clicked = false;
        moving = false;
        started = false;

        //we get the times 
        maxTime =  Timemanager.Instance.getMaxTime()/timeBar.getNumberOfBars();
        timeLeft = Timemanager.Instance.getCurrentTime();
        timeBar.setHealthPercent(timeLeft / maxTime);

        arrow = transform.GetChild(0).gameObject.GetComponent<DirectionArrow>();
        arrow.SetActive(false);

        dragMultiplier = 1;
    }
    public float getTimeLeft()
    {
        return timeLeft;
    }

    private void calculateDragAccel(float vel, float mDist)
    {
        dragAcceleration = -(vel * vel) / (2 * mDist);
    }

    public void reload()
    {
        transform.position = manager.getInitialPos();
        //anim.Play("Idle");
    }

    public void setVelocity(Vector2 v){
        rb2D.velocity = v;
        actualVelocity = v;
    }

    private void Update()
    {
        if (started)
        {
            //clicked but not released
            if (clicked)
            {
                //we check the direction and the percentage of the max amount a player
                Vector3 direction = mouseInitialPos - Input.mousePosition;
                mouseDragAmount = Mathf.Clamp(direction.magnitude / mouseMaxDrag, 0, 1);

                //rotate the gameobject
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x));

                //scale the arrow
                arrow.setScale(mouseDragAmount);

                //we show the player how much will that movement cost
                timeBar.showHealthLossPercent((timeLeft - (1 + MaxDistance * mouseDragAmount * (1 - numOfAlumn * dragAmount))) / maxTime);

                //change scale
                transform.localScale = new Vector3(Mathf.Lerp(1, 1.8f, mouseDragAmount), Mathf.Lerp(1, 0.7f, mouseDragAmount), 1);
                anim.SetFloat("MouseDragAmount", mouseDragAmount);

                //cuando deja ir el click
                if (Input.GetMouseButtonUp(0))
                {
                    position = transform.position;
                    //cambiamos el color 
                    //TODO activar animación
                    spriteRenderer.color = new Color(1, 1, 1);
                    //seteamos la velocidad
                    setVelocity(direction.normalized * velocity);
                    initialVelocity = actualVelocity;

                    //calculamos el drag necesario para que se mueva solo esa distancia
                    calculateDragAccel(velocity, MaxDistance * mouseDragAmount * (1 - (numOfAlumn * dragAmount)));

                    //cambiamos de estado
                    clicked = false;
                    moving = true;

                    //seteamos el tiempo inicial del moviemiento, necesario para los calculos del mrua
                    startMovementTime = Time.time;

                    //we set the new time to the timebar
                    timeLeft -=1 + MaxDistance * mouseDragAmount * (1-numOfAlumn *dragAmount);
                    //Timemanager.Instance.setCurrentTime(timeLeft);
                    timeBar.setHealthPercent(timeLeft / maxTime);

                    //desactivamos la flecha
                    arrow.SetActive(false);

                    //Iniciamos la animacion
                    anim.SetBool("Clicked", false);
                    anim.SetBool("Moving", true);
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            //cuando se esta moviendo 
            else if (moving)
            {
                //seteamos la velocidad según el tiempo que ha transcurrido desde el inicio
                setVelocity(initialVelocity.normalized * (velocity + (dragAcceleration*dragMultiplier * (Time.time - startMovementTime))));
                if (checkMovementStop())
                {
                    moving = false;
                    setVelocity(new Vector2(0, 0));
                    GetComponent<Animator>().SetBool("Moving", false);

                    if (timeLeft <= 0)
                        SceneManager.LoadScene("GameOver");
                }
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
        if (!moving && started)
        {
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            mouseInitialPos = cam.WorldToScreenPoint(transform.position);
            clicked = true;
            arrow.SetActive(true);

            anim.SetBool("Clicked", true);
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
        else if(collision.gameObject.tag == "Pinchos")
        {
            setVelocity(new Vector2(0, 0));
            moving = false;
            anim.SetBool("Moving",false);
        }
        else if(collision.gameObject.tag == "Fuego")
        {
            setVelocity(new Vector2(0,0));
            moving = false;
            anim.Play("SlimeDie");
            collision.gameObject.SetActive(false);
        }
     }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            anim.SetBool("Hit", true);
        }

        if (other.gameObject.tag == "Alumno")
        {
            //float movementLeft = mouseDragAmount * MaxDistance * (1 - (dragAmount * numOfAlumn)) - (initialVelocity.magnitude * (Time.time - startMovementTime) + dragAcceleration *Mathf.Pow(Time.time-startMovementTime,2)/ 2);
            numOfAlumn++;

            //calculateDragAccel(actualVelocity.magnitude, movementLeft);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "EndOfLevel")
        {
            manager.endLevel();
            anim.SetBool("Moving", false);
        }
        else if (other.gameObject.tag == "SlowLiquid")
        {
            dragMultiplier = dragMultiplierSlow;
        }
        else if (other.gameObject.tag == "SpeedLiquid")
        {
            dragMultiplier = dragMultiplierSpeed;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.isTrigger)
            anim.SetBool("Hit", false);

        if (collision.gameObject.tag == "LoseAlum")
        {
            if (numOfAlumn > 0)
            {
                manager.spawnAlumno(collision.transform.position, transform.rotation);
                float movementLeft = mouseDragAmount * MaxDistance * (1 - (dragAmount * numOfAlumn)) - (initialVelocity.magnitude * (Time.time - startMovementTime) + dragAcceleration * Mathf.Pow(Time.time - startMovementTime, 2) / 2);
                numOfAlumn--;
                calculateDragAccel(actualVelocity.magnitude, movementLeft);

            }
        }
        else if (collision.gameObject.tag == "SlowLiquid")
        {
            dragMultiplier = 1;
        }
        else if (collision.gameObject.tag == "SpeedLiquid")
        {
            dragMultiplier = 1;
        }

    }
}

