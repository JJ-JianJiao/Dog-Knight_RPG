using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    private float rotationSpeed;

    public GameObject treasureSign;
    public GameObject MapUI;

    private Vector3 treasureSignPosition;

    // Start is called before the first frame update
    void Start()
    {
        treasureSignPosition = new Vector3(15.8f, 27.8f, 17.78f);
        rotationSpeed = 150.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var mapUI = GameObject.Find("MapUI");
            if (mapUI != null) {

                mapUI.transform.GetChild(0).gameObject.SetActive(true);

            }

            this.gameObject.SetActive(false);
            //treasureSign.gameObject.SetActive(true);
            var treasure = Instantiate(treasureSign, treasureSignPosition, Quaternion.Euler(90, 0, 0));
            //Instantiate(treasureSign, treasureSignPosition, Quaternion.identity);
            if (treasure.activeSelf == false) {
                treasure.SetActive(true);
            }

        }
    }
}
