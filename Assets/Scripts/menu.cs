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
    public Canvas canvasPauzaESC;
    public Canvas canvasMenuPauza;
    public Volume menuVFX;
    public float predkoscObrotu;

    void Start()
    {
        skryptPlayer.canMove = false;

        kameraMenu.enabled = true;
        kameraGracza.enabled = false;

        canvasMenu.enabled = true;
        canvasPauzaESC.enabled = false;
        canvasMenuPauza.enabled = false;
        menuVFX.enabled = true;
    }

    void Update()
    {
        if(kameraMenu.enabled){
            kameraMenu.transform.Rotate(0, predkoscObrotu * Time.deltaTime, 0);
        }

        if(!canvasMenu.enabled){
            if(!skryptPlayer.enabled || !skryptPlayer.canMove){
                canvasPauzaESC.enabled = false;
            }else{
                canvasPauzaESC.enabled = true;
            }
        }

        if(canvasPauzaESC.enabled && Input.GetKeyDown(KeyCode.Escape)){
            canvasMenuPauza.enabled = true;
            canvasPauzaESC.enabled = false;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            skryptPlayer.canMove = false;
        }
    }

    public void unpause(){
        canvasMenuPauza.enabled = false;
        canvasPauzaESC.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        skryptPlayer.canMove = true;
    }

    public void graj(){
        skryptPlayer.canMove = true;

        kameraMenu.enabled = false;
        kameraGracza.enabled = true;

        canvasMenu.enabled = false;
        canvasPauzaESC.enabled = true;
        menuVFX.enabled = false;
    }

    public void wyjdz(){
        Application.Quit();
    }
}
