using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class EndNode : Node
{

    public bool Resultado;
    [Input]public int  Conector;
    public Conversation_NodeGraph conversa_seguinte;

    // Use this for initialization
    protected override void Init()
    {
        base.Init();

    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return true;
    }
}
