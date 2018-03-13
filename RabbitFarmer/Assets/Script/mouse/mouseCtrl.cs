using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCtrl : MonoBehaviour {

    public LayerMask GrabableLayer;

    public Sprite normal, detect, grab;

    private bool grabing; // current state

    private PointEffector2D effector;

    private float mouseSpeed = 0.006f;

    private CircleCollider2D triggerCol;
    private Transform greenRange;
    private SpriteRenderer mouseImg;
    private float detectCircleRadius, closestTargetDis, disCheck;
    [SerializeField]
    private Collider2D[] detectTarget;
    private Transform closestTarget;
    private Transform[] grabedTarget;
    private Vector2 mouseCurPos, mouseLastPos, realCurPos, mouseMoveTLpt, mouseMoveDRpt; //TL = topleft, DR = downright
    private int targetCount, grabbingCount, closestTargetNum, curCursorState; //0 for normal, 1 for detect, 2 for grab

    private AudioSource grappp;
    // Use this for initialization
    void Awake()
    {
        //Debug.Log(Camera.main.pixelWidth + " " + Screen.height);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        triggerCol = GetComponent<CircleCollider2D>();
        detectCircleRadius = triggerCol.radius;
        mouseImg = transform.Find("mouseimg").GetComponent<SpriteRenderer>();
        realCurPos = transform.position;
        //mouseMoveTLpt = GameObject.Find("stagemanager/mousemovearea/topleftpt").transform.position;
        //mouseMoveDRpt = GameObject.Find("stagemanager/mousemovearea/downrightpt").transform.position;
        realCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //effector = transform.Find("effector").GetComponent<PointEffector2D>();
        greenRange = transform.Find("range");
        grappp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();

        MouseCheck();

        if (DayCtrl.atNight)
        {
            if (grabing)
            {
                DropTarget();
            }
        }

        if (!grabing)
        {
            DetectTarget();
        }

    }

    void MouseMove()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        mouseCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //if curpos is Vector3, z will be -10, cause it's now a Vector2, z will be always 0

        /*if (mouseCurPos != mouseLastPos) //Moved
        {
            realCurPos = realCurPos + ((mouseCurPos - mouseLastPos) * mouseSpeed);
        }*/

        /*realCurPos.x = Mathf.Clamp(realCurPos.x + Input.GetAxis("Mouse X") * mouseSpeed, mouseMoveTLpt.x, mouseMoveDRpt.x);
        realCurPos.y = Mathf.Clamp(realCurPos.y + Input.GetAxis("Mouse Y") * mouseSpeed, mouseMoveDRpt.y, mouseMoveTLpt.y);*/

        //realCurPos.x = Mathf.Clamp(mouseCurPos.x, mouseMoveTLpt.x, mouseMoveDRpt.x);
        //realCurPos.y = Mathf.Clamp(mouseCurPos.y, mouseMoveDRpt.y, mouseMoveTLpt.y);
        realCurPos = mouseCurPos;

        transform.position = realCurPos;
        mouseLastPos = mouseCurPos; //this may be better to run in the last of Update 
    }

    void MouseCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grabing = true;
            GrabTarget();
        }
        if (Input.GetMouseButtonUp(0))
        {
            grabing = false;
            DropTarget();
        }
    }

    void DetectTarget()
    {
        detectTarget = Physics2D.OverlapCircleAll(realCurPos, detectCircleRadius, GrabableLayer);
        targetCount = detectTarget.Length;
        if (targetCount == 0)
        {
            SetMouseNormal();
        }
        else
        {
            SetMouseDetect();
        }
    }

    void GrabTarget()
    {
        SetMouseGrab();
        grappp.pitch = 1 * Random.Range(0.8f, 1.2f);
        grappp.Play();

        /////***********************    Find Closest Target   ****************************/////
        if (targetCount > 0)
        {
            /*closestTargetNum = 0;
            closestTargetDis = Vector2.Distance(realCurPos, detectTarget[0].transform.position);

            for (int i = 1; i < targetCount; i++)
            {
                disCheck = Vector2.Distance(realCurPos, detectTarget[i].transform.position);
                if (closestTargetDis > disCheck)
                {
                    closestTargetDis = disCheck;
                    closestTargetNum = i;
                }
            }

            closestTarget = detectTarget[closestTargetNum].transform;*/
       /////****************************************************************************/////

            grabedTarget = new Transform[targetCount];
            for (int i =0; i< targetCount; i++)
            {
                grabedTarget[i] = detectTarget[i].transform;
                if (grabedTarget[i].tag != "Fragments")
                {
                    grabedTarget[i].GetComponent<rabbitAI>().Grabed(true);
                }
                else
                {
                    grabedTarget[i].GetComponent<Rigidbody2D>().isKinematic = true;
                    grabedTarget[i].GetComponent<Collider2D>().enabled = false;
                }
                grabedTarget[i].parent = transform;
            }
            grabbingCount = targetCount;
            //effector.enabled = true;
        }


    }

    void DropTarget()
    {
        /*if (grabbingCount > 0)
        {
            for (int i = 0; i < grabbingCount; i++)
            {
                if (grabedTarget[i].tag != "Fragments")
                {
                    grabedTarget[i].GetComponent<rabbitAI>().Grabed(false);
                }
                else
                {
                    grabedTarget[i].GetComponent<Rigidbody2D>().isKinematic = false;
                    grabedTarget[i].GetComponent<Collider2D>().enabled = true;
                }
                grabedTarget[i].parent = null;
            }

            grabedTarget = null;
            grabbingCount = 0;
            effector.enabled = false;
        }*/
        int grabbedCount = transform.childCount;
        Transform tempTarget;

        if (grabbedCount >3)
        {
            for (int i = 3; i < grabbedCount; i++)
            {
                tempTarget = transform.GetChild(3);
                if (tempTarget.tag != "Fragments")
                {
                    tempTarget.GetComponent<rabbitAI>().Grabed(false);
                }
                else
                {
                    tempTarget.GetComponent<Rigidbody2D>().isKinematic = false;
                    tempTarget.GetComponent<Collider2D>().enabled = true;
                }
                tempTarget.parent = null;
            }
        }
    }

    public void SetMouseNormal()
    {
        if (curCursorState != 0)
        {
            mouseImg.sprite = normal;
            curCursorState = 0;
        }
    }

    public void SetMouseDetect()
    {
        if (curCursorState != 1)
        {
            mouseImg.sprite = detect;
            curCursorState = 1;
        }
    }

    public void SetMouseGrab()
    {
        if (curCursorState != 2)
        {
            mouseImg.sprite = grab;
            curCursorState = 2;
        }
    }

    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
        {
            DropTarget();
        }
    }*/

    public void UpgradeGrabHand(float scaleValue)
    {
        greenRange.localScale *= scaleValue;
        triggerCol.radius *= scaleValue;
        detectCircleRadius = triggerCol.radius;
    }

}
