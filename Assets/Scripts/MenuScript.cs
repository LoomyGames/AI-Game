using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public void startClick() //if the user clicks on play, load the scene
    {
        SceneManager.LoadScene(1);
    }

    public void quitClick() //if the user clicks on quit, quit the game
    {
        Application.Quit();
    }
}
