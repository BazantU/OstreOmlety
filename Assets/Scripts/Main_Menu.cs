using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using UnityEngine;
using UnityEngine.UIElements;

public class Main_Menu : MonoBehaviour
{
    public bool wlaczone = true;

    [Header("Gracz")]
    public Camera kamera;

    [Header("Menu")]
    public GameObject menuCanvas;
    public Camera kameraMenu;
    public float predkoscObrotu = 1f;
    public Q3PlayerController playerScript;
    public Animator napisAnimator;
    public GameObject startBlur;

    void Awake()
    {   
        kameraMenu.transform.rotation = Quaternion.Euler(Vector3.zero);
        if(wlaczone)
        {   
            kameraMenu.enabled = true;
            kamera.enabled = false;
            menuCanvas.SetActive(true);
            playerScript.ChangeCurrentLockState(false);
            startBlur.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {   
            startBlur.SetActive(false);
            napisAnimator.StopPlayback();
        }
    }

    void Update()
    {
        if(wlaczone)
        {
            kameraMenu.transform.Rotate(0f, predkoscObrotu * Time.unscaledDeltaTime, 0f);
        }
    }
}
