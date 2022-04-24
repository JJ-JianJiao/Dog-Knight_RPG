using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileMode {IceBall, SolidBall};
public class PlayerController : MonoBehaviour
{
    ProjectileMode projectileType;

    [Header("Movement Variable")]
    //basic movement input variable
    public float vInput;
    public float hInput;
    public float movementSpeed;
    public float rotateSpeed;
    public float jumperPower;

    //check the player is ground or not
    private bool isGround;

    private Rigidbody rig;
    private Animator anim;

    //player camera control
    public Camera cam;
    public float mouseScrollInput;
    public float camMoveSpeed;
    private float camMinDistance;
    private float camMaxDistance;
    private float distanceCam;
    public GameObject mainCanOriginalPositon;

    //player firstView Camera control
    public Camera firstViewCam;
    public float mouseX;
    public float mouseY;
    public float fvCamRotateSpeed;
    Vector3 fvCamAngles;


    //上下最大视角（Y视角）(Second Way)
    public float fvCamMinmumY;
    public float fvCamMaxmunY;
    public float fvCamMaxmunX;
    public float fvCamMinmumX;
    float fvCamRotationY;
    float fvCamRotationX;
    public float sensitivityX;
    public float sensitivityY;

    //control switch cam
    bool camMainActive;

    //player attack
    float timeStamp;
    float attackRate;

    //player attack magic ball
    public GameObject iceBallPosition;
    //ice magic ball
    public GameObject iceBall;
    private float iceBallSpeed;
    //solid ball
    public GameObject solidBall;
    private float solidBallSpeed;

    private bool attacking;


    public bool isDialog;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        //basic movement
        movementSpeed = 2.0f;
        rotateSpeed = 100.0f;
        jumperPower = 4.0f;
        isGround = true;

        //Main Cam 
        camMoveSpeed = 80.0f;
        camMinDistance = 1.7f;
        camMaxDistance = 5f;

        //first view method One 
        fvCamRotateSpeed = 300.0f;

        //first view method Two
        sensitivityX = 200.0f;
        sensitivityY = 200.0f;
        fvCamMinmumY = -30f;
        fvCamMaxmunY = 30f;
        fvCamMinmumX = -45f;
        fvCamMaxmunX = 45f;
        fvCamRotationX = 0.0f;
        fvCamRotationY = 0.0f;

        camMainActive = true;

        //player attack
        timeStamp = -1;
        attackRate = 1f;

        iceBallSpeed = 10.0f;
        solidBallSpeed = 20.0f;

        projectileType = ProjectileMode.IceBall;
        attacking = false;

