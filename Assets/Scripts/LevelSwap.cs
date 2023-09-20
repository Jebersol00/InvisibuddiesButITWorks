using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
