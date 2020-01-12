using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.SMGraph {
	public class TransitionNode : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public BaseState enteringState;
		[Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseState transitionIfTrue;
		[Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseState transitionIfFalse;

		[HideInInspector]
		public ConditionalGraph conditionGraph = null;

		[TextArea(2, 12)]
		public string comment;

		public bool ShouldTransition() {
			if (conditionGraph == null)
			{
				Debug.LogError("No graph associated with transition: " + this.name);
				return false; //Deny transition by default.
			}
			return conditionGraph.EvaluateGraph();
		}

		public StateNode GetTransitionNode(bool transitionBranch) {
			if(transitionBranch)
			{
				NodePort portBranch = GetPort("transitionIfTrue");
				NodePort connection = portBranch.GetConnection(0);
				if (connection.node is StateNode)
					return connection.node as StateNode;
			} else
			{
				NodePort portBranch = GetPort("transitionIfFalse");
				NodePort connection = portBranch.GetConnection(0);
				if (connection.node is StateNode)
					return connection.node as StateNode;
			}
			return null;
		}

		public override object GetValue(NodePort port) {
			NodePort enteringState = GetInputPort("enteringState");

			//Evaluate condition graph and return its value
			return null;
		}

	}
}
