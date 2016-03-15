using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public void OnRestart()
    {
        Application.LoadLevel(0);
    }
}
