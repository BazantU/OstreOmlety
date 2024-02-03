using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Czytnik : MonoBehaviour
{   
    public Transform klucz;

    [Header("Materialy")]
    public GameObject zielony;
    public GameObject czerwony;

    private Material zielony_mat;
    private Material czerwony_mat;

    private DrzwiKlasa drzwi_klasa;
    
    private bool wlaczone = false;
    private float max_odleglosc;

    [Header("Drzwi")]
    public Transform drzwi;

    [Header("Inne")]
    public Player controller;
    public Itemy itemy_system;
    public Camera cam;
    public Transform reka;
    public Drzwi drzwi_skrypt;

    private void koniec()
    {
        czerwony_mat.DisableKeyword("_EMISSION");

        zielony_mat.EnableKeyword("_EMISSION");
        zielony_mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        zielony_mat.SetColor("_EmissionColor", zielony_mat.color);
        zielony_mat.SetFloat("_EmissionIntensity", 1f);

        klucz.position = new Vector3(0f, -100f, 0);
        drzwi_klasa.obecna_wartosc += 1;
    }

    private void przesun_karte()
    {
        DOTween.Init();
        klucz.DOMove(klucz.position + new Vector3(0f, -0.125f, 0f), 0.5f).OnComplete(() => koniec());
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

        max_odleglosc = controller.maxOdleglosc;
    }

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
                && !wlaczone
                && hit.transform == transform
                && reka.childCount > 0
                && klucz.IsChildOf(reka))
            {   
                drzwi_klasa = drzwi_skrypt.get_odpowiednie_drzwi(drzwi);
                if(drzwi_klasa != null)
                {
                    wlaczone = true;
                    transform.tag = "Untagged";
                    klucz.tag = "Untagged";

                    itemy_system.trzymany_przedmiot = null;
                    itemy_system.rg_body = null;

                    klucz.SetParent(null);
                    klucz.rotation = Quaternion.Euler(180f, 0f, -20f);
                    klucz.position = new Vector3(5.635f,4.7f,16.385f);

                    Invoke("przesun_karte", 0.5f);
                }
            }
        }
    }
}
