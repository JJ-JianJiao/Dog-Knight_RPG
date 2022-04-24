using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    //DialogManager instance;

    [Header("UI Component")]
    public Text textLabel;
    public Image faceImage;

    [Header("Text file")]
    public TextAsset questStartFile;
    public TextAsset questDoneFile;
    public int index;
    public float textSpeed;

    bool textFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    [Header("Head Image")]
    public Sprite face01, face02;

    [Space]
    public GameObject player;
    public Camera dialogCam;

    [Space]
    public GameObject questPanel;
    public Text titleQuest;

    private void Awake()
    {
        //if (instance != null) {

        //    Destroy(gameObject);
        //}
        //instance = this;
        GetTextFromFile(questStartFile);
    }
    private void OnEnable()//onEnable 会在start之前调用
    {
        //textLabel.text = textList[index];
        //index++;
        if (questPanel.GetComponent<QuestManager>().questFinish) {

            GetTextFromFile(questDoneFile);
            titleQuest.color = Color.green;
        }

        StartCoroutine(SetTextUI());

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0) && index == textList.Count)
        if (Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            questPanel.gameObject.SetActive(true);
            gameObject.SetActive(false);
            player.GetComponent<PlayerController>().isDialog = false;
            dialogCam.gameObject.SetActive(false);
            index = 0;

            //set Fog
            if (!questPanel.GetComponent<QuestManager>().questFinish)
            {
                RenderSettings.fogColor = new Color(130 / 255.0f, 66 / 255.0f, 66 / 255.0f, 255 / 255.0f);
            }
            else {
                RenderSettings.fogColor = new Color(0 / 255.0f, 111 / 255.0f, 100 / 255.0f, 255 / 255.0f);
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (textFinished && !cancelTyping)
            {

                StartCoroutine(SetTextUI());

            }
            else if (textFinished == false && cancelTyping == false) {

                cancelTyping = true;

            }
        
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;

        textLabel.text = "";

        switch (textList[index])
        {
            case "A":
                faceImage.sprite = face01;
                index++;
                break;
            case "B":
                faceImage.sprite = face02;
                index++;
                break;
        }


        int letter = 0;

        while (cancelTyping == false && letter < textList[index].Length - 1)
        {

            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);

        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }

    void GetTextFromFile(TextAsset file) {

        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    
    }
}
