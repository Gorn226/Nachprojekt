using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
    public float linkeGrenze = 1.5f;

    void LateUpdate()
    {
        float PlayerX = GameObject.FindGameObjectWithTag("Player").transform.position.x;
        Vector3 whereToGo = new Vector3(PlayerX, 2.8f, -10f);
        transform.position = Vector3.Lerp(transform.position, whereToGo, Time.deltaTime * 8);


        if (transform.position.x <linkeGrenze)
            transform.position = new Vector3(linkeGrenze, 2.8f, -10);
    }
}
