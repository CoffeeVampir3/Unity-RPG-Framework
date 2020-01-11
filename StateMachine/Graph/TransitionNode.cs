using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

namespace RPGFramework {
	public class TransitionNode : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public BaseState enteringState;
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public bool enteringCondition;
		[Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseState transitionToState;

		public bool ShouldTransition() {
			return GetInputValue<bool>("enteringCondition");
		}

		public StateNode GetTransitionNode() {
			NodePort p = GetPort("transitionToState");
			if(p.IsConnected)
			{
				NodePort connection = p.GetConnection(0);
				if(connection.node is StateNode)
					return connection.node as StateNode;
			}
			return null;
		}

		public override object GetValue(NodePort port) {
			NodePort enteringState = GetInputPort("enteringState");
			NodePort condPort = GetInputPort("enteringCondition");

			//Ports arent connected, no transition
			if(!enteringState.IsConnected || !condPort.IsConnected) {
				return null;
			}

			if(condPort.GetInputValue<bool>())
			{
				return enteringState.GetInputValue<BaseState>();
			}
			return null;
		}

	}
}
