using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SemE_ConversationStart : MonoBehaviour
{

    private bool trigo;
    public GameObject img_E;
    public Conversation_NodeGraph Conversation;
    public GameObject Self;
    public Conversation_NodeGraph ultimafalashad;

    public bool cutscenefinal;
    private bool segundaParteIniciada = false;
    private bool conversationStarted = false;
    private bool cutsceneAcabou = false;
    
    private bool trigger = false; // isso e da cutscene

    [Header("Settings")]
    public bool TemCutScene;
    public PlayableDirector timeline;
    public float tempocut;

    public bool DesativaBoxCollider;
    public GameObject Desativa;
    
    public bool AtivaBoxCollider;
    public GameObject Ativa;


    // Update is called once per frame
    void Update()
    {
        if (trigo == true)
        {

            if (ultimafalashad != null && Conversation == ultimafalashad)
            {
                Parametros_globais.cutscene2final = true;
            }

            img_E.gameObject.SetActive(true);
            GameObject.Find("ConversationHandler").GetComponent<ConversationHandler>().IniciarConversa(Conversation);
            trigo = false;
            img_E.gameObject.SetActive(false);
            conversationStarted = true;
            Self.GetComponent<BoxCollider>().enabled = false;

            if (AtivaBoxCollider == true)
                Ativa.GetComponent<BoxCollider>().enabled = true;
            if (DesativaBoxCollider == true && Desativa != null)
            {
                Desativa.GetComponent<BoxCollider>().enabled = false;
                
            }
               



        }

        //Sua atual fala acaba
       else if (conversationStarted == true)
        {
            if (GameObject.Find("ConversationHandler").GetComponent<ConversationHandler>().Conversation == null && !segundaParteIniciada)
            {
                Debug.Log("rola");



                segundaParteIniciada = true;
                conversationStarted = false;
                //roda a cutscene 
                if (TemCutScene == true)
                {
                    timeline.Play();                    
                }
                trigger = true;

                
            }
        }

        if (TemCutScene == true)
        {
            if (trigger == true && timeline.time < tempocut)
            {
                Parametros_globais.speed = 0f;

            }
            else if (trigger == true && timeline.time > tempocut && cutsceneAcabou == false)
            {
                Parametros_globais.speed = 5f;
                Debug.Log("voltou a andar");
                cutsceneAcabou = true;
                timeline.Pause();
            }

        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            trigo = true; 
           

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            trigo = false;
            img_E.gameObject.SetActive(false);

        }
    }

}
