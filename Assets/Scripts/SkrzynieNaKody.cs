using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;

public class SkrzynieNaKody : MonoBehaviour
{
    public Camera cam;
    public GameObject kursorObiekt;

    [Space]
    public List<Skrzyneczki> Skrzynie = new List<Skrzyneczki>();

    private Player player_controller;
    private float max_odleglosc;
    private float poczekaj_counter = 0f;

    private bool zajety_skrzynia = false;
    private Skrzyneczki skrzyneczka_klasa = null;

    private void powieksz_na_skrzynie(Transform skrzynia_)
    {
        foreach(var skrzynia_klasa in Skrzynie)
        {
            if(skrzynia_klasa.skrzynia == skrzynia_)
            {   
                player_controller.canMove = false;
                kursorObiekt.SetActive(false);

                zajety_skrzynia = true;
                skrzyneczka_klasa = skrzynia_klasa;

                cam.enabled = false;
                skrzynia_klasa.kamera.enabled = true;
            }
        }
    }

    private void opusc_skrzynie(Camera stara_kamera)
    {   
        player_controller.canMove = true;
        kursorObiekt.SetActive(true);

        cam.enabled = true;
        stara_kamera.enabled = false;

        zajety_skrzynia = false;
        skrzyneczka_klasa = null;

        poczekaj_counter = 0f;
    }

    private void otworz_klape(Transform klapa)
    {
        DOTween.Init();
        skrzyneczka_klasa.klapa.DOLocalRotate(skrzyneczka_klasa.rotacjaKlapy, skrzyneczka_klasa.czasOtwierania).OnComplete(() => opusc_skrzynie(skrzyneczka_klasa.kamera));
    }

    void Start()
    {   
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;

        foreach(var skrzynia in Skrzynie)
        {
            skrzynia.poleTekstowe.text = skrzynia.wpisane_haslo;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !zajety_skrzynia)
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) && hit.transform.tag == "SkrzyniaNaKod")
            {   
                powieksz_na_skrzynie(hit.transform);
            }
        }
        if(Input.GetKeyDown(KeyCode.Q) && zajety_skrzynia && skrzyneczka_klasa != null)
        {
            opusc_skrzynie(skrzyneczka_klasa.kamera);
        }
        if(zajety_skrzynia && skrzyneczka_klasa != null && skrzyneczka_klasa.mozna_wpisac)
        {   
            if(poczekaj_counter < 0.1f)
            {
                poczekaj_counter += Time.deltaTime;
            }
            foreach (char c in Input.inputString)
            {
                if (c == '\b') //usuwanie
                {
                    if (skrzyneczka_klasa.wpisane_haslo.Length != 0)
                    {
                        skrzyneczka_klasa.wpisane_haslo = skrzyneczka_klasa.wpisane_haslo.Substring(0, skrzyneczka_klasa.wpisane_haslo.Length - 1);
                        skrzyneczka_klasa.poleTekstowe.text = skrzyneczka_klasa.wpisane_haslo;
                    }
                }
                else if ((c == '\n') || (c == '\r')) //podsumowanie
                {   
                    if(skrzyneczka_klasa.wpisane_haslo == skrzyneczka_klasa.haslo)
                    {
                        skrzyneczka_klasa.mozna_wpisac = false;
                        skrzyneczka_klasa.skrzynia.GetComponent<BoxCollider>().enabled = false;
                        otworz_klape(skrzyneczka_klasa.klapa);
                    }
                }
                else //dalsze pisanie
                {
                    if(skrzyneczka_klasa.wpisane_haslo.Length < skrzyneczka_klasa.maxDlugoscKodu && poczekaj_counter >= 0.1f)
                    {
                        skrzyneczka_klasa.wpisane_haslo += c;
                        skrzyneczka_klasa.poleTekstowe.text = skrzyneczka_klasa.wpisane_haslo;
                    }
                }
            }
        }
    }
}

[Serializable]
public class Skrzyneczki
{
    public Transform skrzynia;

    [Header("Input")]
    public string haslo;
    public int maxDlugoscKodu;
    public TMP_Text poleTekstowe;

    [Header("Klapa")]
    public Transform klapa;
    public Vector3 rotacjaKlapy;
    public float czasOtwierania;

    [Header("Inne")]
    public Camera kamera;

    [HideInInspector] public bool mozna_wpisac = true;
    [HideInInspector] public string wpisane_haslo = "";
}
