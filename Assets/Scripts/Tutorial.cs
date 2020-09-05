using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject finger,panel,tutorialObject,finger2,playBut,panel2;
    public tutObject tutAccess;
   
    public void FirstTut()
    {
        
            finger.SetActive(false);
            panel.SetActive(false);
            tutorialObject.SetActive(true);
            finger2.SetActive(true);
            
        

    }
    public void LastTut()
    {
        panel2.SetActive(false);
        tutAccess.finger3.SetActive(false);
        
    }
}
