using UnityEngine;
using System.Collections;

public class WallCheck : MonoBehaviour {
    public GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        player.GetComponent<Charactercontroller>().isWall = true;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        player.GetComponent<Charactercontroller>().isWall = false;
    }
	}
