using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class PC_interakcja : MonoBehaviour
{
    public GameObject urzadzenie;
    public Transform gracz;
    public Camera cam;
    public Global_interakcja_zasieg global_interakcja;
    public TMP_Text pole_tekstowe;
    public string haslo;
    public RawImage test;

    public Camera cam_na_kompa;

    private float odleglosc_max;
    private bool aktywowany;
    private bool mozna_wpisac = true;

    private string tekst;

    private float poczekaj = 0f;
    private float ile_czekac = 0.1f;

    void Start()
    {   
        odleglosc_max = global_interakcja.odleglosc;
        aktywowany = false;
        pole_tekstowe.text = tekst;
    }
    
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E) && (gracz.position - urzadzenie.transform.position).magnitude <= odleglosc_max && !aktywowany)
        {   
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject == urzadzenie)
            {
                aktywowany = true;
                gracz.gameObject.GetComponent<Player>().enabled = false;
                cam.GetComponent<SC_HeadBobber>().enabled = false;

                cam_na_kompa.enabled = true;
                cam.enabled = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q) && aktywowany)
        {   
            aktywowany = false;
            gracz.gameObject.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;

            cam.enabled = true;
            cam_na_kompa.enabled = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pole_tekstowe.text = tekst;
            poczekaj = 0f;
        }
        if(aktywowany && mozna_wpisac)
        {   
            if(poczekaj < ile_czekac)
            {
                poczekaj += Time.deltaTime;
            }
            else if(poczekaj >= ile_czekac) //ma sie dziać
            {
                foreach (char znak in Input.inputString)
                {
                if (znak >= 32 && znak <= 126)
                    {
                        tekst += znak;
                    }
                }
            }
            pole_tekstowe.text = tekst;
        }
        if(Input.GetKeyDown(KeyCode.Backspace) && aktywowany)
        {
            if (tekst.Length > 0)
            {
                tekst = tekst.Substring(0, tekst.Length - 1);
            }
        }
        if(Input.GetKeyDown(KeyCode.Return) && aktywowany)
        {
            if(tekst == haslo)
            {   
                mozna_wpisac = false;

                //cos sie zrobi po wpisanu
                pole_tekstowe.enabled = false;
                test.color = Color.green;
            }
        }
    }
}
