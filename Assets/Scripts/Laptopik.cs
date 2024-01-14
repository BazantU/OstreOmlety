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
    public GameObject kartka2;
    public bool pokazLaptoka2;
    public GameObject dzi;


    // Start is called before the first frame update
    void Start()
    {
        pokazLaptoka = false;
        text1.text = null;
        rozwiaz = false;
        text1 = text.GetComponent<TextMesh>();
        kartka2.SetActive(false);
        pokazLaptoka2 = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !rozwiaz && PendriveDoLaptopika.trzymany)
        {
            PendriveDoLaptopika.trzymany2 = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && odleglosc.magnitude <= odlegloscPodnoszenia && !PendriveDoLaptopika.trzymany && PendriveDoLaptopika.udaloSie)
        {
            pPosition = player.position;
            pokazLaptoka = true;
            player.SetParent(cum);
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);

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


        if (Input.GetKeyDown(KeyCode.Escape) && pokazLaptoka)
        {
            pokazLaptoka = false;
            player.SetParent(null);
            player.transform.position = pPosition;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


        }
        if (text1.text == "cidH8Wp)qi8K(b!M" && Input.GetKeyDown(KeyCode.Return)) rozwiaz = true;
        //if (text1.text == "pup") rozwiaz = true;
        if (rozwiaz)
        {
            text.SetActive(false);
            if (pokazLaptoka) kartka2.SetActive(true);
            else kartka2.SetActive(false);
            pokazLaptoka2 = true;
            dzi.transform.localRotation = Quaternion.Euler(-90, 150, 180);
            dzi.transform.localPosition = new Vector3(0.172800004f, 0.0360002518f, -0.224999994f);
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
        if (pokazLaptoka2) text.SetActive(false);
    }
    void schowajFunkcja()
    {
        tapeta.SetActive(false);
        text.SetActive(false);
    }
}
