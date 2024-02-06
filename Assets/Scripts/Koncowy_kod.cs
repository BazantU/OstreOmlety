using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Koncowy_kod : MonoBehaviour
{   
    public int haslo;

    [Header("Materialy")]
    public GameObject zielony;
    public GameObject czerwony;

    private Material zielony_mat;
    private Material czerwony_mat;

    [Header("Inne")]
    public Camera cam;
    public Player playerController;
    public Pokretlo_do_sejfu pokretlo_kod;
    private float max_odleglosc;
    private bool mozna_zmieniac = true;
    private string placeholder;

    public List<PoleKodu> polaNaKod = new List<PoleKodu>();

    private void zakocz()
    {
        mozna_zmieniac = false;

        czerwony_mat.DisableKeyword("_EMISSION");

        zielony_mat.EnableKeyword("_EMISSION");
        zielony_mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        zielony_mat.SetColor("_EmissionColor", zielony_mat.color);
        zielony_mat.SetFloat("_EmissionIntensity", 1f);

        pokretlo_kod.mozna_krecic = true;
    }

    private string get_ustawione_haslo()
    {   
        placeholder = "";
        foreach(var kod_klasa in polaNaKod)
        {   
            placeholder += kod_klasa.obecnaWartosc.ToString();
        }
        return placeholder;
    }

    private string gora_czy_dol(PoleKodu klasa, Transform przycisk)
    {   
        if(przycisk == klasa.strzalkaDoGory)
        {
            return "Gora";
        }
        if(przycisk == klasa.strzalkaDoDolu)
        {
            return "Dol";
        }
        return "Null";
    }

    private PoleKodu get_odpowiednie_pole(Transform przycisk)
    {   
        foreach(var kod_klasa in polaNaKod)
        {
            if(przycisk == kod_klasa.strzalkaDoGory || przycisk == kod_klasa.strzalkaDoDolu)
            {
                return kod_klasa;
            }
        }
        return null;
    }

    void Start()
    {
        max_odleglosc = playerController.maxOdleglosc;
        foreach(var kod_klasa in polaNaKod)
        {
            kod_klasa.obecnaWartosc = kod_klasa.wartoscPoczatkowa;
            kod_klasa.poleZWartoscia.text = kod_klasa.obecnaWartosc.ToString();
        }

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
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
                && mozna_zmieniac 
                && hit.transform.tag == "Przycisk" 
                && (hit.transform.parent.parent.parent.parent == transform || hit.transform.parent.parent.parent == transform))
            {
                if(hit.transform.name == "Return")
                {   
                    if(get_ustawione_haslo() == haslo.ToString())
                    {
                        zakocz();
                    }
                }
                else if(hit.transform.name == "Clear")
                {
                    foreach(var kod_klasa in polaNaKod)
                    {
                        kod_klasa.obecnaWartosc = kod_klasa.wartoscPoczatkowa;
                        kod_klasa.poleZWartoscia.text = kod_klasa.obecnaWartosc.ToString();
                    }
                }
                else
                {   
                    PoleKodu kod_klasa = get_odpowiednie_pole(hit.transform);
                    if(kod_klasa != null)
                    {
                        if(gora_czy_dol(kod_klasa, hit.transform) == "Gora" && kod_klasa.obecnaWartosc < 9)
                        {
                            kod_klasa.obecnaWartosc += 1;
                            kod_klasa.poleZWartoscia.text = kod_klasa.obecnaWartosc.ToString();
                        }
                        if(gora_czy_dol(kod_klasa, hit.transform) == "Dol" && kod_klasa.obecnaWartosc > 0)
                        {
                            kod_klasa.obecnaWartosc -= 1;
                            kod_klasa.poleZWartoscia.text = kod_klasa.obecnaWartosc.ToString();
                        }
                    }
                }
            }
        }
    }
}

[Serializable]
public class PoleKodu
{
    public Transform strzalkaDoGory;
    public TMP_Text poleZWartoscia;
    public Transform strzalkaDoDolu;

    [Header("Inne")]
    public int wartoscPoczatkowa;

    [HideInInspector] public int obecnaWartosc;
}
