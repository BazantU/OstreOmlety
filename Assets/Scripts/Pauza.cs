using System.Collections;
using System.Collections.Generic;
using Q3Movement;
using UnityEngine;

public class Pauza : MonoBehaviour
{
    public Camera kameraGracza;
    public Camera kameraMenu;
    public GameObject blur;
    public Q3PlayerController playerScript;
    public Przyciski_Menu przyciskiMenu;

    [Header("Menu")]
    public GameObject ustawieniaMenu;
    public GameObject przyciskWylaczPauze;
    public GameObject przyciskWyjdz_zGry;

    private bool menuWlaczone = false;

    public void pokazMenu()
    {
        menuWlaczone = true;
        playerScript.ChangeCurrentLockState(false);
        Time.timeScale = 0f;
        ustawieniaMenu.SetActive(true);
        przyciskWylaczPauze.SetActive(true);
        blur.SetActive(true);
        przyciskWyjdz_zGry.SetActive(true);
    }

    public void ukryjMenu()
    {
        menuWlaczone = false;
        Time.timeScale = 1f;
        ustawieniaMenu.SetActive(false);
        przyciskWylaczPauze.SetActive(false);
        blur.SetActive(false);
        przyciskiMenu.zaladujUstawienia();
        przyciskWyjdz_zGry.SetActive(false);
        playerScript.ChangeCurrentLockState(true);
    }

    void Update()
    {
        if(kameraGracza.enabled && !kameraMenu.enabled)
        {   
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(!menuWlaczone){pokazMenu();}
            }
        }
    }
}
