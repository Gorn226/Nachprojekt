using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public enum potType
    {
        Small,
        Medium,
        Big,
    }
    public potType type;
    public Sprite pot;
    private int hpRecovery;

    void Start()
    {
        if (type == potType.Small)
            hpRecovery = 10;
        if (type == potType.Medium)
            hpRecovery = 20;
        if (type == potType.Big)
            hpRecovery = 30;
    }
}
