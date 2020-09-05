using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator finishObject;
    int levelNumber;
    Animator finishAnim;
    public Text rightText, leftText, verticalText, horizontalText,levelCounterText;
    public int rightCount, leftCount, verticalCount, horizontalCount;
    public float speed=10f;
    Rigidbody2D rb;
   public  string objectAssistan;
   private  bool isSpawnObject = false,isGameOn=false,checkIfAddForceOn=false;
    public GameObject spawningObject,pausePanel,finPanel;
    public Color targetColor;
    void Start()
    {
       
        rb = gameObject.GetComponent<Rigidbody2D>();
        finishAnim = gameObject.GetComponent<Animator>();
        
    }

    void Spawn()
    {
        isSpawnObject = false;
        if(objectAssistan=="right")
            Instantiate(spawningObject, new Vector2(0, 0), transform.rotation * Quaternion.Euler(0, 0, 45));
        else if(objectAssistan=="left")
            Instantiate(spawningObject, new Vector2(0, 0), transform.rotation * Quaternion.Euler(0, 0, -45));
        else if(objectAssistan=="horizontal")
            Instantiate(spawningObject, new Vector2(0, 0), transform.rotation * Quaternion.Euler(0, 0, 0));
        else if(objectAssistan=="vertical")
            Instantiate(spawningObject, new Vector2(0, 0), transform.rotation * Quaternion.Euler(0, 0, 90));
    }


    void OnBecameInvisible()
    {
        Debug.Log("game over");
    }
    void Update()
    {
        
        levelNumber = SceneManager.GetActiveScene().buildIndex;

        rightText.text = rightCount.ToString("");
        leftText.text = leftCount.ToString("");
        horizontalText.text = horizontalCount.ToString("");
        verticalText.text = verticalCount.ToString("");


        if (isSpawnObject)
        {
           if(!isGameOn)
                Spawn();
            
        }
        //its for check if game is on, do not addforce again
        if (isGameOn)
        {
            checkIfAddForceOn = true;
        }

 
    }
    
    public void Right()
    {
        if (rightCount == 0)
        {
            GameObject.Find("rightText").GetComponent<Animator>().SetTrigger("rightanim");
        }
       else if (rightCount != 0)
        {
            isSpawnObject = true;
            objectAssistan = "right";
        }
        rightCount--;
        if (rightCount <= 0)
            rightCount = 0;
    }  
    public void Left()
    {
        if (leftCount == 0)
        {
            GameObject.Find("leftText").GetComponent<Animator>().SetTrigger("leftanim");
        }
        if (leftCount != 0)
        {

            isSpawnObject = true;
            objectAssistan = "left";
        }
        leftCount--;
        if (leftCount <= 0)
            leftCount = 0;
    }  
    public void Horizontal()
    {
        if (horizontalCount == 0)
        {
            GameObject.Find("horizontalText").GetComponent<Animator>().SetTrigger("horanim");
        }

        if (horizontalCount != 0)
        {
            isSpawnObject = true;
            objectAssistan = "horizontal";
        }
        horizontalCount--;
        if (horizontalCount <= 0)
            horizontalCount = 0;

    }
    public void Vertical()
    {
        if (verticalCount == 0)
        {
            GameObject.Find("verticalText").GetComponent<Animator>().SetTrigger("veranim");
        }

        if (verticalCount != 0)
        {
            isSpawnObject = true;
            objectAssistan = "vertical";
        }
        verticalCount--;
        if (verticalCount <= 0)
            verticalCount = 0;

    }
    public void Play()
    {
        if(!checkIfAddForceOn)
        rb.AddForce(transform.up * -speed);
        Destroy(GameObject.FindGameObjectWithTag("way"));
        isGameOn = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        
       
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);   
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);   
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");   
    }
    public void Restart()
    {
        if(Time.timeScale==0)
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            col.gameObject.GetComponent<SpriteRenderer>().material.color = targetColor;
        }
      

    }
    void  OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "finish")
        {
            Invoke("LevelScene", 1f);
            GameObject.Find("play").transform.SetAsFirstSibling();
         //  finishAnim.SetTrigger("finish");
          //  rb.simulated = false;
            finishObject.SetTrigger("finish");
            finPanel.SetActive(true);
            levelCounterText.text = levelNumber.ToString();
        }

    }
  
    void LevelScene()
    {
        levelCounterText.gameObject.SetActive(true);
     
        Invoke("LevelCount", 0.5f);
      
       
    }
    void LevelCount()
    {
        levelNumber++;
        
        levelCounterText.text = levelNumber.ToString();
        
        Invoke("PassLevel", 1f);
        
    }
    void PassLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
