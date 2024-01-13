using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Rigidbody body;
    public BoxCollider bcollider;
    public Transform player, hand, cam;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equiped;
    public static bool slotFull;
    

    private Vector3 size;
    Vector3 distanceToPlayer;


    // Start is called before the first frame update
    void Start()
    {
        if (!equiped)
        {
            body.isKinematic = false;
            bcollider.isTrigger = false;
        }
        if(equiped)
        {
            body.isKinematic = true;
            bcollider.isTrigger = true;
        }
    }

    private void OnMouseOver()
    {

        if (!equiped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !PickupKartka.slotFull) PickUp();
    }

    void Update()
    {
        distanceToPlayer = player.position - transform.position;
        

        if (equiped && Input.GetKeyDown(KeyCode.Q)) Drop();

        if (equiped)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
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

    }
    private void Drop()
    {
        equiped = false;
        PickupKartka.slotFull = false;
        transform.SetParent(null);
        body.isKinematic = false;
        bcollider.isTrigger = false;
        body.velocity = player.GetComponent<Rigidbody>().velocity;
        body.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        body.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        float random = Random.Range(-1f, 1f);
        body.AddTorque(new Vector3(random, random, random));
        transform.localScale = size;
    }
}
