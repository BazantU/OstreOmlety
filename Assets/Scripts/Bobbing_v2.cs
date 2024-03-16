using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Bobbing_v2 : MonoBehaviour
{
    public Transform camHandler;
    CharacterController controller;

    [Header("Rotacja")]
    public float wielkosc = 1f;
    public float czestotliwosc = 1f;
    public AnimationCurve przyspieszenie;

    [Header("Pozycja")]
    public float _wielkosc = 1f;
    public float _czestotliwosc = 1f;
    public AnimationCurve _przyspieszenie;
    Vector3 startPos;

    void obrocKamere()
    {   
        if(controller.isGrounded && !Input.GetButton("Jump") && controller.velocity.magnitude > 0f)
        {
            float nowe_z = Mathf.Sin(Time.time * czestotliwosc) * wielkosc * przyspieszenie.Evaluate(controller.velocity.magnitude);
            camHandler.localRotation = Quaternion.Euler(camHandler.localRotation.x, camHandler.localRotation.y, camHandler.localRotation.z + nowe_z);
        }
        else
        {   
            camHandler.localRotation = Quaternion.Lerp(camHandler.localRotation, Quaternion.Euler(camHandler.localRotation.x, camHandler.localRotation.y, 0f), Time.deltaTime);
        }
    }

    void zmienPozycjeKamery()
    {
        if(controller.isGrounded && !Input.GetButton("Jump") && controller.velocity.magnitude > 0f)
        {
            float nowe_y = Mathf.Sin(Time.time * _czestotliwosc*1.5f) * _wielkosc*1.5f * _przyspieszenie.Evaluate(controller.velocity.magnitude);
            camHandler.localPosition = new Vector3(startPos.x + nowe_y, startPos.y + nowe_y, startPos.z);
        }
        else
        {   
            camHandler.localPosition = Vector3.Lerp(camHandler.localPosition, new Vector3(startPos.x, startPos.y, startPos.z), Time.deltaTime);
            if(Mathf.Abs(startPos.x) < 0.00001f) camHandler.localPosition = new Vector3(0f, startPos.y, startPos.z);
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = camHandler.localPosition;
    }

    void Update()
    {   
        obrocKamere();
        zmienPozycjeKamery();
    }
}
