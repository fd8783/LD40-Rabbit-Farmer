using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class onebyone : MonoBehaviour {

    public float speed = 0.05f;

    private string talktext;

    private TextMeshProUGUI textMesh;

	// Use this for initialization
	void Awake () {
        textMesh = GetComponent<TextMeshProUGUI>();
        talktext = textMesh.text;
        textMesh.text = "";
	}

    void Start()
    {

        StartCoroutine(talking(talktext));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    IEnumerator talking(string text)
    {
        int c = text.Length;
        for (int i = 0; i < c;i++)
        {
            yield return new WaitForSeconds(speed);
            textMesh.text += text[i];
        }
    }
    
}
