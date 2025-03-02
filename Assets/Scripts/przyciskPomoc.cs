using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class przyciskPomoc : MonoBehaviour
{
    public GameObject laptop;    

    public void przyciskanie()
    {
        laptop.GetComponent<LaptopikWos>().pokazLaptoka2 = true;
    }
}
