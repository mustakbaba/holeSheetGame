using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AssitantObjects : MonoBehaviour
{
    float deltaX, deltaY;
    public bool isTouched = false;
    public Color deleteColor;
    bool isDelete = false;
    public Player playerScript;
    Rigidbody2D rb;


    bool moveAllowed = false;
    private Vector3 offset;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            playerScript = GameObject.Find("player").GetComponent<Player>();
        }


            rb = GetComponent<Rigidbody2D>();


      


    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "deleter")
        {
            isDelete = true;
            gameObject.GetComponent<SpriteRenderer>().material.color = deleteColor;
        }
    } 
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "deleter")
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
            isDelete = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
     
        if (gameObject.transform.rotation.eulerAngles.z < 50 && gameObject.transform.rotation.eulerAngles.z > 40)
        {
            gameObject.tag = "right";
        }
        if (gameObject.transform.rotation.eulerAngles.z < 5 && gameObject.transform.rotation.eulerAngles.z > -5)
        {
            gameObject.tag = "horizontal";
        }
        if (gameObject.transform.rotation.eulerAngles.z < 95 && gameObject.transform.rotation.eulerAngles.z > 85)
        {
            gameObject.tag = "vertical";
        }  
        if (gameObject.transform.rotation.eulerAngles.z  < 320 && gameObject.transform.rotation.eulerAngles.z  > 310)
        {
          
            gameObject.tag = "left";
        }
      




        if (isTouched)
        {
            
            if (SceneManager.GetActiveScene().buildIndex != 1)
            {
                GameObject.Find("deleter").GetComponent<Animator>().SetTrigger("deleterup");
            }
        }
        #region MobileTouch
        // Initiating touch event
        // if touch event takes place
        if (Input.touchCount > 0)
        {

            
            // get touch position
            Touch touch = Input.GetTouch(0);


            // obtain touch position
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);


            // get touch to take a deal with
            switch (touch.phase)
            {


                // if you touches the screen
                case TouchPhase.Began:


                    // if you touch the ball
                    if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        

                        // get the offset between position you touches
                        // and the center of the game object
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;


                        // if touch begins within the ball collider
                        // then it is allowed to move
                        moveAllowed = true;


                        // restrict some rigidbody properties so it moves
                        // more  smoothly and correctly
                        rb.freezeRotation = true;
                        rb.velocity = new Vector2(0, 0);
                        rb.gravityScale = 0;
                        GetComponent<CircleCollider2D>().sharedMaterial = null;
                    }
                    break;


                // you move your finger
                case TouchPhase.Moved:

                    // if you touches the ball and movement is allowed then move
                    if (GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(touchPos) && moveAllowed)
                    {
                        isTouched = true;
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }

                    break;


                // you release your finger
                case TouchPhase.Ended:
                    isTouched = false;
                    if (SceneManager.GetActiveScene().buildIndex != 1)
                        GameObject.Find("deleter").GetComponent<Animator>().SetTrigger("deleterdown");
                    // restore initial parameters
                    // when touch is ended

                    break;
            }
        }
        #endregion MobileTouch
    }

    #region PcClick
    void OnMouseDown()
    {
        
        offset = gameObject.transform.position -
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            GameObject.Find("deleter").GetComponent<Animator>().SetTrigger("deleterup");
        }
    }

    void OnMouseDrag()
    {
        
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }


    void OnMouseUp()
    {
        if(SceneManager.GetActiveScene().buildIndex!=1)
        GameObject.Find("deleter").GetComponent<Animator>().SetTrigger("deleterdown");
        if (isDelete)
        {
            

            switch (gameObject.tag)
            {
                case "left":
                    Destroy(gameObject);
                    playerScript.leftCount++;
                    break;
                case "right":
                    Destroy(gameObject);
                    playerScript.rightCount++;
                    break;

                case "horizontal":
                    Destroy(gameObject);
                    playerScript.horizontalCount++;
                    break;
                case "vertical":
                    Destroy(gameObject);
                    playerScript.verticalCount++;
                    break;

            }
            
        }
    }

    #endregion PcClick
}


