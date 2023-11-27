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
    public void MoveToLevelAR1()
    {
        SceneManager.LoadScene(5);
    }
    public void MoveToLevelAR2()
    {
        SceneManager.LoadScene(6);
    }
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(4);
    }
    public void backToStart()
    {
        SceneManager.LoadScene(0);
    }
    public void optionsScreen()
    {
        SceneManager.LoadScene(3);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
