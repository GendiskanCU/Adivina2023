using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{         
    private CanvasController canvasController;

    private bool juegoComenzado = false;

    private int numeroSecreto;
    private int numeroIntentos;

    [SerializeField] private int minimoIntentos = 5;
    [SerializeField] private int maximoIntentos = 10;
    [SerializeField] private int numeroSecretoMenor = 0;
    [SerializeField] private int numeroSecretoMayor = 100;

    private string nombreUsuario;
    private int numeroIntroducido;
    private string mensaje;
    private float tiempoPausa = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        canvasController = FindObjectOfType<CanvasController>();
        canvasController.okPulsado.AddListener(CheckText);
        
        ResetGame();
    }

    public void CheckText()
    {
        string contenidoInput = canvasController.GetInput();

        if(contenidoInput != "")
        {
            if(!juegoComenzado)
            {
                nombreUsuario = contenidoInput;
                mensaje = string.Format( "¡Encantada de conocerte, {0}!", nombreUsuario);
                canvasController.UpdateText(mensaje);
                Invoke("StartGame", tiempoPausa);                 
            }
            else
            {
                numeroIntroducido = int.Parse(contenidoInput);
                CheckNumber();
            }
                
        }
        else
        {
            canvasController.NewInput();
            mensaje = "Vaya, no has escrito nada. Vuelve a intentarlo";
            canvasController.UpdateText(mensaje);            
        }       
    }


    private void ResetGame()
    {
        nombreUsuario = "";
        
        mensaje = string.Format("Hola. ¿Cómo te llamas?. Escribe tu nombre, y pulsa el botón Ok. "+
        "Puedes moverte con flecha arriba y abajo, y confirmar con la tecla Intro");

        canvasController.ChangeActiveInputField();
        canvasController.NewInput();
        canvasController.UpdateText(mensaje);             
    }

    private void StartGame()    {
       
        numeroSecreto = Random.Range(numeroSecretoMenor, numeroSecretoMayor + 1);
        numeroIntentos = Random.Range(minimoIntentos, maximoIntentos + 1);

        mensaje = string.Format("{0}, ¿Serás capaz de adivinar el número entre {2} y {3} en el que estoy pensando?. " +
        "Escribe a continuación tu respuesta y pulsa el botón Ok. Tienes {1} intentos", nombreUsuario, numeroIntentos,
        numeroSecretoMenor, numeroSecretoMayor);        

        juegoComenzado = true;

        canvasController.ChangeActiveInputField();
        canvasController.NewInput();
        canvasController.UpdateText(mensaje);                
    }

    private void CheckNumber()
    {
        if(numeroIntroducido == numeroSecreto)
        {
            mensaje = string.Format("¡Muy bien, {0}, has acertado!. El número secreto era el {1}", nombreUsuario, numeroSecreto); 
            juegoComenzado = false;           
        }
        else
        {
            numeroIntentos--;
            if(numeroIntentos <= 0)            {
                
                mensaje = string.Format("¡Oh, fallaste {0}, y era ya tu último intento. Lo siento, has perdido!", nombreUsuario);
                juegoComenzado = false;
            }
            else
            {
                mensaje = numeroIntroducido < numeroSecreto ?
                 string.Format("{0} no es la respuesta correcta. El número secreto es más grande", numeroIntroducido) :
                 string.Format("{0} no es la respuesta correcta. El número secreto es más pequeño", numeroIntroducido);

                canvasController.NewInput();

                mensaje = string.Format("{0}. Prueba a escribir otro número. Te quedan {1} intentos",
                mensaje, numeroIntentos);                 
            }
        }

        canvasController.UpdateText(mensaje);

        if(!juegoComenzado)
            Invoke("ResetGame", 10.0f);
    }   
 
}
