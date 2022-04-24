using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMovement : MonoBehaviour
{
    private float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 300;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
    }
}
