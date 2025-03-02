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
    public GameObject komunikat3;
    public GameObject cam;
    public bool pokazLaptoka;
    public GameObject text;
    public TMP_InputField text1;
    public bool pokazLaptoka2; // po kliknięciu przycisku ma informować o kartce pod stołem
    public bool pokazLaptoka3; // ma informować o zniknięciu salcesona

    Vector3 odleglosc;
    bool rozwiazano;
    bool mouseOver;

    void Start()
    {
        Uj.SetActive(false);
        rozwiazano = false;
        pokazLaptoka = false;
        pokazLaptoka2 = false;
        pokazLaptoka3 = false;
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
            pokazLaptoka = false;
            pokazLaptoka2 = false;

        }
        if (text1.text == "1945" && Input.GetKeyDown(KeyCode.Return)) rozwiazano = true;
        //if (text1.text == "pup") rozwiaz = true;
        if (rozwiazano)
        {
            text.SetActive(false);
            if (pokazLaptoka) komunikat2.SetActive(true);
            else komunikat2.SetActive(false);
            pokazLaptoka3 = true;
            salceson.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (!pokazLaptoka) schowajFunkcja();
        if (pokazLaptoka) pokazFunkcja();
    }

    void pokazFunkcja()
    {
        Uj.SetActive(true);
        // text.SetActive(true);
        if(!pokazLaptoka3)
        {
            if(!pokazLaptoka2){
                komunikat1.SetActive(true);
                komunikat2.SetActive(false);
                komunikat3.SetActive(false);
            }
            if (pokazLaptoka2){
                komunikat1.SetActive(false);
                komunikat2.SetActive(true);
                komunikat3.SetActive(false);
            }
        }
        if(pokazLaptoka3)
        {
            komunikat1.SetActive(false);
            komunikat2.SetActive(false);
            komunikat3.SetActive(true);
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
