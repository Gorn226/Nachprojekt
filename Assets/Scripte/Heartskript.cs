using UnityEngine;
using System.Collections;

public class Heartskript : MonoBehaviour {
    public GameObject[] hearts;
    public GameObject[] halfHearts;
	// Use this for initialization
    void Awake()
    {
        for(int i =0; i<hearts.Length;i++)
        {
            halfHearts[i].SetActive(true);
            hearts[i].SetActive(true);
        }
    }
    public void setHearts(int health)
    {
        Debug.Log(health);
        int fullhearts = health/2;
        int halfhearts;
        if (health%2==1)
        {
            halfhearts = health / 2 + 1;
        }
        else 
        {
            halfhearts = health/2;
        }
        for (int i = hearts.Length - 1; i >= fullhearts; i--)
        {
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < fullhearts; i++)
        {
            hearts[i].SetActive(true);
        }
            for (int i = hearts.Length - 1; i >= halfhearts; i--)
            {
                halfHearts[i].SetActive(false);
            }
            for (int i = 0; i < halfhearts; i++)
            {
                halfHearts[i].SetActive(true);
            }
    }
}
