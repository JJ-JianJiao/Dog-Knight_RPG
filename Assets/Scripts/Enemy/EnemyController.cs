using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { PATROL, CHASE, DEAD}

public class EnemyController : MonoBehaviour
{
    public EnemyStates enemyStates;

    private NavMeshAgent agent;

    private Animator anim;

    private bool isChase;
    private bool isDie;
    private bool isAttack;

    // Start is called before the first frame update
    void Awake()
    {
        isChase = false;
        isDie = false;
        isAttack = false;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        //SwitchStates();
        SwitchAnimator();

    }

    void SwitchAnimator() {

        anim.SetBool("Chase", isChase);
        //anim.SetBool("Die", isDie);
        if (isDie)
        {
            isChase = false;
            isAttack = false;
            StartCoroutine(DieAnim());
        }
        //if (isAttack)
        StartCoroutine(AttackAnim());

    }

    IEnumerator DieAnim() {
        anim.SetBool("Die", isDie);
        yield return new WaitForSeconds(10.0f);
        gameObject.SetActive(false);

    }

    IEnumerator AttackAnim()
    {
        anim.SetBool("Attack", isAttack);
        //yield return new WaitForSeconds(10.0f);
        yield return null;
        //gameObject.SetActive(false);
        isAttack = false;

    }

    //void SwitchStates() {

    //    switch (enemyStates)
    //    {
    //        case EnemyStates.PATROL:

    //            break;
    //        case EnemyStates.CHASE:
    //            break;
    //        case EnemyStates.DEAD:
    //            break;
    //    }

    //}

    void FindPlayer()
    {

        //Physics.CheckSphere(transform.position, detectDistance);
        var colliders = Physics.OverlapSphere(transform.position, 12);
        foreach (var collider in colliders)
        {
            if (collider.transform.CompareTag("Player"))
            {
                if (!isDie)
                {
                    agent.destination = collider.transform.position;
                    isChase = true;
                }
                else
                {
                    agent.destination = this.transform.position;
                    isChase = false;
                }

                if (Vector3.Distance(gameObject.transform.position, collider.transform.position) < 3) {
                    agent.destination = this.transform.position;
                    isAttack = true;

                }


                return;
            }
        }
        agent.destination = this.transform.position;
        isChase = false;

    }

    void GetDieMessage() {

        isDie = true;
        isChase = false;
    }
}
