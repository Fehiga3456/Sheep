using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationprimo : MonoBehaviour
{

    public bool trigo;
    public GameObject img_E;
    public Conversation_NodeGraph Conversation;
    public GameObject self;    
   // public GameObject outros;

    

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
                //outros.GetComponent<BoxCollider>().enabled = true;

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
