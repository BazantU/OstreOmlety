using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemy : MonoBehaviour
{
    public Camera cam;
    public Transform reka;

    private Player player_controller;
    private float max_odleglosc;

    [Header("Specjalne przedmioty")]
    public List<SpecjalnePrzedmioty> Przedmioty = new List<SpecjalnePrzedmioty>();
    
    [HideInInspector] public Transform trzymany_przedmiot;
    [HideInInspector] public Rigidbody rg_body;

    private bool spelnia_wymagania(GameObject obiekt)
    {
        if(obiekt.GetComponent<Rigidbody>())
        {
            return true;
        }
        Debug.LogWarning($"Przedmiot: [{obiekt}] nie spelnia wymagan!");
        return false;
    }

    private Tuple<bool, Vector3, Vector3> Dane(Transform przedmiot)
    {
        foreach(var sp_przedmiot in Przedmioty)
        {
            if(sp_przedmiot.przedmiot == przedmiot)
            {
                return Tuple.Create(true, sp_przedmiot.rotacja, sp_przedmiot.offset);
            }
        }
        return Tuple.Create(false, Vector3.zero, Vector3.zero);
    }

    private void podnies_przedmiot(Transform przedmiot)
    {
        trzymany_przedmiot = przedmiot;
        rg_body = przedmiot.GetComponent<Rigidbody>();
        rg_body.isKinematic = true;
        trzymany_przedmiot.SetParent(reka, true);

        var rezultat = Dane(przedmiot);

        if(rezultat.Item1)
        {   
            trzymany_przedmiot.localRotation = Quaternion.Euler(rezultat.Item2);
            trzymany_przedmiot.localPosition = rezultat.Item3;
        }
        else
        {
            trzymany_przedmiot.localPosition = new Vector3(0f, 0f, 0f);
            trzymany_przedmiot.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void upusc_przedmiot()
    {
        trzymany_przedmiot.SetParent(null);
        rg_body.isKinematic = false;

        rg_body.AddForce(cam.transform.forward * 1.25f, ForceMode.Impulse);
        rg_body.AddForce(cam.transform.up * 1.5f, ForceMode.Impulse);

        trzymany_przedmiot = null;
        rg_body = null;
    }

    void Start()
    {
        player_controller = transform.GetComponent<Player>();
        max_odleglosc = player_controller.maxOdleglosc;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if(Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
                && trzymany_przedmiot == null
                && hit.transform.tag == "Item"
                && spelnia_wymagania(hit.transform.gameObject))
            {
                podnies_przedmiot(hit.transform);
            }
        }
        if(Input.GetKeyDown(KeyCode.Q) && trzymany_przedmiot && rg_body)
        {   
            upusc_przedmiot();
        }
    }
}

[Serializable]
public class SpecjalnePrzedmioty
{   
    public Transform przedmiot;
    public Vector3 rotacja;
    public Vector3 offset;
}
