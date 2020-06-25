using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class Conversation_NodeGraph : NodeGraph { 
	public StartNode GetStartingNode()
    {
        return (StartNode)this.nodes.Find(n => n.name == "Start");
    }

    public List<Conversation_Node> getAllConversationNodes()
    {
        var all = nodes.FindAll(x => typeof(Conversation_Node) == x.GetType());
        List<Conversation_Node> resultado = new List<Conversation_Node>();
        foreach (Node n in all)
        {
            resultado.Add((Conversation_Node)n);
        }
        return resultado;
    }
}