using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Pokretlo_do_sejfu : MonoBehaviour
{   
    public Camera cam;
    public Player playerController;
    private float max_odleglosc;

    [HideInInspector] public bool mozna_krecic;
    private bool zakrecono = false;

    [Header("Dane sejfu")]
    public Transform drzwiDoSejfu;
    public Vector3 koncowaRotacja;
    public float czasOtwierania;

    private void otworz_drzwi()
    {   
        DOTween.Init();
        drzwiDoSejfu.DOLocalRotate(koncowaRotacja, czasOtwierania);
    }

    private void zakrec_pokretlem()
    {
        DOTween.Init();
        transform.parent.DOLocalRotate(new Vector3(1440f, 0f, 0f), 4f, RotateMode.FastBeyond360).OnComplete(() => otworz_drzwi());
    }

    void Start()
    {
        max_odleglosc = playerController.maxOdleglosc;
        mozna_krecic = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, max_odleglosc)
                && mozna_krecic
                && !zakrecono
                && hit.transform == transform)
            {   
                zakrecono = true;
                transform.tag = "Untagged";
                zakrec_pokretlem();
            }
        }
    }
}
