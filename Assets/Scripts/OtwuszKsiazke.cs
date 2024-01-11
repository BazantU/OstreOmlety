using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtwuszKsiazke : MonoBehaviour
{
    Vector3 pozycja;
    Quaternion rotacja;
    private bool otwarta = false;

    // Start is called before the first frame update
    void Start()
    {
        pozycja = transform.localPosition;
        rotacja = transform.localRotation;
    }

    // Update is called once per frame
    private void OnMouseUpAsButton()
    {
        if (!otwarta)
        {
            transform.localPosition = new Vector3(1.541f, 0, 0);
            transform.localRotation = Quaternion.Euler(-269.98f, 180, 0);
            otwarta=true;
        }
        else if (otwarta)
        {
            transform.localPosition = pozycja;
            transform.localRotation = rotacja;
            otwarta = false;
        }
    }
}
