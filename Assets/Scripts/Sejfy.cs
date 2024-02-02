using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Sejfy : MonoBehaviour
{
    public Camera cam;
    public List<Sejf> sejfy = new List<Sejf>();

    private Player player_controller;
    private float max_odleglosc;

    void Start()
    {
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) && 
                hit.transform.tag == "Przycisk" && 
                hit.transform.parent.parent.parent)
            {   
                foreach(var sejf_klasa in sejfy)
                {   
                    if(!sejf_klasa.otwarty)
                    {
                        if(hit.transform.name == "Return")
                        {   
                            if(sejf_klasa.obecnyTekst == sejf_klasa.haslo)
                            {
                                sejf_klasa.otwarty = true;
                                DOTween.Init();
                                sejf_klasa.klapa.DOLocalRotate(sejf_klasa.rotacjaKlapy, sejf_klasa.czasOtwierania);
                            }
                        }
                        else if(hit.transform.name == "Clear")
                        {
                            sejf_klasa.obecnyTekst = "";
                            sejf_klasa.poleNaHaslo.text = sejf_klasa.obecnyTekst;
                        }
                        else
                        {
                            if(sejf_klasa.sejf == hit.transform.parent.parent.parent && sejf_klasa.obecnyTekst.Length < sejf_klasa.maxDlugoscHasla)
                            {
                                sejf_klasa.obecnyTekst += hit.transform.name;
                                sejf_klasa.poleNaHaslo.text = sejf_klasa.obecnyTekst;
                            }
                        }
                    }
                }
            }
        }
    }
}

[Serializable]
public class Sejf
{
    public Transform sejf;

    [Header("Dane")]
    public TMP_Text poleNaHaslo;
    public Transform klapa;
    public Vector3 rotacjaKlapy;
    public float czasOtwierania;

    [Header("Warunki")]
    public string haslo;
    public int maxDlugoscHasla;
    [HideInInspector] public string obecnyTekst = "";
    [HideInInspector] public bool otwarty = false;
}