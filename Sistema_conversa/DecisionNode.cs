using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DecisionNode : Node {

    [Input]public int Value;
    [Output] public bool Exit1;
    [Output] public bool Exit2;
    [Output] public bool Exit3;
    [Output] public bool Exit4;

    // Use this for initialization
    protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
        if (Value != null)
        {
            if (Value == 1 && port.fieldName == "Exit1")
            {
                return true;
            }
            else if (Value == 2 && port.fieldName == "Exit2")
            {
                return true;
            }
            else if (Value == 3 && port.fieldName == "Exit3")
            {
                return true;
            }
            else if (Value == 4 && port.fieldName == "Exit4")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public Conversation_Node GetNextNode(string ChosenOption)
    {
        if (ChosenOption == "ConversaOpcao1")
        {
            return (Conversation_Node)this.GetOutputPort("Exit1").Connection.node;
        }
        else if (ChosenOption == "ConversaOpcao2")
        {
            return (Conversation_Node)this.GetOutputPort("Exit2").Connection.node;
        }
        else if (ChosenOption == "ConversaOpcao3")
        {
            return (Conversation_Node)this.GetOutputPort("Exit3").Connection.node;
        }
        else if (ChosenOption == "ConversaOpcao4")
        {
            return (Conversation_Node)this.GetOutputPort("Exit4").Connection.node;
        }
        else
            return null;
    }
}