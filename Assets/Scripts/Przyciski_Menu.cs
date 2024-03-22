using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Przyciski_Menu : MonoBehaviour
{   
    public GameObject menuGlowne;
    public Camera kameraMenu;
    public Camera kameraGracza;
    public Q3PlayerController playerScript;
    public Bobbing_v2 bobbingScript;
    public Light mainLight;
    public GameObject startBlur;
    public Transform camHandler;

    [Header("Menu Ustawienia")]
    public GameObject menuUstawienia;
    public GameObject przyciskCofnij;

    [Header("Ustawienia")]
    public Slider renderDistance;
    public Toggle cienie;
    public Slider czuloscMyszy;
    public Slider fov;
    public Toggle headBobbing;

    [HideInInspector] public float czuloscProcent = 1f;

    void cienieUstawienia(bool state)
    {   
        if(state == true)
        {
            mainLight.shadows = LightShadows.Soft;
        }
        else
        {
            mainLight.shadows = LightShadows.None;
        }
    }

    void zmienFOV(Camera cam, float wartoscUstawienia)
    {
        float hFOVrad = wartoscUstawienia * Mathf.Deg2Rad;
        float camH = Mathf.Tan(hFOVrad * 0.5f) / cam.aspect;
        float vFOVrad = Mathf.Atan(camH) * 2;
        cam.fieldOfView = vFOVrad * Mathf.Rad2Deg;
    }

    public void zaladujUstawienia()
    {
        kameraGracza.farClipPlane = renderDistance.value * 10;
        cienieUstawienia(cienie.isOn);
        czuloscProcent = czuloscMyszy.value;
        zmienFOV(kameraGracza, fov.value);
        bobbingScript.wlaczone = headBobbing.isOn;
        camHandler.localRotation = Quaternion.Euler(Vector3.zero);
    }

    void Start()
    {   
        menuUstawienia.SetActive(false);
        przyciskCofnij.SetActive(false);
    }

    public void graj()
    {   
        kameraGracza.enabled = true;
        kameraMenu.enabled = false;
        menuGlowne.SetActive(false);
        playerScript.ChangeCurrentLockState(true);
        Time.timeScale = 1f;
        startBlur.SetActive(false);
        zaladujUstawienia();
    }

    public void ustawienia()
    {   
        przyciskCofnij.SetActive(true);
        menuUstawienia.SetActive(true);
        menuGlowne.SetActive(false);
    }

    public void cofnij_zUstawien()
    {
        menuGlowne.SetActive(true);
        menuUstawienia.SetActive(false);
        przyciskCofnij.SetActive(false);
    }

    public void wylacz()
    {
        Application.Quit();
    }
}
