using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementctrl : MonoBehaviour {

    private Rigidbody2D body;
    private Animator anim;
    private float speed, speedcoe = 0.8f;
    private Vector2 speedVec, scaleVec;

    private bool sexAble = true;
    private float sexRestTime = 2f;

    public GameObject birthTarget;

    // Use this for initialization
    void Awake () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scaleVec.y = 1;
    }
	
	// Update is called once per frame
	void Update () {

        Move();

        anim.SetFloat("speed", Mathf.Abs(speed));
	}

    void Move()
    {
        speed = Input.GetAxis("Horizontal");
        speedVec.x = speed;
        if (speed != 0 && Mathf.Sign(transform.localScale.x) != Mathf.Sign(speed))
        {
            scaleVec.x = Mathf.Sign(speed);
            transform.localScale = scaleVec;
        }
        body.velocity = speedVec* speedcoe;
    }

    public void Sex()
    {
        sexAble = false;
        transform.tag = "RestingRabbit";
        StartCoroutine(SexRest(sexRestTime));
    }

    public void Birth(Vector3 pos)
    {
        Instantiate(birthTarget, (transform.position + pos) / 2, Quaternion.identity);
    }

    void Recover()
    {
        sexAble = true;
        transform.tag = "Rabbit";
    }

    IEnumerator SexRest(float time)
    {
        yield return new WaitForSeconds(time);
        Recover();
    }


}
