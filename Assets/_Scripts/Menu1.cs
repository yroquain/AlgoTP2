using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu1 : MonoBehaviour {

    public GameObject ContinuerButton;
    void Start()
    {
        if (PlayerPrefs.GetFloat("Princesse") == 1)
        {
            if(PlayerPrefs.GetFloat("RespectRouge")==0 || PlayerPrefs.GetFloat("RespectRouge") == 100)
            {
                ContinuerButton.SetActive(false);
            }
        }
        if (PlayerPrefs.GetFloat("Princesse") == 2)
        {
            if (PlayerPrefs.GetFloat("RespectBleu") == 0 || PlayerPrefs.GetFloat("RespectBleu") == 100)
            {
                ContinuerButton.SetActive(false);
            }
        }
        if (PlayerPrefs.GetFloat("Princesse") == 3)
        {
            if (PlayerPrefs.GetFloat("RespectVert") == 0 || PlayerPrefs.GetFloat("RespectVert") == 100)
            {
                ContinuerButton.SetActive(false);
            }
        }
        if (PlayerPrefs.GetFloat("Mort")==1)
        {
            ContinuerButton.SetActive(false);
        }
    }
	public void OnClickNewGame()
    {
        PlayerPrefs.SetFloat("Mort", 0);
        float princesse = Random.Range(0, 2.9f);
        if (princesse < 1)
        {
            PlayerPrefs.SetFloat("Princesse", 1);
        }
        else if (princesse < 2)
        {
            PlayerPrefs.SetFloat("Princesse", 2);
        }
        else if (princesse < 3)
        {
            PlayerPrefs.SetFloat("Princesse", 3);
        }
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
