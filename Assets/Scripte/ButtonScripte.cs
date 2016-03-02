using UnityEngine;
using System.Collections;

public class ButtonScripte : MonoBehaviour {

    public void starten(string name)
    {
        Application.LoadLevel(name);
    }
    public void verlassen()
    {
        Application.Quit();
    }
}
