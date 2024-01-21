using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class timerUndButton : MonoBehaviour
{
    public GameObject textTiimera;
    public TextMesh textTimera;
    public bool wcisnieto;
    public bool wcisnieto2;
    public float prafdziwyCzas;
    public int pokazyfanyCzas;
    public bool licz;
    Vector3 pozycja = new Vector3(3.29500008f, -0.116f, 14.0900002f);

    // Vector3(3.29500008,-0.0820000023,14.0900002)
    // Vector3(3.29500008,-0.116999999,14.0900002)

    // Start is called before the first frame update
    void Start()
    {
        textTiimera.SetActive(false);
        licz = true;
        wcisnieto = false;
        wcisnieto2 = false;
        transform.localPosition = new Vector3(3.29500008f, -0.082f, 14.0900002f);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            textTiimera.SetActive(true);
            licz = false;
            wcisnieto = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (licz)
        {
            prafdziwyCzas = prafdziwyCzas + Time.deltaTime;
            pokazyfanyCzas = (int)prafdziwyCzas;
            textTimera.text = pokazyfanyCzas.ToString();
        }
        //if (wcisnieto && transform.position == pozycja)
        //{
        //    wcisnieto = false;
        //    wcisnieto2 = true;
        //}
        //else if (wcisnieto)
        //{
        //    transform.position = transform.position - new Vector3(0, 0.001f, 0);
        //}
        //else if (wcisnieto2)
        //{
        //    transform.position = transform.position + new Vector3(0, 0.001f, 0);
        //    if (transform.position != new Vector3(3.29500008f, -0.082f, 14.0900002f)) wcisnieto2 = false;
        //}
        if (wcisnieto)
        {
            
        }
    }
}
