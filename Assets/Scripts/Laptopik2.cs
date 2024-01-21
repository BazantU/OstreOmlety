using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Laptopik2 : MonoBehaviour
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
    public GameObject kartka2;
    public bool pokazLaptoka2;
    public GameObject dzi;
    bool chuj32 = true;
    bool wTrakcie;
    public GameObject salceson;
   


    // Start is called before the first frame update
    void Start()
    {
        wTrakcie = false;
        pokazLaptoka = false;
        text1.text = null;
        rozwiaz = false;
        text1 = text.GetComponent<TextMesh>();
        kartka2.SetActive(false);
        pokazLaptoka2 = false;

    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !rozwiaz && PickupRouter.trzymany && !rozwiaz)
        {
            PickupRouter.trzymany2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !PickupRouter.trzymany && PickupRouter.udaloSie)
        {
            pPosition = player.position;
            pokazLaptoka = true;
            player.SetParent(cum);
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            cam.transform.localRotation = Quaternion.Euler(Vector3.zero);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            wTrakcie = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (pokazLaptoka) player.SetParent(cum);
        odleglosc = player.position - transform.position;


        if (Input.GetKeyDown(KeyCode.Escape) && wTrakcie)
        {
            pokazLaptoka = false;
            player.SetParent(null);
            player.transform.position = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            rozwiaz = false;
            tapeta.SetActive(false);
            text.SetActive(false);
            kartka2.SetActive(false);
            wTrakcie = false;
        }
        if ((text1.text == "RODO" || text1.text == "rodo") && Input.GetKeyDown(KeyCode.Return)) rozwiaz = true;
        //if (text1.text == "pup") rozwiaz = true;
        if (rozwiaz)
        {
            tapeta.SetActive(false);
            text.SetActive(false);
            kartka2.SetActive(true);
            pokazLaptoka2 = true;
            dzi.transform.localRotation = Quaternion.Euler(-90, 150, 180);
            dzi.transform.localPosition = new Vector3(0.172800004f, 0.0360002518f, -0.224999994f);
            pokazLaptoka = false;
            salceson.SetActive(false);
        }
        if (pokazLaptoka)
        {

            foreach (char c in Input.inputString)
            {
                if (c == '\b')
                {
                    if (text1.text.Length != 0)
                    {
                        text1.text = text1.text.Substring(0, text1.text.Length - 1);
                    }
                }
                else if (c == '\r')
                {
                    continue;
                }
                else if (text1.text.Length == 10)
                {
                    continue;
                }
                else
                {
                    text1.text += c;
                }
                while (chuj32 == true)
                {
                    chuj32 = false;
                    text1.text = null;
                }
            }


        }
    }

    private void LateUpdate()
    {
        if (!pokazLaptoka) schowajFunkcja();
        else if (pokazLaptoka) pokazFunkcja();
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
}
