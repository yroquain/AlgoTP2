using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu1 : MonoBehaviour {

	public void OnClickNewGame()
    {
        PlayerPrefs.SetFloat("Gold", 0);
        PlayerPrefs.SetFloat("RespectRouge", 50);
        PlayerPrefs.SetFloat("RespectBleu", 50);
        PlayerPrefs.SetFloat("RespectVert", 50);
        SceneManager.LoadScene(1);
    }
	
	public void OnClickContinue()
    {
        SceneManager.LoadScene(1);
    }
}
