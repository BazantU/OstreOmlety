using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class menu : MonoBehaviour
{
    public Camera kameraGracza;
    public Camera kameraMenu;

    public Player skryptPlayer;

    public Canvas canvasMenu;
    public Volume menuVFX;
    public float predkoscObrotu;

    void Start()
    {
        skryptPlayer.canMove = false;

        kameraMenu.enabled = true;
        kameraGracza.enabled = false;

        canvasMenu.enabled = true;
        menuVFX.enabled = true;
    }

    void Update()
    {
        if(kameraMenu.enabled){
            kameraMenu.transform.Rotate(0, predkoscObrotu * Time.deltaTime, 0);
        }
    }

    public void graj(){
        skryptPlayer.canMove = true;

        kameraMenu.enabled = false;
        kameraGracza.enabled = true;

        canvasMenu.enabled = false;
        menuVFX.enabled = false;
    }

    public void wyjdz(){
        Application.Quit();
    }
}
