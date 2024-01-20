using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Linq;
using System;
using JetBrains.Annotations;
using System.Diagnostics.CodeAnalysis;

public class Drzwi : MonoBehaviour
{   
    public Transform gracz;
    public Camera cam;

    public Vector3 podstawowy_obrot_o;
    private List<Transform> otwarte_drzwi = new List<Transform>();

    public List<DrzwiKlasa> drzwi_specjalne = new List<DrzwiKlasa>();

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 1f) && hit.transform.tag == "Drzwi" && !otwarte_drzwi.Contains(hit.transform))
            {   
                Transform drzwi = hit.transform;
                foreach(var klasa in drzwi_specjalne)
                {
                    if(klasa.obiekt == drzwi)
                    {   
                        if(klasa.obecna_wartosc >= klasa.ile_do_otwarcia)
                        {   
                            otwarte_drzwi.Add(drzwi);
                            drzwi.tag = "Untagged";
                            DOTween.Init();
                            drzwi.DOLocalRotate(klasa.rotacja, klasa.czas_otwierania);
                        }
                        else
                        {
                            print("Czegos tu brakuje...");
                        }
                    }
                    else
                    {   
                        otwarte_drzwi.Add(drzwi);
                        drzwi.tag = "Untagged";
                        DOTween.Init();
                        drzwi.DOLocalRotate(drzwi.localRotation.eulerAngles + podstawowy_obrot_o, 1.5f);
                    }
                }
            }
        }
    }
}

[Serializable]
public class DrzwiKlasa
{   
    public Transform obiekt;
    public Vector3 rotacja;
    public float czas_otwierania;

    [Space]
    public int ile_do_otwarcia;
    [HideInInspector] public int obecna_wartosc = 0;
}