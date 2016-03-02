using UnityEngine;
using System.Collections;

public class Heartskript : MonoBehaviour {
    public GameObject[] hearts;
	// Use this for initialization
    void Awake()
    {
        for(int i =0; i<hearts.Length;i++)
        {
            hearts[i].SetActive(true);
        }
    }
    public void setHearts(int health)
    {
        Debug.Log("Hallo");
        for (int i = hearts.Length - 1; i >= health; i--)
        {
            hearts[i].SetActive(false);
        }
    }
}
