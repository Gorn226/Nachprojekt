﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sieg : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col) 
    {
        Debug.Log("Hallo");
        if (col.transform.tag== "Player")
        {

            SceneManager.LoadScene("Sieg");
        }
    }
}
