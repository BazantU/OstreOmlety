using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class Testowy_sejf : MonoBehaviour
{   
    public Transform drzwi;
    public GameObject sejf;
    public Vector3 koncowa_rotacja;

    public Global_interakcja_zasieg global;
    public Transform gracz;
    public Camera cam;

    public TMP_Text pole_tekstowe;
    public string haslo;

    private float odleglosc_max;
    private bool otwarty = false;
    private string tekst = "";
    
    void Start()
    {
        odleglosc_max = global.odleglosc;
        pole_tekstowe.text = tekst;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (gracz.position - sejf.transform.position).magnitude <= odleglosc_max && !otwarty)
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit) && 
                hit.transform.tag == "Przycisk" && hit.transform.parent.parent.parent.gameObject == sejf)
            {   
                if(hit.transform.name == "0" ||
                hit.transform.name == "1" || 
                hit.transform.name == "2" || 
                hit.transform.name == "3" || 
                hit.transform.name == "4" || 
                hit.transform.name == "5" || 
                hit.transform.name == "6" || 
                hit.transform.name == "7" || 
                hit.transform.name == "8" || 
                hit.transform.name == "9")
                {
                    if(tekst.Length < 6)
                {
                    tekst = tekst + hit.transform.name;
                    pole_tekstowe.text = tekst;
                }
                }
                else if(hit.transform.name == "Clear")
                {
                    tekst = "";
                    pole_tekstowe.text = tekst;
                }
                else if(hit.transform.name == "Return")
                {
                    if(tekst == haslo)
                    {
                        otwarty = true;
                        pole_tekstowe.text = "";
                        DOTween.Init();
                        drzwi.DOLocalRotate(koncowa_rotacja, 2f);
                    }
                    else
                    {
                        tekst = "";
                        pole_tekstowe.text = tekst;
                    }
                }
            }
        }
    }
}
