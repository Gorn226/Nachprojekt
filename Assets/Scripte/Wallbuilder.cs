using UnityEngine;
using System.Collections;

public class Wallbuilder : MonoBehaviour {
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public float chance1;
    public float chance2;
    public float chance3;
    public float chance4;
  
    public int right;
    public int up;
    public GameObject background;
    private GameObject g;
	// Use this for initialization
	void Start () {
        Vector3 vec = transform.position;
        float scaleRight = sprite1.bounds.extents.y*2*background.transform.localScale.y;
        float scaleUp = sprite1.bounds.extents.x*2*background.transform.localScale.x;
        for (int i = 0; i < right; i++)
        {
            vec.y = transform.position.y;
            for(int j =0;j <up;j++)
            {
                background.GetComponent<SpriteRenderer>().sprite = getSprite();
                g = (GameObject)Instantiate(background, vec, Quaternion.identity);
                g.transform.parent = this.gameObject.transform;
                vec.y += scaleUp;
            }
            vec.x += scaleRight;
        }
        }
    private Sprite getSprite()
    {
        float gesamtChance = chance1 + chance2 + chance3 + chance4;
        chance1 = chance1 / gesamtChance;
        chance2 = chance2 / gesamtChance;
        chance3 = chance3 / gesamtChance;
        chance4 = chance4 / gesamtChance;
        float rand = Random.value;
        
        if(rand <=chance1)
        {
            return sprite1;
        }
        if(rand <= chance2+chance1)
        {
            return sprite2;
        }
        if (rand <= chance1 + chance2 + chance3)
        {
            return sprite3;
        }
        return sprite4;
    }
	// Update is called once per frame

}
