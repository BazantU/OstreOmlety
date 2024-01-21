using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrzyciskDoKlodki : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TextMesh text;
    public bool up;

    // Update is called once per frame
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (up)
            {
                if (text.text == "A")
                {
                    text.text = "B";
                }
                else if (text.text == "B")
                {
                    text.text = "C";
                }
                else if (text.text == "C")
                {
                    text.text = "D";
                }
                else if (text.text == "D")
                {
                    text.text = "E";
                }
                else if (text.text == "E")
                {
                    text.text = "F";
                }
                else if (text.text == "F")
                {
                    text.text = "G";
                }
                else if (text.text == "G")
                {
                    text.text = "H";
                }
                else if (text.text == "H")
                {
                    text.text = "I";
                }
                else if (text.text == "I")
                {
                    text.text = "J";
                }
                else if (text.text == "J")
                {
                    text.text = "K";
                }
                else if (text.text == "K")
                {
                    text.text = "L";
                }
                else if (text.text == "L")
                {
                    text.text = "M";
                }
                else if (text.text == "M")
                {
                    text.text = "N";
                }
                else if (text.text == "N")
                {
                    text.text = "O";
                }
                else if (text.text == "O")
                {
                    text.text = "P";
                }
                else if (text.text == "P")
                {
                    text.text = "R";
                }
                else if (text.text == "R")
                {
                    text.text = "S";
                }
                else if (text.text == "S")
                {
                    text.text = "T";
                }
                else if (text.text == "T")
                {
                    text.text = "U";
                }
                else if (text.text == "U")
                {
                    text.text = "W";
                }
                else if (text.text == "W")
                {
                    text.text = "Y";
                }
                else if (text.text == "Y")
                {
                    text.text = "Z";
                }
                else if (text.text == "Z")
                {
                    text.text = "A";
                }

                if (text.text == "1")
                {
                    text.text = "2";
                }
                else if (text.text == "2")
                {
                    text.text = "3";
                }
                else if (text.text == "3")
                {
                    text.text = "4";
                }
                else if (text.text == "4")
                {
                    text.text = "5";
                }
                else if (text.text == "5")
                {
                    text.text = "6";
                }
                else if (text.text == "6")
                {
                    text.text = "7";
                }
                else if (text.text == "7")
                {
                    text.text = "8";
                }
                else if (text.text == "8")
                {
                    text.text = "9";
                }
                else if (text.text == "9")
                {
                    text.text = "0";
                }
                else if (text.text == "0")
                {
                    text.text = "1";
                }
            }
            if (!up)
            {
                if (text.text == "Z")
                {
                    text.text = "Y";
                }
                else if (text.text == "Y")
                {
                    text.text = "W";
                }
                else if (text.text == "W")
                {
                    text.text = "U";
                }
                else if (text.text == "U")
                {
                    text.text = "T";
                }
                else if (text.text == "T")
                {
                    text.text = "S";
                }
                else if (text.text == "S")
                {
                    text.text = "R";
                }
                else if (text.text == "R")
                {
                    text.text = "P";
                }
                else if (text.text == "P")
                {
                    text.text = "O";
                }
                else if (text.text == "O")
                {
                    text.text = "N";
                }
                else if (text.text == "N")
                {
                    text.text = "M";
                }
                else if (text.text == "M")
                {
                    text.text = "L";
                }
                else if (text.text == "L")
                {
                    text.text = "K";
                }
                else if (text.text == "K")
                {
                    text.text = "J";
                }
                else if (text.text == "J")
                {
                    text.text = "I";
                }
                else if (text.text == "I")
                {
                    text.text = "H";
                }
                else if (text.text == "H")
                {
                    text.text = "G";
                }
                else if (text.text == "G")
                {
                    text.text = "F";
                }
                else if (text.text == "F")
                {
                    text.text = "E";
                }
                else if (text.text == "E")
                {
                    text.text = "D";
                }
                else if (text.text == "D")
                {
                    text.text = "C";
                }
                else if (text.text == "C")
                {
                    text.text = "B";
                }
                else if (text.text == "B")
                {
                    text.text = "A";
                }
                else if (text.text == "A")
                {
                    text.text = "Z";
                }

                if (text.text == "0")
                {
                    text.text = "9";
                }
                else if (text.text == "9")
                {
                    text.text = "8";
                }
                else if (text.text == "8")
                {
                    text.text = "7";
                }
                else if (text.text == "7")
                {
                    text.text = "6";
                }
                else if (text.text == "6")
                {
                    text.text = "5";
                }
                else if (text.text == "5")
                {
                    text.text = "4";
                }
                else if (text.text == "4")
                {
                    text.text = "3";
                }
                else if (text.text == "3")
                {
                    text.text = "2";
                }
                else if (text.text == "2")
                {
                    text.text = "1";
                }
                else if (text.text == "1")
                {
                    text.text = "0";
                }
            }
        }
    }
}
