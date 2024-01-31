using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;

public class Komputery : MonoBehaviour
{   
    public Camera cam;
    public GameObject kursorObiekt;

    [Space]
    public List<Urzadzenia> Urzadzenia = new List<Urzadzenia>();

    private Player player_controller;
    private float max_odleglosc;
    private float poczekaj_counter = 0f;

    private bool obsluguje_komputer = false;
    private Urzadzenia komputer_klasa = null;

    private void powieksz_na_komputer(Transform komputer)
    {
        foreach(var urzadzenie in Urzadzenia)
        {
            if(urzadzenie.komputer == komputer)
            {   
                player_controller.canMove = false;
                kursorObiekt.SetActive(false);

                obsluguje_komputer = true;
                komputer_klasa = urzadzenie;

                cam.enabled = false;
                urzadzenie.kamera.enabled = true;
            }
        }
    }

    private void opusc_komputer(Camera stara_kamera)
    {   
        player_controller.canMove = true;
        kursorObiekt.SetActive(true);

        cam.enabled = true;
        stara_kamera.enabled = false;

        obsluguje_komputer = false;
        komputer_klasa = null;

        poczekaj_counter = 0f;
    }

    void Start()
    {   
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;

        foreach(var urzadzenie in Urzadzenia)
        {
            urzadzenie.poleTekstowe.text = urzadzenie.wpisane_haslo;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !obsluguje_komputer)
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) && hit.transform.tag == "Komputer")
            {   
                powieksz_na_komputer(hit.transform);
            }
        }
        if(Input.GetKeyDown(KeyCode.Q) && obsluguje_komputer && komputer_klasa != null)
        {
            opusc_komputer(komputer_klasa.kamera);
        }
        if(obsluguje_komputer && komputer_klasa != null && komputer_klasa.mozna_wpisac)
        {   
            if(poczekaj_counter < 0.1f)
            {
                poczekaj_counter += Time.deltaTime;
            }
            foreach (char c in Input.inputString)
            {
                if (c == '\b') //usuwanie
                {
                    if (komputer_klasa.wpisane_haslo.Length != 0)
                    {
                        komputer_klasa.wpisane_haslo = komputer_klasa.wpisane_haslo.Substring(0, komputer_klasa.wpisane_haslo.Length - 1);
                        komputer_klasa.poleTekstowe.text = komputer_klasa.wpisane_haslo;
                    }
                }
                else if ((c == '\n') || (c == '\r')) //podsumowanie
                {   
                    if(komputer_klasa.wpisane_haslo == komputer_klasa.haslo)
                    {
                        komputer_klasa.mozna_wpisac = false;
                        komputer_klasa.wyswietlacz.GetComponent<RawImage>().texture = komputer_klasa.kocnowaGrafika;
                    }
                }
                else //dalsze pisanie
                {
                    if(komputer_klasa.wpisane_haslo.Length < komputer_klasa.maxDlugoscKodu && poczekaj_counter >= 0.1f)
                    {
                        komputer_klasa.wpisane_haslo += c;
                        komputer_klasa.poleTekstowe.text = komputer_klasa.wpisane_haslo;
                    }
                }
            }
        }
    }
}

[Serializable]
public class Urzadzenia
{
    public Transform komputer;

    [Header("Input")]
    public string haslo;
    public int maxDlugoscKodu;
    public TMP_Text poleTekstowe;

    [Header("Inne")]
    public Transform wyswietlacz;
    public Texture kocnowaGrafika;
    public Camera kamera;

    [HideInInspector] public bool mozna_wpisac = true;
    [HideInInspector] public string wpisane_haslo = "";
}
