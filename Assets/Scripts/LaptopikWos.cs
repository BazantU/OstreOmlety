using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopikWos : MonoBehaviour
{
    public Transform player;
    public GameObject Uj;
    public float odlegloscPodnoszenia;
    public GameObject salceson;
    public GameObject komunikat1;
    public GameObject komunikat2;

    Vector3 odleglosc;
    bool rozwiazano;
    bool mouseOver;
    bool pendriveDoLaptopika;

    void Start()
    {
        Uj.SetActive(false);
        pendriveDoLaptopika = false;
        rozwiazano = false;
    }

    void Update()
    {
        if(odleglosc.magnitude > odlegloscPodnoszenia || !mouseOver)
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        
        odleglosc = player.position - transform.position;

        if(rozwiazano)
        {
            salceson.SetActive(false);
        }
    }

    void OnMouseOver()
    {
        if(odleglosc.magnitude < odlegloscPodnoszenia)
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Outline");
        }

        mouseOver = true;

        if(odleglosc.magnitude < odlegloscPodnoszenia && Input.GetKeyDown(KeyCode.E) && !rozwiazano)
        {
            Uj.SetActive(true);
            komunikat2.SetActive(false);
        }
    }

    void OnMouseExit()
    {
        mouseOver = false;
    }
}
