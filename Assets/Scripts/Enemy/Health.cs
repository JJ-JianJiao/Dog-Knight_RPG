using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float fullHealth;

    public GameObject RockMan;
    public GameObject Orc;
    public GameObject Turtle;

    public GameObject map;

    private bool isDead;

    private Animator anim;

    private bool isMapCreate;

    // Start is called before the first frame update
    void Start()
    {
        isMapCreate = false;
        isDead = false;
        switch (gameObject.transform.name)
        {
            case "RockMan":
                fullHealth = 200;
                break;
            case "Turtle":
                fullHealth = 150;
                break;
            case "Orc":
                fullHealth = 100;
                break;
        }
        currentHealth = fullHealth;
        anim = gameObject.transform.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHealth == 0) {

            Death();

        }
    }

    private void Death()
    {
        //Debug.Log(gameObject.transform.name + "   Dead!");
        isDead = true;
        BroadcastMessage("GetDieMessage");
        //this.gameObject.SetActive(false);
        if (!isMapCreate && this.gameObject.name == "Turtle" && Orc.gameObject.GetComponent<Health>().isDead && RockMan.gameObject.GetComponent<Health>().isDead) {

            CreatMap();
            isMapCreate = true;
            //RenderSettings.fogColor = new Color(0 / 255.0f, 111 / 255.0f, 100 / 255.0f, 255 / 255.0f);

        }
        else if (!isMapCreate && this.gameObject.name == "RockMan" && Orc.gameObject.GetComponent<Health>().isDead && Turtle.gameObject.GetComponent<Health>().isDead)
        {
            CreatMap();
            isMapCreate = true;
            //RenderSettings.fogColor = new Color(0 / 255.0f, 111 / 255.0f, 100 / 255.0f, 255 / 255.0f);

        }
        else if (!isMapCreate && this.gameObject.name == "Orc" && Turtle.gameObject.GetComponent<Health>().isDead && RockMan.gameObject.GetComponent<Health>().isDead)
        {
            CreatMap();
            isMapCreate = true;
            //RenderSettings.fogColor = new Color(0 / 255.0f, 111 / 255.0f, 100 / 255.0f, 255 / 255.0f);

        }
    }

    private void CreatMap()
    {
        var mapInstant = Instantiate(map, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), Quaternion.identity);
    }

    public void GetHit(int damage) {

        currentHealth -= damage;
        if (currentHealth <= 0) {
            currentHealth = 0;
        }
    
    }
}
