using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
    public float linkeGrenze = 1.5f;

    void LateUpdate()
    {
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		Vector3 whereToGo = new Vector3(PlayerPos.x, PlayerPos.y+0.8f, -10f);
        transform.position = Vector3.Lerp(transform.position, whereToGo, Time.deltaTime * 8);


        if (transform.position.x <linkeGrenze)
			transform.position = new Vector3(linkeGrenze, PlayerPos.y+0.8f, -10);
    }
}
