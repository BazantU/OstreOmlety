using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Laptopik : MonoBehaviour
{
    public GameObject tapeta;
    public Transform cum;
    public GameObject text;
    public TextMesh text1;
    public bool pokazLaptoka;
    Vector3 odleglosc;
    public Transform player;
    public float odlegloscPodnoszenia;
    Vector3 pPosition;
    public GameObject cam;
    public static bool rozwiaz;


    // Start is called before the first frame update
    void Start()
    {
        pokazLaptoka = false;
        text1.text = null;
        rozwiaz = false;
        text1 = text.GetComponent<TextMesh>();

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !Pickup.slotFull && !Pickup2.slotFull && !rozwiaz)
        {
            pPosition = player.position;
            pokazLaptoka = true;
            player.SetParent(cum);
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Pickup.slotFull = true;
            Pickup2.slotFull = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        odleglosc = player.position - transform.position;


        if (Input.GetKeyDown(KeyCode.Escape) && Pickup.slotFull && Pickup2.slotFull && pokazLaptoka)
        {
            pokazLaptoka = false;
            player.SetParent(null);
            player.transform.position = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Pickup.slotFull = false;
            Pickup2.slotFull = false;

        }

        if (pokazLaptoka)
        {
            
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // has backspace/delete been pressed?
                {
                    if (text1.text.Length != 0)
                    {
                        text1.text = text1.text.Substring(0, text1.text.Length - 1);
                    }
                }
                else if ((c == '\n') || (c == '\r')) // enter/return
                {
                    checkIf();
                }
                else if (rozwiaz)
                {
                    pokazLaptoka = false;
                    player.SetParent(null);
                    player.transform.position = pPosition;
                    player.GetComponent<Player>().enabled = true;
                    cam.GetComponent<SC_HeadBobber>().enabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Pickup.slotFull = false;
                    Pickup2.slotFull = false;
                }
                else
                {
                    text1.text += c;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (!pokazLaptoka) schowajFunkcja();
        else if (pokazLaptoka) pokazFunkcja();

        if (pokazLaptoka) player.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    void pokazFunkcja()
    {
        tapeta.SetActive(true);
        text.SetActive(true);
    }
    void schowajFunkcja()
    {
        tapeta.SetActive(false);
        text.SetActive(false);
    }
    void checkIf()
    {
        if (text1.text == "cidH8Wp)qi8K(b!M") rozwiaz = true;
    }
}
