using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

    private TextMeshProUGUI FPSvalue;

    // Use this for initialization
    void Awake()
    {
        FPSvalue = transform.Find("FPS").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        FPSvalue.text = "FPS: "+ (1 / Time.deltaTime).ToString("n2");
    }
}
