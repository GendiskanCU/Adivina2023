using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CanvasController : MonoBehaviour
{    
    public Text mensajesTxt;
    public InputField usuarioTxt;
    public InputField numeroTxt;
    public Button botonOK;

    private int elementoActivo;
    private int elementoAnterior;

    private ReadSpeakerController readSpeaker;
    private string textoADecir;

    public UnityEvent okPulsado;


    private void Awake() {
        readSpeaker = FindObjectOfType<ReadSpeakerController>();
    }

   

    // Update is called once per frame
    void Update()
    {
        elementoAnterior = elementoActivo;

        if(Input.GetKeyDown(KeyCode.DownArrow) && elementoActivo != -1)
        {            
            elementoActivo = Mathf.Clamp(elementoActivo + 1, 0, 1);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && elementoActivo != -1)
        {
            elementoActivo = Mathf.Clamp(elementoActivo - 1, 0, 1);
        }

        if(elementoActivo != elementoAnterior)
        {
            ChangeActiveElement();
        }

        if(Input.GetKeyDown(KeyCode.Return) && elementoActivo == 1)
        {
            elementoActivo = -1;
            //botonOK.interactable = false;
            okPulsado.Invoke();
        }        
       
       if(Input.GetKeyDown(KeyCode.Escape))
       {
            Debug.Log("Saliendo del juego");
            Application.Quit();
       }
    }  


    private void ChangeActiveElement()
    {
        switch (elementoActivo)
        {
            case 0:
                //botonOK.interactable = false;
                if(usuarioTxt.gameObject.activeSelf)
                {
                    textoADecir = (usuarioTxt.text != "" && usuarioTxt.text != null) ?
                        string.Format("Campo de texto. {0}", usuarioTxt.text) :
                         "Campo de texto. Escribe aquí tu nombre";                    
                    usuarioTxt.ActivateInputField();
                }
                if(numeroTxt.gameObject.activeSelf)
                {
                    textoADecir = (numeroTxt.text != "" && numeroTxt.text != null) ?
                        string.Format("Campo de texto. {0}", numeroTxt.text)
                         : "Campo de texto. Escribe aquí el número";
                    numeroTxt.ActivateInputField();
                }
                break;
            case 1:
                //botonOK.interactable = true;
                if(usuarioTxt.gameObject.activeSelf)
                    usuarioTxt.DeactivateInputField();
                if(numeroTxt.gameObject.activeSelf)
                    numeroTxt.DeactivateInputField();

                textoADecir = "Botón OK. Pulsa ahora Intro para confirmar";                
                break;                  
        }

        readSpeaker.SpeakText(textoADecir);
    }

    public void UpdateText(string nuevoTexto)
    {
        mensajesTxt.text = nuevoTexto;
        readSpeaker.SpeakText(nuevoTexto);
    }

    public void ChangeActiveInputField()
    {
        usuarioTxt.gameObject.SetActive(!usuarioTxt.gameObject.activeSelf);
        numeroTxt.gameObject.SetActive(!usuarioTxt.gameObject.activeSelf);        
    }

    public void NewInput()
    {
        elementoActivo = 0;
        //botonOK.interactable = false;

        if(usuarioTxt.gameObject.activeSelf)
        {
            usuarioTxt.text = "";
            usuarioTxt.ActivateInputField();
        }
            
        
        if(numeroTxt.gameObject.activeSelf)
        {
            numeroTxt.text = "";
            numeroTxt.ActivateInputField();
        }        
    }

    public string GetInput()
    {
        string textoEscrito = "";

        if(usuarioTxt.gameObject.activeSelf)
            textoEscrito = (usuarioTxt.text != null && usuarioTxt.text != "") ?
            usuarioTxt.text : "";
        
        if(numeroTxt.gameObject.activeSelf)
            textoEscrito = (numeroTxt.text != null && numeroTxt.text != "") ?
             numeroTxt.text : "";

        
        return textoEscrito;
    }
}
