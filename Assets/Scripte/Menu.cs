using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    public void starten(string name)
    {
        Application.LoadLevel(name);
    }
    public void verlassen()
    {
        Application.Quit();
    }

}
