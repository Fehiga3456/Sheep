using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConversationOptionSelector : MonoBehaviour
{
    public Button Opcao1;
    public Button Opcao2;
    public Button Opcao3;
    public Button Opcao4;

    // Start is called before the first frame update
    void Start()
    {
        //Opcao1 = GameObject.Find("ConversaOpcao1").GetComponent<Button>();
        //Opcao2 = GameObject.Find("ConversaOpcao2").GetComponent<Button>();
        //Opcao3 = GameObject.Find("ConversaOpcao3").GetComponent<Button>();
        //Opcao4 = GameObject.Find("ConversaOpcao4").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.A))
        {
            if (Opcao1.gameObject.activeSelf)
            {
                if (EventSystem.current.currentSelectedGameObject == Opcao1.gameObject)
                {
                    Opcao1.onClick.Invoke();
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                    Opcao1.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.W))
        {
            if (Opcao2.gameObject.activeSelf)
            {
                if (EventSystem.current.currentSelectedGameObject == Opcao2.gameObject)
                {
                    Opcao2.onClick.Invoke();
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                    Opcao2.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) ||Input.GetKeyDown(KeyCode.D))
        {
            if (Opcao3.gameObject.activeSelf)
            {
                if (EventSystem.current.currentSelectedGameObject == Opcao3.gameObject)
                {
                    Opcao3.onClick.Invoke();
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                    Opcao3.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) ||Input.GetKeyDown(KeyCode.S))
        {
            if (Opcao4.gameObject.activeSelf)
            {
                if (EventSystem.current.currentSelectedGameObject == Opcao4.gameObject)
                {
                    Opcao4.onClick.Invoke();
                    EventSystem.current.SetSelectedGameObject(null);
                }
                else
                    Opcao4.Select();
            }
        }

    }
}
