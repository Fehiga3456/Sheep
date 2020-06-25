using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using System.IO;
using System;

public class ConversationHandler : MonoBehaviour
{
    [SerializeField]
    GameObject DialogCanvas;
    GameObject DialogCanvas2;
    public Conversation_NodeGraph Conversation;
    StartNode startNode;
    public Conversation_Node nodeAtual;
    bool WaitingAnswer = false;
    bool conversationStarted = false;
    Text TextoPrincipal;
    Text ConversaOpcao1;
    Text ConversaOpcao2;
    Text ConversaOpcao3;
    Text ConversaOpcao4;

    string Pensamento1;
    string Pensamento2;
    string Pensamento3;
    string Pensamento4;

    string preferred_language = "";

    Text balaoPensamento;


    // Start is called before the first frame update
    void Start()
    {
        
        DialogCanvas = GameObject.Find("DialogCanvas");
        DialogCanvas2 = GameObject.Find("DialogCanvas2");
        TextoPrincipal = GameObject.Find("TextoPrincipal").GetComponent<Text>();
        ConversaOpcao1 = GameObject.Find("ConversaOpcao1").GetComponent<Text>();
        ConversaOpcao2 = GameObject.Find("ConversaOpcao2").GetComponent<Text>();
        ConversaOpcao3 = GameObject.Find("ConversaOpcao3").GetComponent<Text>();
        ConversaOpcao4 = GameObject.Find("ConversaOpcao4").GetComponent<Text>();
        balaoPensamento = GameObject.Find("BalaoPensamento").GetComponent<Text>();
        LoadLanguage();

        if (Conversation != null)
        {
            IniciarConversa(Conversation);
            
        }
        else
        {
            DialogCanvas.SetActive(false);
            DialogCanvas2.SetActive(false);
        }
        print(Conversation);

    }

    // Update is called once per frame
    void Update()
    {

        if (Conversation != null)
        {
            if (!conversationStarted)
            {
                //para designar os valores apos start
                conversationStarted = true;
                //print(conversationStarted);
                nodeAtual = (Conversation_Node)startNode.GetOutputPort("Inicio").Connection.node;
            }
            if (WaitingAnswer == false)
            {
                
                //zerar os pensamentos
                Pensamento1 = "";
                Pensamento2 = "";
                Pensamento3 = "";
                Pensamento4 = "";

                //metodo para trazer pessoa na conversa da cutscene. deve ser refeito.
                if (nodeAtual.pessoaatrazer != null)
                {
                    Instantiate(nodeAtual.pessoaatrazer);
                }

                //colocar o alinhamento do texto a direta
                TextoPrincipal.alignment = TextAnchor.UpperRight;

                //atualizar o texto principal, que será do NPC Sempre
                TextoPrincipal.text = nodeAtual.NPCText;

                //se houver opcao 1...
                if (!string.IsNullOrEmpty(nodeAtual.Opcao1))
                {
                    //ativar o botao
                    ConversaOpcao1.gameObject.SetActive(true);
                    //buscar o pensamento
                    string[] pensamentoOp1 = RemoverPensamento(nodeAtual.Opcao1);
                    if (pensamentoOp1 != null)
                    {
                        Pensamento1 = pensamentoOp1[0];
                        ConversaOpcao1.text = pensamentoOp1[1];
                    }
                    else
                    {
                        //colocar o texto no botao
                        ConversaOpcao1.text = nodeAtual.Opcao1;
                    }
                }
                //se estiver sem opcao1...
                else
                {
                    //desabilitar o botao
                    ConversaOpcao1.gameObject.SetActive(false);
                }

                if (!string.IsNullOrEmpty(nodeAtual.Opcao2))
                {
                    ConversaOpcao2.gameObject.SetActive(true);
                    string[] pensamentoOp2 = RemoverPensamento(nodeAtual.Opcao2);
                    if (pensamentoOp2 != null)
                    {
                        Pensamento2 = pensamentoOp2[0];
                        ConversaOpcao2.text = pensamentoOp2[1];
                    }
                    else
                    {
                        ConversaOpcao2.text = nodeAtual.Opcao2;
                    }
                }
                else
                {
                    ConversaOpcao2.gameObject.SetActive(false);
                }

                if (!string.IsNullOrEmpty(nodeAtual.Opcao3))
                {
                    ConversaOpcao3.gameObject.SetActive(true);
                    string[] pensamentoOp3 = RemoverPensamento(nodeAtual.Opcao3);
                    if (pensamentoOp3 != null)
                    {
                        Pensamento3 = pensamentoOp3[0];
                        ConversaOpcao3.text = pensamentoOp3[1];
                    }
                    else
                    {
                        ConversaOpcao3.text = nodeAtual.Opcao3;
                    }
                }
                else
                {
                    ConversaOpcao3.gameObject.SetActive(false);
                }

                if (!string.IsNullOrEmpty(nodeAtual.Opcao4))
                {
                    ConversaOpcao4.gameObject.SetActive(true);
                    string[] pensamentoOp4 = RemoverPensamento(nodeAtual.Opcao4);
                    if (pensamentoOp4 != null)
                    {
                        Pensamento4 = pensamentoOp4[0];
                        ConversaOpcao4.text = pensamentoOp4[1];
                    }
                    else
                    {
                        ConversaOpcao4.text = nodeAtual.Opcao4;
                    }
                }
                else
                {
                    ConversaOpcao4.gameObject.SetActive(false);
                }

            }

            //se nao há mais opcoes, encerramos a conversa
            if (!nodeAtual.GetOutputPort("Exit").IsConnected)
            {
                endConversation();
            }
            else
            {
                WaitingAnswer = true;
            }
        }
    }



