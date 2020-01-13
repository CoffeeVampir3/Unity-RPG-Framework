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

		[SerializeField]
		public bool overrideOut = false;
		[SerializeField]
		public bool overrideValue = false;

		[HideInInspector]
		public ConditionalGraph conditionGraph = null;

		public bool ShouldTransition() {
			if (conditionGraph == null)
			{
				Debug.LogError("No graph associated with transition: " + this.name);
				return false; //Deny transition by default.
			}
			return conditionGraph.EvaluateGraph();
		}

		public NodePort GetTransitionPath(bool transitionBranch, bool dontOverride = false) {
			if(overrideOut == true && dontOverride == false)
			{
				return GetTransitionPath(overrideValue, true);
			}
			if(transitionBranch)
			{
				NodePort portBranch = GetPort("transitionIfTrue");
				NodePort connection = portBranch.GetConnection(0);
				return connection;
			} else
			{
				NodePort portBranch = GetPort("transitionIfFalse");
				NodePort connection = portBranch.GetConnection(0);
				return connection;
			}
		}

		public override object GetValue(NodePort port) {
			NodePort enteringState = GetInputPort("enteringState");

			//Evaluate condition graph and return its value
			return null;
		}

	}
}
