using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class TerminalW211 : MonoBehaviour
{
    public static bool rozwiazanoTerminal;
    public static bool rozwiazujeTerminal;
    public GameObject terminal;
    public GameObject text;
    public TextMesh textMesh;
    public Transform cum;
    public Transform player;
    public Transform cam;
    Vector3 pPosition;
    Vector3 odleglosc;
    public float podnoszenie;
    public Transform dzi;
    public GameObject text1;
    public GameObject text2;
    bool chuj32 = true;
    public GameObject roletq;
    public GameObject roletq1;
    bool chujniaOdDzi;
    bool roz = true;

    bool mouseOver = false;

    void Start()
    {
        chujniaOdDzi = true;
        roletq.SetActive(true);
        roletq1.SetActive(false);
        rozwiazanoTerminal = false;
        textMesh.text = null;
        rozwiazujeTerminal = false;
        textMesh.GetComponent<TextMesh>();
        text2.SetActive(false);
    }

    private void OnMouseExit(){
        mouseOver = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && !rozwiazanoTerminal && odleglosc.magnitude <= podnoszenie)
        {
            rozwiazujeTerminal = true;
            pPosition = player.transform.position;
            player.transform.SetParent(cum);
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(Vector3.zero);
            cam.transform.localRotation = Quaternion.Euler(Vector3.zero);
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        if(odleglosc.magnitude < podnoszenie){
            transform.gameObject.layer = LayerMask.NameToLayer("Outline");
        }
        mouseOver = true;
    }

    void Update()
    {
        if(odleglosc.magnitude > podnoszenie || !mouseOver){
            transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        odleglosc = player.position - transform.position;
        if (textMesh.text == "uodo" || textMesh.text == "UODO")
        {
            if(!rozwiazanoTerminal){
                player.GetComponent<DzwiekHandler>().graj("pass_kubel");
            }
            rozwiazanoTerminal = true;
            textMesh.text = null;
            chujniaOdDzi = true;
            roz = true;
        }
        if (!rozwiazujeTerminal)
        {
            terminal.SetActive(false);
            text.SetActive(false);
        }
        else
        {
            terminal.SetActive(true);
            text.SetActive(true);
            foreach (char c in Input.inputString)
            {
                if (c ==  '\b')
                {
                    if (textMesh.text.Length != 0)
                    {
                        textMesh.text = textMesh.text.Substring(0, textMesh.text.Length - 1);
                    }
                }
                else if (c == '\r')
                {
                    continue;
                }
                else if (textMesh.text.Length == 6)
                {
                    continue;
                }
                else
                {
                    textMesh.text += c;
                }
                while (chuj32 == true)
                {
                    chuj32 = false;
                    textMesh.text = null;
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && rozwiazujeTerminal)
        {
            rozwiazujeTerminal = false;
            player.transform.SetParent(null);
            player.transform.localPosition = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        while (rozwiazanoTerminal)
        {
            rozwiazanoTerminal = false;
            rozwiazujeTerminal = false;
            player.transform.SetParent(null);
            player.transform.localPosition = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            while (chujniaOdDzi)
            {
                chujniaOdDzi = false;
                dzi.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            }
            text2.SetActive(true);
            text1.SetActive(false);
            roletq1.SetActive(true);
            roletq.SetActive(false);
        }
        if (odleglosc.magnitude >= podnoszenie && !chujniaOdDzi)
        {
            while (roz)
            {
                roz = false;
                dzi.transform.localRotation = Quaternion.Euler(-90, 0, 0);
            }
        }
    }
}
