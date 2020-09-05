using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator playButtonAnime;
   
    public void PlayGame()
    {
        playButtonAnime.SetTrigger("play");
        IEnumerator MyMethod()
        {
           
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(1);
        }
        StartCoroutine(MyMethod());
    }
    
}
