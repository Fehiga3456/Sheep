using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Conversation_Node : Node {

    [Input] public bool Entry;
    public string identificador;
    public string NPCText;
    public string Opcao1;
    public string Opcao1FullText;
    public string Opcao2;
    public string Opcao2FullText;
    public string Opcao3;
    public string Opcao3FullText;
    public string Opcao4;
    public string Opcao4FullText;
    int ChosenAnswer;
    public GameObject pessoaatrazer;

    [Output] public int Exit;

    // Use this for initialization
    protected override void Init() {
		base.Init();		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}
}