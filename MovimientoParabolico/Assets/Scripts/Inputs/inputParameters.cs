using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class inputParameters : MonoBehaviour
{
    public TiroParabolico tiroParabolico;
    public InputField velInput;
    public InputField angInput;
    public InputField distEstimadaInput;
    public Button iniciarSimulacion;
    public Button reiniciar;
    public GameObject msjErrorNegativos;
    public GameObject msjErrorRangoAngulos;
    public GameObject msjDatosIncompletos;
    public Text textoRespuestaDistMax;
    public Text felicitaciones;
    public Text incorrecto;
    public static float vel;
    public static float ang;
    public static float estimaciondist;
    double radianes = 0;
    double radianesPor2 = 0;
    double pi = 3.1416;
    public static double seno = 0;
    public static double dosSeno = 0;
    public static double coseno = 0;

    void Awake()
    {
        velInput.characterLimit = 5;
        angInput.characterLimit = 2;
        distEstimadaInput.characterLimit = 10;
        iniciarSimulacion.interactable = false;
    }

    void Update()
    {
        float.TryParse(velInput.text, out float resultvel);
        vel = resultvel;

        float.TryParse(angInput.text, out float resultang);
        ang = resultang;

        float.TryParse(distEstimadaInput.text, out float resultdistEstimada);
        estimaciondist = resultdistEstimada;

        radianes = ang * (pi/180);
        radianesPor2 = (ang * 2) * (pi/180);

        seno = Mathf.Sin(((float)radianes));
        dosSeno = Mathf.Sin(((float)radianesPor2));
        coseno = Mathf.Cos(((float)radianes));

        if(velInput.text != "" && angInput.text != "" && distEstimadaInput.text != "")
        {
            iniciarSimulacion.interactable = true;
        }
        else
        {
            iniciarSimulacion.interactable = false;
        }

        ComprobarInputFields();
    }

    //Esta funcion comprueba los campos de velocidad y angulo iniciales, revisando siestan dentro de los rangos permitidos
    public void ComprobarInputFields()
    {
        if(vel < 0)
        {
            msjErrorNegativos.SetActive(true);
            velInput.text = "";
            StartCoroutine (MsjError());
        }

        if(ang < 0 || ang > 90)
        {
            msjErrorRangoAngulos.SetActive(true);
            angInput.text = "";
            StartCoroutine (MsjAngulos());
        }
    }

    //Esta funcion comprueba si todos los datos han sido diligenciados
    public void ComprobarDatosDiligenciados()
    {

        if(velInput.text == "" || angInput.text == "" || distEstimadaInput.text == "")
        {
            msjDatosIncompletos.SetActive(true);
            StartCoroutine (MsjDatosIncompletos());
        }

        else
        {
            this.gameObject.SetActive(false);
            textoRespuestaDistMax.text = "";
            felicitaciones.text = "";
            incorrecto.text = "";
        }
    }

    //Estas corrutinas nos ayudan a mejorar la usabilidad en los mensajes de error, dandoles un tiempo a los mensajes a ser leidos y borrar lo introducido para minimizar el error "typo" o de tecleo
    IEnumerator MsjError()
    {
        velInput.interactable = false;
        yield return new WaitForSeconds(1.4f);
        msjErrorNegativos.SetActive(false);
        velInput.interactable = true;
    }

    IEnumerator MsjAngulos()
    {
        angInput.interactable = false;
        yield return new WaitForSeconds(1.4f);
        msjErrorRangoAngulos.SetActive(false);
        angInput.interactable = true;
    }

    IEnumerator MsjDatosIncompletos()
    {
        iniciarSimulacion.interactable = false;
        yield return new WaitForSeconds(1.4f);
        msjDatosIncompletos.SetActive(false);
        iniciarSimulacion.interactable = true;
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