        isDialog = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDialog)
        {
            PlayerMovement();
            SwitchAnimation();
            SwitchBulletMode();
            PlayerAttack();
            SwitchCam();
        }

    }


    void SwitchCam() {

        if (Input.GetKeyDown(KeyCode.F1)) {
            camMainActive = !camMainActive;
            if (camMainActive)
            {
                StartCoroutine(firstViewCamMoveToMainCam());
            }
            else
            {
                StartCoroutine(MainCamMoveToHead());
            }
        }
        if (camMainActive)
        {
            PlayerCameraController();
        }
        else {
            FVCameraController();
        }    
    }

    IEnumerator MainCamMoveToHead() {

        while (Vector3.Distance(transform.position, cam.transform.position) > camMinDistance) {
            //TODO: retest the cam move speed
            cam.transform.Translate(Vector3.forward * Time.deltaTime * 10);
            yield return null;
        }
        cam.gameObject.SetActive(false);
        firstViewCam.gameObject.SetActive(true);
        firstViewCam.transform.localEulerAngles = Vector3.zero;
    }

    IEnumerator firstViewCamMoveToMainCam() {
        firstViewCam.transform.localEulerAngles = Vector3.zero;
        firstViewCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
        while (Vector3.Distance(transform.position, cam.transform.position) < camMaxDistance)
        {
            //TODO: retest the cam move speed
            cam.transform.Translate(-Vector3.forward * Time.deltaTime * 10);
            yield return null;
        }

    }

    void PlayerCameraController() {
        distanceCam = Vector3.Distance(transform.position, cam.transform.position);
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (distanceCam > camMaxDistance && mouseScrollInput < 0)
        {
            camMoveSpeed = 0;
        }
        else if (distanceCam < camMinDistance && mouseScrollInput > 0)
        {
            camMoveSpeed = 0;
        }
        else {
            camMoveSpeed = 80.0f;
        }
        cam.transform.Translate(Vector3.forward * camMoveSpeed * Time.deltaTime * mouseScrollInput);
    }

    void FirstViewCameraController() {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        firstViewCam.transform.Rotate(-mouseY * fvCamRotateSpeed * Time.deltaTime, mouseX * fvCamRotateSpeed * Time.deltaTime, 0);
        //firstViewCam.transform.Rotate(-mouseY * fvCamRotateSpeed * Time.deltaTime , 0 , 0);
        fvCamAngles = firstViewCam.transform.localEulerAngles;
        if (fvCamAngles.z != 0) {
            firstViewCam.transform.localEulerAngles = new Vector3(fvCamAngles.x, fvCamAngles.y, 0);        
        }
        if (fvCamAngles.x > 30 && fvCamAngles.x < 90)
        {
            //firstViewCam.transform.localRotation = Quaternion.Euler(30.0f, 0, 0);
            firstViewCam.transform.localEulerAngles = new Vector3(30.0f, fvCamAngles.y, 0);
        }
        else if (fvCamAngles.x < 330 && fvCamAngles.x > 270) {
            //firstViewCam.transform.localRotation = Quaternion.Euler(330.0f, 0, 0);
            firstViewCam.transform.localEulerAngles = new Vector3(330.0f, fvCamAngles.y, 0);
        }

        if (fvCamAngles.y > 30 && fvCamAngles.y < 90) {
            firstViewCam.transform.localEulerAngles = new Vector3(fvCamAngles.x, 30.0f, 0);
        }
        else if (fvCamAngles.y < 330 && fvCamAngles.y > 270)
        {
            //firstViewCam.transform.localRotation = Quaternion.Euler(330.0f, 0, 0);
            firstViewCam.transform.localEulerAngles = new Vector3(fvCamAngles.x, 330.0f, 0);
        }

    }


    void FVCameraController() {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        //rotationX = firstViewCam.transform.localEulerAngles.y + mouseX * sensitivityX;
        fvCamRotationY += mouseY * sensitivityY * Time.deltaTime;
        fvCamRotationX += mouseX * sensitivityX * Time.deltaTime;
        //角度限制，rotationY小于min返回min  大于max 返回max  不然返回value
        fvCamRotationY = Clamp(fvCamRotationY, fvCamMaxmunY, fvCamMinmumY);
        fvCamRotationX = Clamp(fvCamRotationX, fvCamMaxmunX, fvCamMinmumX);
        firstViewCam.transform.localEulerAngles = new Vector3(-fvCamRotationY, fvCamRotationX, 0);
    }

    public float Clamp(float value, float max, float min)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    //Player basic movemnt function ( forward, back, rotation and Jump)
    void PlayerMovement() {
        vInput = Input.GetAxis("Vertical");
        hInput = Input.GetAxis("Horizontal");
        if (vInput > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * vInput * movementSpeed);
        }
        else {
            transform.Translate(Vector3.forward * Time.deltaTime * vInput * movementSpeed/2);
        }
        transform.Rotate(Vector3.up * Time.deltaTime * hInput * rotateSpeed);
        if (Input.GetKeyDown(KeyCode.Space) && isGround) {
            isGround = false;
            rig.velocity = new Vector3(rig.velocity.x, jumperPower, rig.velocity.z);
        }
    }

    void PlayerAttack() {

        //if (Input.GetMouseButtonDown(0) && Time.time > timeStamp + attackRate)
        //if (Input.GetMouseButton(0) && Time.time > timeStamp + attackRate)
        if (Input.GetKey(KeyCode.Alpha1) && Time.time > timeStamp + attackRate)
        {
            attacking = true;
            //StartCoroutine(PlayerAttackAction());
            switch (projectileType)
            {
                case ProjectileMode.IceBall:
                    var projectileIceBall = Instantiate(iceBall, iceBallPosition.transform.position, iceBallPosition.transform.rotation);
                    projectileIceBall.GetComponent<Rigidbody>().velocity = projectileIceBall.transform.forward * iceBallSpeed;
                    Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), projectileIceBall.gameObject.GetComponent<Collider>(), true);
                    break;
                case ProjectileMode.SolidBall:
                    var projectileSolidBall = Instantiate(solidBall, iceBallPosition.transform.position, iceBallPosition.transform.rotation);
                    projectileSolidBall.GetComponent<Rigidbody>().velocity = projectileSolidBall.transform.forward * solidBallSpeed;
                    Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), projectileSolidBall.gameObject.GetComponent<Collider>(), true);
                    break;
                default:
                    break;
            }

            //Debug.Log("Click One time");
            timeStamp = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {

            attacking = false;

        }

    }

    void SwitchBulletMode() {

        if (Input.GetKeyDown(KeyCode.Tab)){
            switch (projectileType)
            {
                case ProjectileMode.IceBall:
                    projectileType = ProjectileMode.SolidBall;
                    break;
                case ProjectileMode.SolidBall:
                    projectileType = ProjectileMode.IceBall;
                    break;
            }
        }
    }

    //IEnumerator  PlayerAttackAction()
    //{
    //    anim.SetBool("Attack", true);
    //    yield return null;
    //    anim.SetBool("Attack", false);
    //}

    //Display the player animations
    void SwitchAnimation() {
        anim.SetFloat("Speed", vInput);
        if (vInput < 0)
        {
            anim.SetBool("MoveBack", true);
        }
        else {
            anim.SetBool("MoveBack", false);
        }

        if (attacking)
        {
            anim.SetBool("Attack", true);
        }
        else {
            anim.SetBool("Attack", false);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "Ground") {
            isGround = true;        
        }
    }
}
