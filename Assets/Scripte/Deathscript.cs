using UnityEngine;
using System.Collections;

public class Deathscript : MonoBehaviour
{

    public GameObject player;

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("Hallo");
        if (otherObject.tag == "Player")
        {
            otherObject.gameObject.GetComponent<Charactercontroller>().andStayDead();
            
        }
    }
}

