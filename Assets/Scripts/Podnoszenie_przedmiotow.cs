using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podnoszenie_przedmiotow : MonoBehaviour
{
    public Camera cam;
    public Transform reka;
    public Player player_controller;
    private float max_odleglosc;

    [HideInInspector] public Transform trzymany_przedmiot;
    private Rigidbody rg_body;

    public float dropForwardForce, dropUpwardForce;

    private bool spelnia_wymagania(GameObject obiekt)
    {
        if(obiekt.GetComponent<Rigidbody>())
        {
            return true;
        }
        else
        {   
            Debug.LogWarning($"Przedmiot: [{obiekt}] nie spelnia wymagan!");
            return false;
        }
    }

    void Start()
    {
        max_odleglosc = player_controller.max_od_obiektu;
    }

    void Update()
    {   
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if(Input.GetKeyDown(KeyCode.E) 
            && Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
            && trzymany_przedmiot == null
            && hit.transform.tag == "Item"
            && spelnia_wymagania(hit.transform.gameObject))
        {   
            trzymany_przedmiot = hit.transform;
            rg_body = trzymany_przedmiot.GetComponent<Rigidbody>();
            rg_body.isKinematic = true;

            trzymany_przedmiot.SetParent(reka, true);
            trzymany_przedmiot.localPosition = new Vector3(0f, 0f, 0f);
            trzymany_przedmiot.localRotation = Quaternion.Euler(Vector3.zero);
        }
        if(Input.GetKeyDown(KeyCode.Q) && trzymany_przedmiot)
        {   
            trzymany_przedmiot.SetParent(null);
            rg_body.isKinematic = false;
            trzymany_przedmiot = null;

            rg_body.AddForce(cam.transform.forward * dropForwardForce, ForceMode.Impulse);
            rg_body.AddForce(cam.transform.up * dropUpwardForce, ForceMode.Impulse);
            //float random = Random.Range(-1f, 1f);
            //rg_body.AddTorque(new Vector3(random, random, random));
        }
    }
}
