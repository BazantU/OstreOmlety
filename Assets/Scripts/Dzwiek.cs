using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Dzwiek
{
    public string nazwa;

    public AudioSource zrodloDzwieku;

    [Range(0f, 1f)]
    public float glosnosc;

    void Awake(){
        glosnosc = zrodloDzwieku.volume;
    }
}
