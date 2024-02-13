using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class kod_biblia : MonoBehaviour
{
    public Camera cam;
    public Player playerController;
    private float max_odleglosc;

    public TMP_Text poleTekstowe;
    public int maxDlugoscHasla;

    [Header("Dostep")]
    public string poprawneHaslo;
    private string obecnyTekst = "";

    private bool wprowadzono_kod = false;

    [Header("Drzwi")]
    public Transform drzwiObiekt;
    public Vector3 koncowaRotacja;
    public float czasOtwierania;

    void Start()
    {
        max_odleglosc = playerController.maxOdleglosc;
        poleTekstowe.text = obecnyTekst;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc) 
                && hit.transform.tag == "Przycisk2"
                && !wprowadzono_kod) 
            {   
                if(hit.transform.name == "Return")
                {   
                    if(obecnyTekst == poprawneHaslo)
                    {
                        wprowadzono_kod = true;
                        DOTween.Init();
                        drzwiObiekt.DOLocalRotate(koncowaRotacja, czasOtwierania);

                    }
                }
                else if(hit.transform.name == "Clear")
                {
                    obecnyTekst = "";
                    poleTekstowe.text = obecnyTekst;
                }
                else
                {
                    if(obecnyTekst.Length < maxDlugoscHasla)
                    {
                        obecnyTekst += hit.transform.name;
                        poleTekstowe.text = obecnyTekst;
                    }
                }
            }
        }
    }
}
