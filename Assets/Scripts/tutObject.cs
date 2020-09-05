using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutObject : MonoBehaviour
{
    
    public GameObject finger3;
    public Tutorial tu;
  
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "left")
        {
            finger3.SetActive(true);
            tu.finger2.SetActive(false);
            tu.playBut.transform.SetAsLastSibling();
            tu.panel2.SetActive(true);
            GameObject.Find("play").GetComponent<Button>().interactable = true;
        }
    }
}
