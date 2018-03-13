using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : MonoBehaviour {

    //private float mouseSpeed = 0.006f;
    public Transform[] rockets;
    private int curRockets = 0;

    private SpriteRenderer mouseImg;
    private float disCheck;
    private Vector2 mouseCurPos, mouseLastPos, realCurPos, mouseMoveTLpt, mouseMoveDRpt; //TL = topleft, DR = downright

    private Animator anim;

    private float fireRate = 2f, nextFireTime;
    private int bulletCost = 100;

    private AudioSource boom;

    // Use this for initialization
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        //mouseImg = transform.Find("mouseimg").GetComponent<SpriteRenderer>();
        realCurPos = transform.position;
        realCurPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        anim = GetComponent<Animator>();
        boom = GetComponent<AudioSource>();
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
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                anim.SetTrigger("Shoot");
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        SceenShake.ShakeScreen(0.02f);
        Instantiate(rockets[curRockets], transform.position, Quaternion.identity);
        BackGroundSetting.FundsComsumed(bulletCost);

        boom.pitch = 1 * Random.Range(0.8f, 1.2f);
        boom.Play();
    }

    public void UpdateRPGLevel(float fireRate, int level)
    {
        this.fireRate = fireRate;
        bulletCost = level*100;
        curRockets = level - 1;
    }
}
