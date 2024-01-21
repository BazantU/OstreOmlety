using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRouter : MonoBehaviour
{
    public static bool trzymany;
    public static bool trzymany2;
    public Rigidbody body;
    public MeshCollider bcollider;
    public Transform player, hand, cam;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equiped;
    public static bool slotFull;


    private Vector3 size;
    Vector3 distanceToPlayer;

    public Transform laptopik;
    public static bool udaloSie;
    public GameObject kabel;


    // Start is called before the first frame update
    void Start()
    {
        kabel.SetActive(false);
        trzymany2 = false;
        udaloSie = false;
        if (!equiped)
        {
            body.isKinematic = false;
            bcollider.isTrigger = false;
            trzymany = false;
        }
        if (equiped)
        {
            body.isKinematic = true;
            bcollider.isTrigger = true;
            trzymany = true;
        }

    }

    private void OnMouseOver()
    {

        if (!equiped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !PickupKartka.slotFull && !Pickup.slotFull) PickUp();
    }

    void Update()
    {
        distanceToPlayer = player.position - transform.position;

        if (trzymany2)
        {
            transform.SetParent(laptopik);
            transform.localPosition = new Vector3(-0.368499994f, 0.0270000007f, -1f);
            transform.localRotation = Quaternion.Euler(270, 90, 0);
            transform.localScale = new Vector3(7.63311052f, 7.63311052f, 7.63311052f);
            equiped = false;
            trzymany = false;
            udaloSie = true;
            PickupKartka.slotFull = false;
            Pickup.slotFull = false;
            kabel.SetActive(true);
        }

        if (equiped && Input.GetKeyDown(KeyCode.Q)) Drop();

        if (equiped && !trzymany2)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

        }
        else trzymany = false;
    }
    private void PickUp()
    {
        size = transform.localScale;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = size;
        equiped = true;
        PickupKartka.slotFull = true;
        body.isKinematic = true;
        bcollider.isTrigger = true;
        trzymany = true;
    }
    private void Drop()
    {
        equiped = false;
        PickupKartka.slotFull = false;
        transform.SetParent(null);
        body.isKinematic = false;
        bcollider.isTrigger = false;
        body.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        body.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        body.AddTorque(new Vector3(random, random, random));
        transform.localScale = size;
    }
}
