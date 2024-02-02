using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Skrzynie : MonoBehaviour
{   
    public Camera cam;
    public List<Skrzynki> skrzynki = new List<Skrzynki>();

    private Player player_controller;
    private float max_odleglosc;

    void Start()
    {
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) && hit.transform.tag == "Skrzynia")
            {   
                foreach(var skrzynia_klasa in skrzynki)
                {
                    if(skrzynia_klasa.skrzynia == hit.transform)
                    {   
                        if(skrzynia_klasa.obecnaWartosc >= skrzynia_klasa.liczbaDoOtwarcia && !skrzynia_klasa.otwarta)
                        {   
                            skrzynia_klasa.otwarta = true;
                            hit.transform.GetComponent<BoxCollider>().enabled = false;

                            DOTween.Init();
                            skrzynia_klasa.klapa.DOLocalRotate(skrzynia_klasa.rotacjaKlapy, skrzynia_klasa.czasOtwierania);
                        }
                    }
                }
            }
        }
    }
}

[Serializable]
public class Skrzynki
{
    public Transform skrzynia;

    [Header("Dane")]
    public Transform klapa;
    public Vector3 rotacjaKlapy;
    public float czasOtwierania;

    [Header("Warunki")]
    public int liczbaDoOtwarcia;
    [HideInInspector] public int obecnaWartosc = 0;
    [HideInInspector] public bool otwarta = false;
}