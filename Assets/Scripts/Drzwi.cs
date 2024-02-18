using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Linq;
using System;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.UI;

public class Drzwi : MonoBehaviour
{   
    public Camera cam;
    public List<DrzwiKlasa> drzwi = new List<DrzwiKlasa>();

    private Player player_controller;
    private float max_odleglosc;

    private void otworz_drzwi(DrzwiKlasa drzwi_klasa)
    {
        drzwi_klasa.otwarte = true;
        DOTween.Init();
        drzwi_klasa.drzwi.DOLocalRotate(drzwi_klasa.rotacjaKoncowa, drzwi_klasa.czasOtwierania);
    }

    private void zamknij_drzwi(DrzwiKlasa drzwi_klasa)
    {
        drzwi_klasa.otwarte = false;
        DOTween.Init();
        drzwi_klasa.drzwi.DOLocalRotate(drzwi_klasa.poczatkowaRotacja, drzwi_klasa.czasOtwierania);
    }

    public DrzwiKlasa get_odpowiednie_drzwi(Transform drzwi_obiekt)
    {   
        foreach(var drzwi_klasa in drzwi)
        {
            if(drzwi_klasa.drzwi == drzwi_obiekt)
            {
                return drzwi_klasa;
            }
        }
        return null;
    }

    void Start()
    {
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;

        foreach(var drzwi_klasa in drzwi)
        {
            drzwi_klasa.poczatkowaRotacja = drzwi_klasa.drzwi.rotation.eulerAngles;
        }
    }

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) && hit.transform.tag == "Drzwi")
            {   
                DrzwiKlasa drzwi = get_odpowiednie_drzwi(hit.transform);
                if(drzwi != null)
                {
                    if(!drzwi.otwarte && drzwi.obecna_wartosc >= drzwi.ileDoOtwarcia)
                    {
                        otworz_drzwi(drzwi);
                    }
                    else if(drzwi.otwarte)
                    {
                        zamknij_drzwi(drzwi);
                    }
                }
            }
        }
    }
}

[Serializable]
public class DrzwiKlasa
{   
    public Transform drzwi;
    public Vector3 rotacjaKoncowa;
    public float czasOtwierania;

    [Header("Wymagania")]
    public int ileDoOtwarcia;

    [HideInInspector] public bool otwarte = false;
    [HideInInspector] public int obecna_wartosc = 0;
    [HideInInspector] public Vector3 poczatkowaRotacja;
}
