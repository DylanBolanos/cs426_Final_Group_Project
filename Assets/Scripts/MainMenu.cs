using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start(){
    }
    
    public void StartClick(){
        SceneManager.LoadScene(1);
    }

    public void ReturnMain(){
        SceneManager.LoadScene(0);
    }

    public void QuitClick(){
        Application.Quit();
    }


}
