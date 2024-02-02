using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Klodka_Skrzynia1 : MonoBehaviour
{   
    public Itemy itemySystem;
    public Player playerController;
    public Skrzynie skrzynieSystem;

    [Space]
    public Camera cam;
    public Transform reka;
    public Transform skrzynia;
    public Transform klucz;

    private bool otworzonaKlodka = false;
    private float max_odleglosc;

    private void zwieksz_wartosc(Skrzynki skrzynka)
    {
        skrzynka.obecnaWartosc += 1;
    }

    void Start()
    {
        max_odleglosc = playerController.maxOdleglosc;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if(Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
                && !otworzonaKlodka
                && hit.transform == transform
                && reka.childCount > 0
                && klucz.IsChildOf(reka))
            {
                otworzonaKlodka = true;
                transform.GetComponent<Collider>().enabled = false;

                itemySystem.trzymany_przedmiot = null;
                itemySystem.rg_body = null;

                klucz.SetParent(null);
                klucz.position = new Vector3(0f, -100f, 0f);

                foreach(var skrzynia_klasa in skrzynieSystem.skrzynki)
                {
                    if(skrzynia_klasa.skrzynia == skrzynia)
                    {   
                        DOTween.Init();
                        transform.DOMoveY(-100f, 0.1f).OnComplete(() => zwieksz_wartosc(skrzynia_klasa));
                    }
                }
            }
        }
    }
}
