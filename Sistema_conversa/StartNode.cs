using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class StartNode : Node {


    [Output] public bool Inicio;
    public Sprite Retrato_Esquerda;
    public Sprite Retrato_direita;

	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
        return true;
	}
}