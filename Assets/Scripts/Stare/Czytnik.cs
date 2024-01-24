using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class Czytnik : MonoBehaviour
{   
    public GameObject zielony;
    [HideInInspector] public Material zielony_mat;

    public GameObject czerwony;
    [HideInInspector] public Material czerwony_mat;

    private bool wlaczone = false;
    private Outline outline_czytnika;

    public Transform klucz;

    private bool nie_bylo_animacji = true;
    private float wait_for = 0f;

    [Space]
    public Player controller;
    public Podnoszenie_przedmiotow itemy_system;
    public Camera cam;
    public Transform reka;
    public Drzwi drzwi_skrypt;
    private float max_odleglosc;

    private List<DrzwiKlasa> lista_drzwi;

    [Space]
    public Transform drzwi;

    private void koniec()
    {
        czerwony_mat.DisableKeyword("_EMISSION");

        zielony_mat.EnableKeyword("_EMISSION");
        zielony_mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        zielony_mat.SetColor("_EmissionColor", zielony_mat.color);
        zielony_mat.SetFloat("_EmissionIntensity", 1f);

        klucz.position = new Vector3(0f, -100f, 0);
        foreach(var obiekt_w_klasie in lista_drzwi)
        {
            if(obiekt_w_klasie.obiekt == drzwi)
            {
                obiekt_w_klasie.obecna_wartosc += 1;
            }
        }
    }

    void Start()
    {   
        Renderer renderer_1 = zielony.GetComponent<Renderer>();
        Material originalMaterial = renderer_1.material;
        zielony_mat = new Material(originalMaterial);
        renderer_1.material = zielony_mat;

        Renderer renderer_2 = czerwony.GetComponent<Renderer>();
        Material originalMaterial2 = renderer_2.material;
        czerwony_mat = new Material(originalMaterial2);
        renderer_2.material = czerwony_mat;

        czerwony_mat.EnableKeyword("_EMISSION");
        czerwony_mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        czerwony_mat.SetColor("_EmissionColor", originalMaterial2.color);
        czerwony_mat.SetFloat("_EmissionIntensity", 1f);

        //max_odleglosc = controller.max_od_obiektu;
        max_odleglosc = 1f;
        outline_czytnika = transform.GetComponent<Outline>();
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

            itemy_system.trzymany_przedmiot = null;
            klucz.SetParent(null);
            klucz.rotation = Quaternion.Euler(0f, 0f, 160f);
            klucz.position = new Vector3(4.8405f,4.6705f,16.0883f);
        }
        if(wlaczone && nie_bylo_animacji)
        {   
            if(wait_for >= 0.5f)
            {
                nie_bylo_animacji = false;
                DOTween.Init();
                klucz.DOMove(klucz.position + new Vector3(0f, -0.125f, 0f), 0.5f).OnComplete(koniec);
            }
            else
            {
                wait_for += Time.deltaTime;
            }
        }
    }
}
