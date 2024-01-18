using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using System.Linq;

public class Drzwi : MonoBehaviour
{   
    public Transform gracz;
    public Camera cam;

    public Vector3 podstawowy_obrot_o;
    private List<Transform> otwarte_drzwi = new List<Transform>();

    void Start()
    {   
        
    }

    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 1f) && hit.transform.tag == "Drzwi" && !otwarte_drzwi.Contains(hit.transform))
            {   
                Transform drzwi = hit.transform;
                otwarte_drzwi.Add(drzwi);

                DOTween.Init();
                drzwi.DOLocalRotate(drzwi.localRotation.eulerAngles + podstawowy_obrot_o, 1.5f);
            }
        }
    }
}
