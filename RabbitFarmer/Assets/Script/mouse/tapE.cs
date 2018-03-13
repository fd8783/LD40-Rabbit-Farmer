using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapE : MonoBehaviour {
    
    //private float mouseSpeed = 0.006f;

    private SpriteRenderer mouseImg;
    private float disCheck;
    private Vector2 mouseCurPos, mouseLastPos, realCurPos, mouseMoveTLpt, mouseMoveDRpt; //TL = topleft, DR = downright

    private Transform MeatGrinder;
    private meatGrinder grinderCtrl;
    private Rigidbody2D wheel;
    private Animator anim;

    // Use this for initialization
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        //mouseImg = transform.Find("mouseimg").GetComponent<SpriteRenderer>();
        realCurPos = transform.position;
        realCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        MeatGrinder = GameObject.Find("meatGrinder").transform;
        wheel = MeatGrinder.Find("wheel").GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grinderCtrl = MeatGrinder.GetComponent<meatGrinder>();

    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();

        if (!DayCtrl.atNight)
        {
            MouseClick();
        }
        
    }

    void MouseMove()
    {

        mouseCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //if curpos is Vector3, z will be -10, cause it's now a Vector2, z will be always 0

        realCurPos = mouseCurPos;
        
        transform.position = realCurPos;
        mouseLastPos = mouseCurPos; //this may be better to run in the last of Update 
    }

    void MouseClick()
    {
        if (Input.GetButtonDown("E") || Input.GetMouseButtonDown(0))
        {
            wheel.AddTorque(150f);
            grinderCtrl.TurboOn(true);
            anim.SetBool("clicking", true);
        }
        else
        {
            anim.SetBool("clicking", false);
        }
    }
}
