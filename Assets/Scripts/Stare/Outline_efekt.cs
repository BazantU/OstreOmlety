using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class Outline_efekt : MonoBehaviour
{   
    public Camera cam;
    public Player player_controller;
    private float max_odleglosc;

    public List<string> dozwolony_tag = new List<string>();

    [HideInInspector] public GameObject podswietlony_przedmiot;
    private Outline outline_przedmiotu;
    public Transform reka;



    private bool spelnia_wymagania(GameObject obiekt)
    {
        if(obiekt.GetComponent<Outline>() && obiekt.transform.parent != reka)
        {
            return true;
        }
        else
        {   
            return false;
        }
    }

    void Start()
    {
        //max_odleglosc = player_controller.max_od_obiektu;
        max_odleglosc = 1f;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
            && dozwolony_tag.Contains(hit.transform.tag))
        {   
            if(spelnia_wymagania(hit.transform.gameObject))
            {   
                Outline stary_outline = outline_przedmiotu;

                podswietlony_przedmiot = hit.transform.gameObject;
                outline_przedmiotu = podswietlony_przedmiot.GetComponent<Outline>();

                if(stary_outline && stary_outline.enabled){stary_outline.enabled = false;}
            }
        }
        else if(podswietlony_przedmiot && outline_przedmiotu)
        {   
            outline_przedmiotu.enabled = false;
            podswietlony_przedmiot = null;
            outline_przedmiotu = null;
        }
        if(podswietlony_przedmiot && outline_przedmiotu.enabled == false)
        {
            outline_przedmiotu.enabled = true;
        }
    }
}
