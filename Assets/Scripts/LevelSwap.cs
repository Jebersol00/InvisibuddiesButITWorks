using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwap : MonoBehaviour
{
    public void MoveToLevel1(){
        SceneManager.LoadScene(1);
    }
    public void MoveToLevel2(){
        SceneManager.LoadScene(2);
    }
    public void backToStart()
    {
        SceneManager.LoadScene(0);
    }
    public void optionsScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void LevelScreen()
    {
        SceneManager.LoadScene(4);
    }
}
