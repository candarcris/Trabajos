using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiroParabolico : MonoBehaviour
{    
    public inputParameters inputParameters;
    public GameObject circulo;
    public Transform objetivo;
    float alturaMax;
    public static float distMax;
    float velFinal;

    public float gravity = 9.8f;
    
    void Update()
    {
        
    }

    public void Lanzamiento()
    {
        alturaMax = ((Mathf.Pow(inputParameters.vel, 2)) * (Mathf.Pow(((float)inputParameters.seno), 2))) / (2 * 9.8f);
        distMax = ((Mathf.Pow(inputParameters.vel, 2)) * (((float)inputParameters.dosSeno))) / 9.8f;
        velFinal = (inputParameters.vel * ((float)inputParameters.coseno)) + (inputParameters.vel * ((float)inputParameters.seno));

        float tolerancia = (distMax * 5f) / 100;
        float toleranciaMayor = distMax + tolerancia;
        float toleranciaMenor = distMax - tolerancia;

        objetivo.transform.position = new Vector2(distMax, 0);
        Debug.Log(distMax);
        Debug.Log(alturaMax);

        inputParameters.textoRespuestaDistMax.text = "La distancia recorrida en x fue:" + distMax;


        if(inputParameters.estimaciondist == distMax || inputParameters.estimaciondist <= toleranciaMayor && inputParameters.estimaciondist >= toleranciaMenor)
        {
            inputParameters.felicitaciones.text = "Felicitaciones, tu respuesta es correcta";
        }

        else
        {
            inputParameters.incorrecto.text = "No has acertado, intentalo de nuevo";
        }
        

        //------------------------------------------------------------------------------------------------------------------------
        //Esta prueba se realizó para confirmar la veracidad de los datos
        //float prueba = (Mathf.Pow(inputParameters.vel, 2)) * (Mathf.Pow(((float)inputParameters.seno), 2));
        //float prueba2 = 2 * gravity;
        //Debug.Log(prueba / prueba2);

        //Debug.Log(inputParameters.coseno);
        //-------------------------------------------------------------------------------------------------------------------------

        
        Rigidbody2D circuloRigidBody = circulo.GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.up * -9.8f;
        circuloRigidBody.gravityScale = 1;
        circuloRigidBody.velocity = new Vector2((inputParameters.vel * ((float)inputParameters.coseno)), (inputParameters.vel * ((float)inputParameters.seno)));
    }

}
