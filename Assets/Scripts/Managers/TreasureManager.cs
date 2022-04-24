using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureManager : MonoBehaviour
{
    static TreasureManager instance;

    public Text treasureTaskText; 

    private void Awake()
    {
        if (instance != null)
        {

            Destroy(this);

        }
        instance = this;

    }

    public static void SetTreasureTaskText() {

        //treasureTaskText

    }

}
