using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KlodkaDoSkrzyniaka : MonoBehaviour
{
    public bool rozwiazanaZagadka;
    public Transform player;
    public float pickUpRange;
    public Transform cum;
    public Transform cam;
    public TextMesh text1;
    public TextMesh text2;
    public TextMesh text3;
    public TextMesh text4;
    public TextMesh text5;
    public GameObject text11;
    public GameObject text12;
    public GameObject text13;
    public GameObject text14;
    public GameObject text15;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public GameObject plane4;
    public GameObject plane5;
    public GameObject plane6;
    public GameObject plane7;
    public GameObject plane8;
    public GameObject plane9;
    public GameObject plane10;
    public GameObject plane11;
    public GameObject oska;
    Vector3 pPosition;
    public bool pokaz;
    public Pickup pickup;
    public PickupKartka pickup2;
    public GameObject klapa;
    int papa1 = 1;
    int papa2 = 1;
    Vector3 distanceToPlayer;
    public BoxCollider kolajder;
    public GameObject pen;
    

    public bool rozwiazanaZagadka2;


    // Start is called before the first frame update
    void Start()
    {
        otwartaKlodka = false;
        rozwiazanaZagadka = false;
        rozwiazanaZagadka2 = false;
        pokaz = false;
        pen.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.E) && distanceToPlayer.magnitude <= pickUpRange && !Pickup.slotFull && !PickupKartka.slotFull)
        {
            Pickup.slotFull = true;
            PickupKartka.slotFull = true;
            pokaz = true;
            pPosition = player.transform.position;
            text11.SetActive(true);
            text12.SetActive(true);
            text13.SetActive(true);
            text14.SetActive(true);
            text15.SetActive(true);
            plane1.SetActive(true);
            plane2.SetActive(true);
            plane3.SetActive(true);
            plane4.SetActive(true);
            plane5.SetActive(true);
            plane6.SetActive(true);
            plane7.SetActive(true);
            plane8.SetActive(true);
            plane9.SetActive(true);
            plane10.SetActive(true);
            plane11.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            player.transform.SetParent(cum);
            cam.transform.localPosition = Vector3.zero;
            cam.transform.localRotation = Quaternion.Euler(Vector3.zero);
            player.GetComponent<Player>().enabled = false;
            cam.GetComponent<SC_HeadBobber>().enabled = false;
            player.transform.localPosition = Vector3.zero;
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
    }
    void Update()
    {
        if (!pokaz)
        {
            text11.SetActive(false);
            text12.SetActive(false);
            text13.SetActive(false);
            text14.SetActive(false);
            text15.SetActive(false);
            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            plane4.SetActive(false);
            plane5.SetActive(false);
            plane6.SetActive(false);
            plane7.SetActive(false);
            plane8.SetActive(false);
            plane9.SetActive(false);
            plane10.SetActive(false);
            plane11.SetActive(false);
        }

        distanceToPlayer = player.position - transform.position;
        
        
        if (text1.text == "H" && text2.text == "E" && text3.text == "Z" && text4.text == "O" && text5.text == "A") 
        {
            rozwiazanaZagadka = true;
        }

        if (text1.text == "8" && text2.text == "1" && text3.text == "1" && text4.text == "1" && text5.text == "1")
        {
            rozwiazanaZagadka2 = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && distanceToPlayer.magnitude <= pickUpRange && player.transform.parent == cum)
        {
            Pickup.slotFull = false;
            PickupKartka.slotFull = false;
            pokaz = false;
            player.transform.SetParent(null);
            player.transform.position = pPosition;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
            text11.SetActive(false);
            text12.SetActive(false);
            text13.SetActive(false);
            text14.SetActive(false);
            text15.SetActive(false);
            plane1.SetActive(false);
            plane2.SetActive(false);
            plane3.SetActive(false);
            plane4.SetActive(false);
            plane5.SetActive(false);
            plane6.SetActive(false);
            plane7.SetActive(false);
            plane8.SetActive(false);
            plane9.SetActive(false);
            plane10.SetActive(false);
            plane11.SetActive(false);
        }
        
        if (rozwiazanaZagadka && papa1 == 1)
        {
            papa1 = 0;
            Pickup.slotFull = false;
            PickupKartka.slotFull = false;
            pokaz = false;
            oska.transform.localRotation = Quaternion.Euler(-89.98f, -180f, 0f);
            otwartaKlodka = true;
            player.transform.SetParent(null);
            player.transform.position = pPosition;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
        }
        if (rozwiazanaZagadka2 && papa2 == 1)
        {
            papa2 = 0;
            Pickup.slotFull = false;
            PickupKartka.slotFull = false;
            pokaz = false;
            oska.transform.localRotation = Quaternion.Euler(-89.98f, -180f, 0f);
            otwartaKlodka = true;
            player.transform.SetParent(null);
            player.transform.position = pPosition;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<Player>().enabled = true;
            cam.GetComponent<SC_HeadBobber>().enabled = true;
        }
        if (rozwiazanaZagadka && distanceToPlayer.magnitude <= pickUpRange)
        {
            klapa.transform.localPosition = new Vector3(0.085f, 0f, 0.2f);
            klapa.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            kolajder.enabled = false;
            
        }

        if (rozwiazanaZagadka2 && distanceToPlayer.magnitude <= pickUpRange)
        {
            klapa.transform.localPosition = new Vector3(0.085f, 0f, 0.2f);
            klapa.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            kolajder.enabled=false;
            pen.SetActive(true);
        }
        
    }
}
