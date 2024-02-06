using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Podswietlenie : MonoBehaviour
{
    public Camera cam;
    public Transform reka;
    public List<string> tagi = new List<string>();

    private Player player_controller;
    private float max_odleglosc;

    private Transform podswietlony_przedmiot;
    private Outline outline_przedmiotu;

    private bool mozna_podswietlic(Transform obiekt)
    {   
        if(obiekt.GetComponent<Outline>() && obiekt.parent != reka)
        {
            return true;
        }
        Debug.LogWarning($"{obiekt} nie spelnia wymagan!");
        return false;
    }

    private void usun_stary_outline()
    {   
        if(outline_przedmiotu){outline_przedmiotu.enabled = false;}
        podswietlony_przedmiot = null;
        outline_przedmiotu = null;
    }

    private void add_outline(Transform obiekt)
    {   
        usun_stary_outline();
        podswietlony_przedmiot = obiekt;
        outline_przedmiotu = obiekt.GetComponent<Outline>();
        outline_przedmiotu.enabled = true;
    }

    void Start()
    {
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
            && tagi.Contains(hit.transform.tag)
            && cam.enabled)
        {   
            if(mozna_podswietlic(hit.transform))
            {   
                add_outline(hit.transform);
            }
        }
        else if(podswietlony_przedmiot){usun_stary_outline();}
    }
}
