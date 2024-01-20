using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Zamek1 : MonoBehaviour
{   
    public Podnoszenie_przedmiotow itemy_system;
    public Drzwi drzwi_skrypt;
    public Player controller;
    public Transform reka;
    public Camera cam;
    public Transform klucz;

    private bool wlaczone = false;
    private float max_odleglosc;

    private List<DrzwiKlasa> lista_drzwi;

    [Space]
    public Transform drzwi;

    void DodajWartosc()
    {
        foreach (var obiekt_w_klasie in lista_drzwi)
        {
            if (obiekt_w_klasie.obiekt == drzwi)
            {
                obiekt_w_klasie.obecna_wartosc += 1;
            }
        }
    }

    void Start()
    {
        max_odleglosc = controller.max_od_obiektu;
        lista_drzwi = drzwi_skrypt.drzwi_specjalne;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Input.GetKeyDown(KeyCode.E) 
            && !wlaczone
            && Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
            && hit.transform == transform
            && reka.childCount > 0
            && klucz.IsChildOf(reka))
        {
            wlaczone = true;
            transform.tag = "Untagged";
            klucz.tag = "Untagged";

            transform.GetComponent<Collider>().enabled = false;

            itemy_system.trzymany_przedmiot = null;
            klucz.SetParent(null);
            klucz.position = new Vector3(0f, -100f, 0f);

            Invoke("DodajWartosc", 0.1f);
        }
    }
}
