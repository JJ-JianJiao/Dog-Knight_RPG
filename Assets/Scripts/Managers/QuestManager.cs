using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public GameObject RockMan;
    public GameObject Turtle;
    public GameObject Orc;

    public Text rockmanText;
    public Text turtleText;
    public Text orcText;
    public Text Treasure;

    private bool rockmanDie;
    private bool turtleDie;
    private bool OrcDie;

    private bool showTreasure;
    private bool getTreasure;

    public bool questFinish;


    private void Awake()
    {
        RockMan.gameObject.SetActive(true);
        Turtle.gameObject.SetActive(true);
        Orc.gameObject.SetActive(true);

        rockmanDie = false;
        turtleDie = false;
        OrcDie = false;
        showTreasure = false;
        getTreasure = false;
        questFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (RockMan.gameObject.GetComponent<Health>().currentHealth == 0 && !rockmanDie) {
            rockmanDie = true;
            rockmanText.color = Color.green;
        }
        if (Turtle.gameObject.GetComponent<Health>().currentHealth == 0 && !turtleDie)
        {
            turtleDie = true;
            turtleText.color = Color.green;
        }
        if (Orc.gameObject.GetComponent<Health>().currentHealth == 0 && !OrcDie)
        {
            OrcDie = true;
            orcText.color = Color.green;
        }

        if (!showTreasure && RockMan.gameObject.GetComponent<Health>().currentHealth == 0 && Turtle.gameObject.GetComponent<Health>().currentHealth == 0 && Orc.gameObject.GetComponent<Health>().currentHealth == 0) {
            Treasure.gameObject.SetActive(true);
            showTreasure = true;
        }

        if (rockmanDie && turtleDie && OrcDie && Treasure.color == Color.green && !getTreasure) {

            getTreasure = !getTreasure;
            questFinish = true;
        }
    }
}
