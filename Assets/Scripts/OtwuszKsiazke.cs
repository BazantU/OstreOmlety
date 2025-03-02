using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtwuszKsiazke : MonoBehaviour
{
    Vector3 pozycja;
    Quaternion rotacja;
    private bool otwarta = false;

    public DzwiekHandler dzwiekHandler;

    // Start is called before the first frame update
    void Start()
    {
        pozycja = transform.localPosition;
        rotacja = transform.localRotation;
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (!otwarta && Input.GetKeyDown(KeyCode.E))
        {
            transform.localPosition = new Vector3(1.541f, 0, 0);
            transform.localRotation = Quaternion.Euler(-269.98f, 180, 0);
            otwarta=true;
            dzwiekHandler.graj("book", true);
        }
        else if (otwarta && Input.GetKeyDown(KeyCode.E))
        {
            transform.localPosition = pozycja;
            transform.localRotation = rotacja;
            otwarta = false;
            dzwiekHandler.graj("book", true);
        }

        transform.parent.gameObject.layer = LayerMask.NameToLayer("Outline");
        foreach(Transform child in transform.parent){
            child.gameObject.layer = LayerMask.NameToLayer("Outline");
        }
    }

    private void OnMouseExit(){
        transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
        foreach(Transform child in transform.parent){
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
