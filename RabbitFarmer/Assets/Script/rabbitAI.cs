using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rabbitAI : MonoBehaviour
{
    //private bool atNight;

    public LayerMask rabbitLayer, sexAbleLayer;
    public int oldCount = 10;
    private int birthCount = 0;

    public GameObject[] fragments;

    private Rigidbody2D body;
    private Animator anim;
    private CircleCollider2D bodyCol;
    private float speed = 0.3f, animspeed;
    private Vector2 speedVec, scaleVec, startScale = new Vector2(0.75f, 0.75f);
    private Transform imgTransform;

    private bool moving = false, isGrab = false, wakeup = false, isDeath = false;
    private float countMoveTime = 0f, startMoveTime = 0f;

    private SpriteRenderer rabbitImg;
    private Color rabbitColor;
    private float ranColorH;
    private bool grown = false;
    private float growSpeed = 1.0015f;
    private Vector2 grownSize = new Vector2(1f, 1f);

    private bool sexAble = false;
    private float sexRestTime = 2f, searchRadius;
    private Vector2 searchPt;
    [SerializeField]
    private Collider2D[] sexAbleTarget;
    private int targetCount;

    public GameObject birthTarget;

    // Use this for initialization
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCol = GetComponent<CircleCollider2D>();
        scaleVec = new Vector2(Mathf.Sign(Random.Range(-1f,1f)), 1f);
        transform.localScale = scaleVec;
        imgTransform = transform.Find("img");
        imgTransform.localScale = startScale;
        gameObject.layer = 9; //grabed layer for not grown rabbit
        searchRadius = bodyCol.radius * 1.2f;
        rabbitImg = transform.Find("img").GetComponent<SpriteRenderer>();
        ranColorH = Random.Range(0f, 1f);
        rabbitColor = Color.HSVToRGB(ranColorH, 15f / 255, 1f);
        rabbitImg.color = rabbitColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            //atNight = DayCtrl.atNight;
            //Debug.DrawRay(searchPt, Vector2.up, Color.green, searchRadius);
            if (!DayCtrl.atNight && oldCount >= birthCount)
            {
                if (!wakeup)
                {
                    Recover();
                    wakeup = true;
                    growSpeed = stageCtrl.GetGrowSpeed();
                    sexRestTime = stageCtrl.GetSexRestTime();
                }
                GrowUp();
                if (!isGrab)
                {
                    if (sexAble)
                    {
                        CheckAvailable();
                    }
                    RanMove();
                }
            }
            else
            {
                if (wakeup)
                {
                    Sleep(); //wakeup = false <inside
                }

                animspeed = 0;
            }

            anim.SetFloat("speed", animspeed);
        }
    }

    void RanMove()
    {
        if (Time.time > startMoveTime + countMoveTime)
        {
            startMoveTime = Time.time;
            countMoveTime = Random.Range(1f, 1.5f);
            moving = !moving;

            if (moving)
            {
                //filp
                scaleVec = transform.localScale;
                scaleVec.x = scaleVec.x * -1;
                transform.localScale = scaleVec;
            }
        }
        if (moving)
        {
            speedVec = body.velocity;
            speedVec.x = speed * scaleVec.x;
            body.velocity = speedVec;
            animspeed = speed;
        }
        else
        {
            animspeed = 0f;
        }
    }

    void GrowUp()
    {
        if (!grown)
        {
            if (imgTransform.localScale.x >= grownSize.x)
            {
                grown = true;
                Recover();
            }
            else
            {
                imgTransform.localScale *= growSpeed;
            }
        }
    }

    public void Sex()
    {
        gameObject.layer = 9;
        sexAble = false;
        //transform.tag = "RestingRabbit";
        StartCoroutine(SexRest(sexRestTime));
    }

    public void Birth(Vector3 mypos, Vector3 hispos)
    {
        //pos = (transform.position + pos) / 2;
        mypos.y += 0.05f;
        hispos.y += 0.05f;
        while (Physics2D.Linecast(mypos, hispos, rabbitLayer))
        {
            //pos.y = birthline.birthY + 0.0f;
            mypos.y += 0.04f;
            hispos.y += 0.04f;
        }
        mypos = (mypos + hispos) / 2;
        Instantiate(birthTarget, mypos, Quaternion.identity);
        birthCount++;
        rabbitColor = Color.HSVToRGB(ranColorH, 15f / 255, (140f + (115f*(((float)oldCount-birthCount)/oldCount))) /255 );
        rabbitImg.color = rabbitColor;
        
        if (birthCount >= oldCount)
        {
            Death();
        }
    }

    void Death()
    {
        anim.SetBool("death", true);
        isDeath = true;
        sexAble = false;
        gameObject.tag = "DeadRabbit";
    }

    public void Crush()
    {
        int fragmentCount = fragments.Length;
        Vector2 ranVec = Random.insideUnitCircle.normalized;
        Transform headFrag = Instantiate(fragments[0].transform, transform.position, Quaternion.identity);
        headFrag.GetComponent<Rigidbody2D>().velocity = ranVec * 0.1f;
        headFrag.GetComponentInChildren<SpriteRenderer>().color = rabbitColor;
        ranVec *= -1f;
        Transform bodyFrag = Instantiate(fragments[Random.Range(1,fragmentCount)].transform, transform.position, Quaternion.identity);
        bodyFrag.GetComponent<Rigidbody2D>().velocity = ranVec * 0.1f;
        bodyFrag.GetComponentInChildren<SpriteRenderer>().color = rabbitColor;
        Destroy(gameObject);
    }

    void Sleep()
    {
        sexAble = false;
        wakeup = false;
        //transform.tag = "RestingRabbit";
    }

    void Recover()
    {
        if (grown)
        {
            gameObject.layer = 8;
            sexAble = true;
            //transform.tag = "Rabbit";
        }
    }

    IEnumerator SexRest(float time)
    {
        yield return new WaitForSeconds(time);
        Recover();
    }

    void CheckAvailable()
    {
        searchPt = transform.position;
        searchPt.y -= 0.02f;
        sexAbleTarget = Physics2D.OverlapCircleAll(searchPt, searchRadius, sexAbleLayer);
        targetCount = sexAbleTarget.Length;

        int i = 0;
        while (targetCount > i)
        {
            if (sexAbleTarget[i].gameObject != gameObject)
            {
                Sex();
                Birth(transform.position ,sexAbleTarget[i].transform.position);
                sexAbleTarget[i].GetComponent<rabbitAI>().Sex();
                break;
            }
            i++;
        }
    }

    public void Grabed(bool isGrab)
    {
        this.isGrab = isGrab;
        bodyCol.enabled = !isGrab;
        body.isKinematic = isGrab;
        if (isGrab)
        {
            gameObject.layer = 9;
            body.velocity = Vector2.zero;
            animspeed = 0f;
        }
        else
        {
            StartCoroutine(GrapReset(0.8f));
        }
    }

    IEnumerator GrapReset(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.layer = 8;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
     /*   if (sexAble)
        {
            if (col.transform.CompareTag("Rabbit"))
            {
                Sex();
                Birth(col.transform.position);
                col.transform.GetComponent<rabbitAI>().Sex();
                //col.transform.GetComponent<movementctrl>().Sex();
            }
        }*/
    }
}
