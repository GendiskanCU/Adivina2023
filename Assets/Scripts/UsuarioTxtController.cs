using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UsuarioTxtController : MonoBehaviour
{
    private string cadenaEscrita;
    private char ultimoCaracter;

    private ReadSpeakerController readSpeaker;
    

    // Start is called before the first frame update
    void Start()
    {
        readSpeaker = FindObjectOfType<ReadSpeakerController>();        
    }



    public void ReadLastCharacter(string texto)
    {

        if (texto != null && texto != "")
        {
            cadenaEscrita = texto;
            int longitud = cadenaEscrita.Length;
            ultimoCaracter = cadenaEscrita[longitud - 1];
            if (ultimoCaracter != ' ')
            {
                readSpeaker.SpeakText(ultimoCaracter.ToString());
            }
            else
            {
                readSpeaker.SpeakText("espacio");
            }

        }
    }    
}
