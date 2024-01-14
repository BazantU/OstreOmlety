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
    float podnoszenie;
    public Transform dzi;
    public GameObject text1;
    public GameObject text2;

    
    void Start()
    {
        rozwiazanoTerminal = false;
        textMesh.text = null;
        rozwiazujeTerminal = false;
        textMesh.GetComponent<TextMesh>();
        text2.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && !rozwiazanoTerminal)
        {
            rozwiazujeTerminal = true;
            pPosition = player.transform.position;
            player.transform.SetParent(cum);
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(Vector3.zero);
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }

    void Update()
    {
        if (textMesh.text == "uodo" || textMesh.text == "UODO")
        {
            rozwiazanoTerminal = true;
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
                else
                {
                    textMesh.text += c;
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
        if (rozwiazanoTerminal)
        {
            rozwiazujeTerminal = false;
            player.transform.SetParent(null);
            player.transform.localPosition = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            dzi.transform.localRotation = Quaternion.Euler(-90, 0, 90);
            text2.SetActive(true);
            text1.SetActive(false);
        }
    }
}
