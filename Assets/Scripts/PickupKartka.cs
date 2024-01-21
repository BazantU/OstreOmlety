using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PickupKartka : MonoBehaviour
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

    Vector3 siu = new Vector3(-13.7505999f, 4.22580004f, 9.02980042f);



    // Start is called before the first frame update
    void Start()
    {
        if (!equiped)
        {
            if (siu == transform.position) body.isKinematic = true;
            else body.isKinematic = false;
            bcollider.isTrigger = false;
            body.useGravity = false;

        }
        if (equiped)
        {
            body.isKinematic = true;
            bcollider.isTrigger = true;
            body.useGravity = true;
        }
    }
    

    private void OnMouseOver()
    {
        if (!equiped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = player.position - transform.position;
        

        if (equiped)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        if (equiped && Input.GetKeyDown(KeyCode.Q)) Drop();

    }
    private void PickUp()
    {
        body.useGravity = false;
        size = transform.localScale;
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = size;
        equiped = true;
        slotFull = true;
        body.isKinematic = true;
        bcollider.isTrigger = true;

    }
    private void Drop()
    {
        body.useGravity = true;
        equiped = false;
        slotFull = false;
        transform.SetParent(null);
        body.isKinematic = false;
        bcollider.isTrigger = false;
        body.AddForce(cam.forward * dropForwardForce, ForceMode.Impulse);
        body.AddForce(cam.up * dropUpwardForce, ForceMode.Impulse);
        transform.localScale = size;
    }
}
