using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DzwiekHandler : MonoBehaviour
{
    [Range(0f, 1f)]
    public float globalnaGlosnosc;

    public Dzwiek[] dzwieki;

    public void graj(string nazwaDzwieku, bool stakujDzwiek = false){
        Dzwiek dzwiek = Array.Find(dzwieki, dzwiek => dzwiek.nazwa == nazwaDzwieku);

        if(dzwiek != null){
            dzwiek.zrodloDzwieku.volume = dzwiek.glosnosc * globalnaGlosnosc;
            
            if(stakujDzwiek){
                dzwiek.zrodloDzwieku.Play();
            }else{
                if(!dzwiek.zrodloDzwieku.isPlaying){
                    dzwiek.zrodloDzwieku.Play();
                }
            }
        }
    }
}
