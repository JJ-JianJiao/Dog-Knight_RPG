using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public GameObject requestQuestMark;
    public GameObject questDoneMark;

    public GameObject questPanel;

    public float detectDistance;
    // Start is called before the first frame update
    void Start()
    {
        detectDistance = 8.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (questPanel.GetComponent<QuestManager>().questFinish)
        {

            FindPlayer(questDoneMark);

        }
        else {

            FindPlayer(requestQuestMark);
        }
    }

    void FindPlayer(GameObject questMarkType) {

        //Physics.CheckSphere(transform.position, detectDistance);
        var colliders =  Physics.OverlapSphere(transform.position, detectDistance);
        foreach (var collider in colliders)
        {
            if (collider.transform.CompareTag("Player")) {

                //Debug.Log("I can give you my QUEST!");
                questMarkType.gameObject.SetActive(true);
                return;
            }
        }
        questMarkType.gameObject.SetActive(false);
    }
}
