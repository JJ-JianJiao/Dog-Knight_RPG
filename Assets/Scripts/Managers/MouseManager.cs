using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{

    public Texture2D point, doorway, attack, target, arrow;
    RaycastHit hitInfo;

    public Camera mainCam;
    public Camera fvCam;
    //public Camera fvCam;

    public GameObject player;

    Ray ray;

    public GameObject dialogPanel;
    public Camera dialogCamera;

    public GameObject manuPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCursorTexture();
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            //Application.Quit();
            if (!manuPanel.activeSelf)
                manuPanel.SetActive(true);
            else
                manuPanel.SetActive(false);
        }
    }
    void SetCursorTexture() {
        if (mainCam.gameObject.activeSelf)
        {
            ray = mainCam.ScreenPointToRay(Input.mousePosition);
        } 
        else if (fvCam.gameObject.activeSelf) {
            ray = mainCam.ScreenPointToRay(Input.mousePosition);
        }

        if (Physics.Raycast(ray, out hitInfo))
        {
            //切换鼠标贴图
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    //Debug.Log("Hit Ground");
                    Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    //Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "NPC":
                    //Debug.Log("Hit NPC");
                    //Cursor.SetCursor(point, new Vector2(16, 16), CursorMode.Auto);
                    //Debug.Log(Vector3.Distance(hitInfo.transform.position, player.transform.position));
                    //if (Input.GetMouseButtonUp(0) && Vector3.Distance(hitInfo.transform.position, player.transform.position) < 2)
                    if (Vector3.Distance(hitInfo.transform.position, player.transform.position) < 2.5)
                    {
                        Cursor.SetCursor(point, new Vector2(16, 16), CursorMode.Auto);
                        if (Input.GetMouseButtonUp(0))
                        {
                            //Debug.Log("Give you the quest");
                            dialogPanel.gameObject.SetActive(true);
                            dialogCamera.gameObject.SetActive(true);
                            player.GetComponent<PlayerController>().isDialog = true;
                        }

                    }
                    else {
                        Cursor.SetCursor(arrow, new Vector2(16, 16), CursorMode.Auto);
                    }

                    break;
            }
        }
    }
}
