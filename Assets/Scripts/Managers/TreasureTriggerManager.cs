using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureTriggerManager : MonoBehaviour
{
    public GameObject treasure;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")) {

            treasure.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        
        }
    }
}
