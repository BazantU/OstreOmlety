using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtwuszKlape : MonoBehaviour
{
    public float pickUpRange;
    public Transform player;
    private bool otwarta;
    
    public KlodkaDoSkrzyniaka klodkaDoSkrzyniaka;
    
    

    // Start is called before the first frame update
    void Start()
    {
        otwarta = false;
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 distanceToPlayer = player.position - transform.position;
        if (klodkaDoSkrzyniaka.rozwiazanaZagadka && distanceToPlayer.magnitude <= pickUpRange)
        {
            transform.localPosition = new Vector3(0.085f, 0f, 0.2f);
            transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            otwarta = true;
        }
        
        if (klodkaDoSkrzyniaka.rozwiazanaZagadka2 && distanceToPlayer.magnitude <= pickUpRange)
        {
            transform.localPosition = new Vector3(0.085f, 0f, 0.2f);
            transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            otwarta = true;
        }
    }
}
