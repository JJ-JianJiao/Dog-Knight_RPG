using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBallAndOthers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name != "Ground")
        {
            if (this.transform.name == "IceMagic(Clone)")
                Destroy(this.gameObject);
        }
        if (other.transform.CompareTag("Enemy")) {

            if (this.transform.name == "IceMagic(Clone)")
            {
                Destroy(this.gameObject);
                other.transform.GetComponent<Health>().GetHit(10);
            }
            else if (this.transform.name == "SolidBall(Clone)") {

                Destroy(this.gameObject);
                other.transform.GetComponent<Health>().GetHit(50);
            }

        }

    }
}
