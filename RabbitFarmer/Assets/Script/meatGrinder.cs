using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatGrinder : MonoBehaviour {

    //public Transform[] funnels;
    //private int curFunnel = 0;

    public int maxMeat;
    public float workTime;
    private int startMaxMeat;
    private float startWorkTime;

    public bool turboOn = false;
    private float endTurboTime;

    private Animator anim;
    private bool working = false;

    //private SpriteRenderer funnelImg;
    //private Color funnelColor;

    [SerializeField]
    private int meatCount = 0, workingMeat = 0;
    [SerializeField]
    private List<Transform> meatEnter = new List<Transform>();
    private Transform meatGot;
    private List<string> rabbitTags = new List<string>{ "Rabbit", "RestingRabbit", "DeadRabbit", "SickRabbit", "Fragments" };

    private AudioSource wwwww;
    private float lastPlayTime;

	// Use this for initialization
	void Awake () {
        //funnelImg = transform.Find("funnel/img").GetComponent<SpriteRenderer>();
        //funnelColor = funnelImg.color;
        anim = GetComponent<Animator>();
        startMaxMeat = maxMeat;
        startWorkTime = workTime;
        wwwww = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!DayCtrl.atNight)
        {
            if (turboOn)
            {
                if (Time.time >= endTurboTime)
                {
                    TurboOn(false);
                }
            }
            Work();
        }
        else
        {
            anim.SetBool("working", false);
            wwwww.Stop();
        }
	}

    void Work()
    {
        if (meatCount > 0)
        {
            //funnelColor.a = (float)110 / 255;
            if (maxMeat > workingMeat)
            {
                Smash(meatEnter[0]);
            }
        }
        else
        {
            //funnelColor.a = 1f;
        }

        //funnelImg.color = funnelColor;

        anim.SetBool("working", (workingMeat > 0));
        if (workingMeat > 0)
        {
            //wwwww.pitch = 1 * Random.Range(0.8f, 1.2f);
            if (!wwwww.isPlaying)
            {
                wwwww.Play();
            }
            lastPlayTime = Time.time;
        }
        else
        {
            if (Time.time >= lastPlayTime + 0.5f)
            {
                wwwww.Stop();
            }
        }
    }

    void Smash(Transform meat)
    {
        meatEnter.Remove(meat);
        if (meat.CompareTag("Fragments"))
        {
            BackGroundSetting.FundsEarned(6);
        }
        else
        {
            BackGroundSetting.FundsEarned(10);
        }
        meatCount--;
        Destroy(meat.gameObject);

        workingMeat++;
        StartCoroutine("Recover");
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(workTime);
        workingMeat = Mathf.Max(0, workingMeat-1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (rabbitTags.Contains(col.tag))
        {
            meatEnter.Add(col.transform);
            meatCount++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (rabbitTags.Contains(col.tag))
        {
            meatGot = col.transform;
            if (meatEnter.Contains(meatGot))
            {
                meatEnter.Remove(meatGot);
                meatCount--;
            }
        }
    }

    public void TurboOn(bool onoff)
    {
        turboOn = onoff;
        if (turboOn)
        {
            
            endTurboTime = Time.time + 0.5f;
            maxMeat = startMaxMeat * 2;
            workTime = startWorkTime / 2;
            workingMeat = Mathf.Max(0, workingMeat - 1);
        }
        else
        {
            maxMeat = startMaxMeat;
            workTime = startWorkTime;
        }
    }

    public void UpgradeGrinder(float scaleValue, float scaleSpeed)
    {
        Vector2 tempSca = transform.localScale;
        tempSca *= scaleValue;
        transform.localScale = tempSca;
        maxMeat *= 3;
        startMaxMeat = maxMeat;
        workTime /= scaleSpeed;
        startWorkTime = workTime;
    }
}
