using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LaptopikWos : MonoBehaviour
{
    public Transform player;
    public GameObject Uj;
    public float odlegloscPodnoszenia;
    public GameObject salceson;
    public GameObject komunikat1;
    public GameObject komunikat2;
    public GameObject cam;
    public bool pokazLaptoka;
    public GameObject text;
    public TMP_InputField text1;
    public bool pokazLaptoka2;

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


    private void OnMouseExit(){
        mouseOver = false;
    }

    private void OnMouseOver()
    {
        if(odleglosc.magnitude < odlegloscPodnoszenia){
            transform.gameObject.layer = LayerMask.NameToLayer("Outline");
        }
        mouseOver = true;

        if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !rozwiazano && PendriveDoLaptopikaWos.trzymany)
        {
            PendriveDoLaptopikaWos.trzymany2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !PendriveDoLaptopikaWos.trzymany && PendriveDoLaptopikaWos.udaloSie && !pokazLaptoka)
        {
            pokazLaptoka = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(odleglosc.magnitude > odlegloscPodnoszenia || !mouseOver){
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        odleglosc = player.position - transform.position;


        if (Input.GetKeyDown(KeyCode.Escape) && pokazLaptoka)
        {
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        if (text1.text == "1945" && Input.GetKeyDown(KeyCode.Return)) rozwiazano = true;
        //if (text1.text == "pup") rozwiaz = true;
        if (rozwiazano)
        {
            text.SetActive(false);
            if (pokazLaptoka) komunikat2.SetActive(true);
            else komunikat2.SetActive(false);
            pokazLaptoka2 = true;
            salceson.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (!pokazLaptoka) schowajFunkcja();
        else if (pokazLaptoka) pokazFunkcja();

        if (pokazLaptoka) player.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void pokazFunkcja()
    {
        Uj.SetActive(true);
        text.SetActive(true);
        if (pokazLaptoka2){
            komunikat1.SetActive(false);

        }
    }
    void schowajFunkcja()
    {
        Uj.SetActive(false);
    }













    // void Update()
    // {
    //     if(odleglosc.magnitude > odlegloscPodnoszenia || !mouseOver)
    //     {
    //         transform.gameObject.layer = LayerMask.NameToLayer("Default");
    //     }
        
    //     odleglosc = player.position - transform.position;

    //     if(rozwiazano)
    //     {
    //         salceson.SetActive(false);
    //     }
    // }

    // void OnMouseOver()
    // {
    //     if(odleglosc.magnitude < odlegloscPodnoszenia)
    //     {
    //         transform.gameObject.layer = LayerMask.NameToLayer("Outline");
    //     }

    //     mouseOver = true;

    //     if(odleglosc.magnitude <= odlegloscPodnoszenia && Input.GetKeyDown(KeyCode.E) && !rozwiazano)
    //     {
    //         Uj.SetActive(true);
    //         komunikat2.SetActive(false);
    //     }
    // }

    // void OnMouseExit()
    // {
    //     mouseOver = false;
    // }
}
