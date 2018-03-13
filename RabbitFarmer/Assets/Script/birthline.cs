using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birthline : MonoBehaviour {

    public static float birthY;

    [SerializeField]
    private Transform lineLeft,lineRight;
    private float topY, bottomY;
    private Vector2 scanLeft, scanRight;

    public LayerMask scanLayer;

	// Use this for initialization
	void Awake () {
        lineLeft = transform.Find("leftpt");
        lineRight = transform.Find("rightpt");
        scanLeft.x = lineLeft.position.x;
        scanRight.x = lineRight.position.x;
        topY = Camera.main.orthographicSize;
        bottomY = topY * -1;
        transform.position = new Vector2(0f, 0f+topY/2);
	}
	
	// Update is called once per frame
	void Update () {
        CheckingLine();
        Debug.DrawLine(new Vector2(scanLeft.x,birthY),new Vector2(scanRight.x, birthY));
	}

    void CheckingLine()
    {
        float checkY = topY;
        while (checkY > bottomY)
        {
            scanLeft.y = checkY;
            scanRight.y = checkY;
            if (Physics2D.Linecast(scanLeft, scanRight, scanLayer))
            {
                birthY = scanLeft.y;
                break;
            }
            else
            {
                checkY -= topY / 20;
            }
        }
        //birthY = bottomY * 3 / 5;
    }
}
