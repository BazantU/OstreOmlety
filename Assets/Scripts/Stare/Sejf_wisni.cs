using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Security.Authentication.ExtendedProtection;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;

public class Sejf_wisni : MonoBehaviour
{
    public Transform drzwi;
    public GameObject sejf;
    public Vector3 koncowa_rotacja;

    public Global_interakcja_zasieg global;
    public Transform gracz;
    public Camera cam;
    public GameObject parent_przyciski;
    public string haslo;

    private float odleglosc_max;
    private bool otwarty = false;

    //============================//
    public TMP_Text tekst_1;
    public TMP_Text tekst_2;
    public TMP_Text tekst_3;
    public TMP_Text tekst_4;

    private float Liczba1 = 6f;
    private float Liczba2 = 0f;
    private float Liczba3 = 5f;
    private float Liczba4 = 5f;
    //============================//

    private float zczekuj_czy_dobra_liczba(float liczba, float co_dodac_)
    {   
        //if(liczba > 9){return 0;}
        //else if(liczba < 0){return 0;}
        //else{return co_dodac_;}
        float wynik = liczba + co_dodac_;
        if(wynik >= 10){return 0;}
        else if(wynik <= -1){return 0;}
        else{return co_dodac_;}
    }

    void Start()
    {
        odleglosc_max = global.odleglosc;
        tekst_1.text = Liczba1.ToString();
        tekst_2.text = Liczba2.ToString();
        tekst_3.text = Liczba3.ToString();
        tekst_4.text = Liczba4.ToString();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (gracz.position - sejf.transform.position).magnitude <= odleglosc_max && !otwarty)
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit) && 
                hit.transform.tag == "Przycisk" && (hit.transform.parent.parent.parent.parent.gameObject == sejf || hit.transform.parent.parent.parent.gameObject == sejf))
            {   
                if(hit.transform.name == "Return")
                {
                    string wpisane_haslo = Liczba1.ToString() + Liczba2.ToString() + Liczba3.ToString() + Liczba4.ToString();
                    if(wpisane_haslo == haslo)
                    {
                        otwarty = true;
                        DOTween.Init();
                        drzwi.DOLocalRotate(koncowa_rotacja, 2f, RotateMode.FastBeyond360);
                    }
                }
                else if(hit.transform.name == "Clear")
                {
                    Liczba1 = 6; tekst_1.text = Liczba1.ToString();
                    Liczba2 = 0; tekst_2.text = Liczba2.ToString();
                    Liczba3 = 5; tekst_3.text = Liczba3.ToString();
                    Liczba4 = 5; tekst_4.text = Liczba4.ToString();
                }
                else
                {
                    bool dzialaj = false;
                    GameObject pole = hit.transform.parent.gameObject;
                    Transform pole_tekstowe = pole.transform.Find("Canvas").Find("Text (TMP)");

                    TMP_Text tekst;
                    if(pole.transform.name == "Show1"){tekst = tekst_1; dzialaj = true;}
                    else if(pole.transform.name == "Show2"){tekst = tekst_2; dzialaj = true;}
                    else if(pole.transform.name == "Show3"){tekst = tekst_3; dzialaj = true;}
                    else if(pole.transform.name == "Show4"){tekst = tekst_4; dzialaj = true;}
                    else{tekst = tekst_1;}

                    if(dzialaj)
                    {   
                        float co_dodac = 0f;
                        char index = hit.transform.name[0];

                        if(hit.transform.name.Contains("Gora")){co_dodac = 1f;}
                        else if(hit.transform.name.Contains("Dol")){co_dodac = -1f;} 


                        if(index.ToString() == "1"){Liczba1 += zczekuj_czy_dobra_liczba(Liczba1, co_dodac); tekst.text = Liczba1.ToString();}
                        else if(index.ToString() == "2"){Liczba2 += zczekuj_czy_dobra_liczba(Liczba2, co_dodac); tekst.text = Liczba2.ToString();}
                        else if(index.ToString() == "3"){Liczba3 += zczekuj_czy_dobra_liczba(Liczba3, co_dodac); tekst.text = Liczba3.ToString();}
                        else if(index.ToString() == "4"){Liczba4 += zczekuj_czy_dobra_liczba(Liczba4, co_dodac); tekst.text = Liczba4.ToString();}
                    }
                }
            }
        }
    }
}