    public void IniciarConversa(Conversation_NodeGraph Conversa)
    {
        setAllConversationTexts(Conversa);
        Conversation = Conversa;
        conversationStarted = false;
        startNode = Conversation.GetStartingNode();
        DialogCanvas.SetActive(true);
        DialogCanvas2.SetActive(true);
        GameObject.Find("RostoPlayer").GetComponent<Image>().sprite = startNode.Retrato_Esquerda;
        GameObject.Find("RostoNPC").GetComponent<Image>().sprite = startNode.Retrato_direita;
        Parametros_globais.speed = 0;
        print(Parametros_globais.speed);      
        WaitingAnswer = false;
    }

    void setAllConversationTexts(Conversation_NodeGraph cng)
    //metodo que coloca os textos das conversas dentro dos nodes, conforme a lingua preferida
    //os valores sao lidos de um arquivo de texto
    {
        Debug.Log(Path.Combine(Application.dataPath, "falas", cng.name + '_' + preferred_language + ".conversation"));
        string[] falas = File.ReadAllLines(Path.Combine(Application.dataPath, "falas", cng.name + '_' + preferred_language+".conversation"));
        foreach (Conversation_Node cn in cng.getAllConversationNodes())
        {
            int index_identificador = Array.IndexOf(falas, "@" + cn.identificador) + 1;
            //Debug.Log("index identificador")
            if (index_identificador > -1)
            {
                //texto_npc
                for (int i = index_identificador; i < falas.Length; i++)
                {
                    if (falas[i].StartsWith("npc_text="))
                    {
                        cn.NPCText = falas[i].Split('=')[1];
                        break;
                    }
                    if (falas[i].StartsWith("@"))
                        break;
                }
                //para cada uma das operacoes...
                for (int op = 1; op <= 4; op++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        //buscar em cada uma das linhas do arquivo, a partir do identificador...
                        for (int i = index_identificador; i < falas.Length; i++)
                        {
                            Debug.Log(falas[i]);
                            //para evitar ler sem necessidade, se chegar em @, interromper
                            //isso significa que ja estamos em outro identificador
                            if (falas[i].StartsWith("@"))
                                break;
                            //se ele inicia com o valor da fala curta...
                            if (falas[i].StartsWith(string.Format("Op{0}=", op)))
                            {
                                if (op == 1)
                                    cn.Opcao1 = falas[i].Split('=')[1];
                                else if (op == 2)
                                    cn.Opcao2 = falas[i].Split('=')[1];
                                else if (op == 3)
                                    cn.Opcao3 = falas[i].Split('=')[1];
                                else if (op == 4)
                                    cn.Opcao4 = falas[i].Split('=')[1];
                                break;
                            }
                            //ou se ele inicia com o valor da fala longa...
                            else if (falas[i].StartsWith(string.Format("Op{0}_full=", op)))
                            {
                                if (op == 1)
                                    cn.Opcao1FullText = falas[i].Split('=')[1];
                                else if (op == 2)
                                    cn.Opcao2FullText = falas[i].Split('=')[1];
                                else if (op == 3)
                                    cn.Opcao3FullText = falas[i].Split('=')[1];
                                else if (op == 4)
                                    cn.Opcao4FullText = falas[i].Split('=')[1];
                                break;
                            }
                        }
                    }
                }
            }
        }

    }

    string[] RemoverPensamento(string text)
    {
        int indexInicial = text.IndexOf("(");
        if (indexInicial != -1)
        {
            int indexFinal = text.IndexOf(")");
            if (indexFinal != -1)
            {
                string texto1 = text.Substring(indexInicial + 1, indexFinal - indexInicial - 1);
                string texto2 = text.Substring(indexFinal + 1);
                string[] resposta = new string[2];
                resposta[0] = texto1;
                resposta[1] = texto2;
                return resposta;
            }
        }
        return null;
    }

    public void ConversationButtonPressed(GameObject self)
    {
        TextoPrincipal.alignment = TextAnchor.UpperLeft;
        //buscar pelo nome a opcao certa para escrever
        if (self == ConversaOpcao1.gameObject)
            TextoPrincipal.text = nodeAtual.Opcao1FullText;

        else if (self == ConversaOpcao2.gameObject)
            TextoPrincipal.text = nodeAtual.Opcao2FullText;

        else if (self == ConversaOpcao3.gameObject)
            TextoPrincipal.text = nodeAtual.Opcao3FullText;

        else if (self == ConversaOpcao4.gameObject)
            TextoPrincipal.text = nodeAtual.Opcao4FullText;
        //desabilitar todos os botoes
        ConversaOpcao1.gameObject.SetActive(false);
        ConversaOpcao2.gameObject.SetActive(false);
        ConversaOpcao3.gameObject.SetActive(false);
        ConversaOpcao4.gameObject.SetActive(false);


        StartCoroutine(ResetNodeBehaviour(self, CalculateTextTime()));
    }

    IEnumerator ResetNodeBehaviour(GameObject button, float time)
    {
        //verificando se a saida do node atual chega em um node de tipo EndNode
        //ou seja, validando se a conversa acaba com resultado gravado
        //o yield break sai da funcao
        if (nodeAtual.GetOutputPort("Exit").Connection.node.GetType() == typeof(EndNode))
        {
            EndNode nodeFinal = (EndNode)nodeAtual.GetOutputPort("Exit").Connection.node;
            Parametros_globais.AddOrUpdateResultados(Conversation.name, nodeFinal.Resultado);
            Parametros_globais.DebugAllResultados();
            endConversation(nodeFinal);
            yield break;
        }
        //chegamos aqui se a conversa continua de alguma forma
        yield return new WaitForSeconds(time);
        //reiniciar a situação toda após x tempo, geralmente dado pelo tamanho da ultima fala
        WaitingAnswer = false;
        //se o proximo node for uma decisao baseado em uma conversa anterior
        if (nodeAtual.GetOutputPort("Exit").Connection.node.GetType() == typeof(Decision_resultadoNode))
        {
            Decision_resultadoNode drn = (Decision_resultadoNode)nodeAtual.GetOutputPort("Exit").Connection.node;
            if (drn.GetBasedOnResult() == true)
                nodeAtual = (Conversation_Node)drn.GetOutputPort("Exit_if_true").Connection.node;
            else
                nodeAtual = (Conversation_Node)drn.GetOutputPort("Exit_if_false").Connection.node;
        }
        //se nao for end nem decision_resultado, só pode ser decisionNode
        else
        {
            DecisionNode dn = (DecisionNode)nodeAtual.GetOutputPort("Exit").Connection.node;
            //passa o nome da opcao escolhida para delimitar qual a saida certa do node de decisao
            nodeAtual = dn.GetNextNode(button.name);
        }
    }


    void endConversation()
    {
        StartCoroutine(EndConversationDelay(CalculateTextTime(), null));
    }
    void endConversation(EndNode nodeFinal)
    {
        StartCoroutine(EndConversationDelay(CalculateTextTime(), nodeFinal));
    }

    IEnumerator EndConversationDelay(float time, EndNode nodeFinal)
    //Fecha a interface de conversa e reseta a velocidade do player após time
    //opcionalmente, inicia outra conversa da sequencia após time
    {
        yield return new WaitForSeconds(time);
        if (nodeFinal == null || nodeFinal.conversa_seguinte == null)
        {
            DialogCanvas.SetActive(false);
            DialogCanvas2.SetActive(false);
            Parametros_globais.speed = Parametros_globais.startingSpeed;
            Conversation = null;
        }
        else
        {
            IniciarConversa(nodeFinal.conversa_seguinte);
        }
    }

    float CalculateTextTime()
    //calcula quanto tempo o texto atual precisa ficar na tela antes de sumir
    {
        float valor;
        if (string.IsNullOrEmpty(TextoPrincipal.text))
            valor = 0f;
        else
        {
            valor = TextoPrincipal.text.Length * 0.10f;
            if (valor < 3f)
                valor = 3f;
        }
        return valor;
    }

    public void SetPensamento(GameObject source)
    //lida com o texto do campo de pensamento, acima do frame do personagem
    {
        if (source == ConversaOpcao1.gameObject)
            balaoPensamento.text = Pensamento1;
        else if (source == ConversaOpcao2.gameObject)
            balaoPensamento.text = Pensamento2;
        else if (source == ConversaOpcao3.gameObject)
            balaoPensamento.text = Pensamento3;
        else if (source == ConversaOpcao4.gameObject)
            balaoPensamento.text = Pensamento4;
    }

    void LoadLanguage()
    {
        try
        {
            string[] arquivo_config = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "config"));
            string lingua = arquivo_config.First(x => x.Split('=')[0] == "preferred_language");
            if (!string.IsNullOrEmpty(lingua))
                preferred_language = lingua.Split('=')[1];
            else
                //caso a opcao nao exista, default pt_br
                preferred_language = "pt_br";
        }
        catch (FileNotFoundException)
        {
            Debug.Log("arquivo config nao encontrado");
            preferred_language = "pt_br";
        }
    }

}
