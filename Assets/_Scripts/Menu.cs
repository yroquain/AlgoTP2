using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void OnRestart()
    {
        SceneManager.LoadScene(1);
    }
    public void OnMenuClick()
    {
        SceneManager.LoadScene(0);
    }
}
