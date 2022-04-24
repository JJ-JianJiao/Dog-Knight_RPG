using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureController : MonoBehaviour
{
    //public Text treasureTaskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            var treasureText = GameObject.Find("TreasureText");
            if (treasureText != null)
            {
                treasureText.GetComponent<Text>().color = Color.green;
            }


            var treasureUI = GameObject.Find("TreasureUI");
            if (treasureUI != null)
            {

                treasureUI.transform.GetChild(0).gameObject.SetActive(true);

            }
            this.transform.parent.gameObject.SetActive(false);
        
        }
    }

}
