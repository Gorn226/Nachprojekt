using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// Use this for initialization
	public string name;


	void OnCollisionEnter2D(Collision2D col) 
	{
		Debug.Log("Hallo");
		if (col.transform.tag== "Player")
		{
			load_scene (name);

		}
	}

	public void load_scene(string name){
		Application.LoadLevel (name);
	}
}
