using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Drzwi : MonoBehaviour
{   
    [Header("Main")]
    public Camera cam;
    public float maxOdleglosc = 1f;

    [Header("Drzwi")]
    public Vector3 oIleRotacja = new Vector3(0f, -150f, 0f);
    public float czasOtwierania = 0.75f;
    bool czyOtwarteDrzwi = false;
    Vector3 poczatkowaRotacja;

    void otworz_drzwi()
    {
        czyOtwarteDrzwi = true;
        DOTween.Init();
        transform.DOLocalRotate(poczatkowaRotacja + oIleRotacja, czasOtwierania);
    }

    void zamknij_drzwi()
    {
        czyOtwarteDrzwi = false;
        DOTween.Init();
        transform.DOLocalRotate(poczatkowaRotacja, czasOtwierania);
    }

    void Start()
    {
        poczatkowaRotacja = transform.localRotation.eulerAngles;
    }

    void OnMouseOver()
    {   
        float odlegosc = Vector3.Distance(transform.position, cam.transform.position);
        if(Input.GetKeyDown(KeyCode.E) && odlegosc <= maxOdleglosc)
        {
            if(!czyOtwarteDrzwi){otworz_drzwi();}
            else if(czyOtwarteDrzwi){zamknij_drzwi();}
        }
    }
}
