using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class conversationStart : MonoBehaviour
{

    private bool trigo;
    public GameObject img_E;
    public Conversation_NodeGraph Conversation;
    public GameObject self;
   


    private bool segundaParteIniciada = false;
    private bool conversationStarted = false;
    private bool trigger = false; // isso e da cutscene
    private bool cutsceneAcabou;



    [Header("Settings")]
    public bool TemCutScene;
    public PlayableDirector timeline;
    public float tempocut;
    public bool DesativaBoxCollider;
    public GameObject Desativa;
    public bool AtivaBoxCollider;
    public GameObject Ativa;
    public GameObject Desativa2;



    // Update is called once per frame
    void Update()
    {
        if (trigo == true)
        {
            img_E.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("ConversationHandler").GetComponent<ConversationHandler>().IniciarConversa(Conversation);
                trigo = false;

                img_E.gameObject.SetActive(false);
                self.gameObject.GetComponent<BoxCollider>().enabled = false;

                conversationStarted = true;
                if (AtivaBoxCollider == true)
                    Ativa.GetComponent<BoxCollider>().enabled = true;
                if (DesativaBoxCollider == true)
                {
                    Desativa.GetComponent<BoxCollider>().enabled = false;

                }

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
                if(Desativa2 != null)
                Desativa2.SetActive(false);
                

                //roda a cutscene 
                if (TemCutScene == true)
                    timeline.Play();

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
