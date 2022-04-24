using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManueManager : MonoBehaviour
{
    public GameObject InfoControl;
    public GameObject InfoCredit;
    public GameObject MenuBtns;

    public GameObject backBtn;

    private void OnEnable()
    {
        MenuBtns.SetActive(true);
        InfoControl.SetActive(false);
        InfoCredit.SetActive(false);
        backBtn.SetActive(false);

    }

    public void DisplayInfoControl() {

        MenuBtns.SetActive(false);
        InfoControl.SetActive(true);
        backBtn.SetActive(true);

    }

    public void DisplayInfoCredits()
    {

        MenuBtns.SetActive(false);
        InfoCredit.SetActive(true);
        backBtn.SetActive(true);
    }

    public void ExitBtnClick()
    {
        Application.Quit();
    }

    public void BackBtnClick()
    {

        MenuBtns.SetActive(true);
        InfoControl.SetActive(false);
        InfoCredit.SetActive(false);
        backBtn.SetActive(false);
    }

}
