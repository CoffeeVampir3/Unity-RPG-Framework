using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// TODO:: Conditions
/// </summary>

namespace RT {
	public class StateNode : Node {

		[Input] public BaseState enterState = null;
		[Output] public BaseState exitState; //Variable hard reference by string name in MoveNext()

		public BaseState thisState;

		#region Connections
		/// <summary>
		/// Creating a connection branches into two modes:
		/// 
		/// </summary>
		[SerializeField]
		private bool usingTransitionMode = false;
		private void StateMode(NodePort from, NodePort to) {
			if (from.ConnectionCount > 1)
			{
				from.ClearConnections();
				from.Connect(to);
				usingTransitionMode = false;
			}
		}

		private void TransitionMode(NodePort from, NodePort to) {
			if (usingTransitionMode == false)
			{
				if (from.ConnectionCount > 1)
				{
					from.ClearConnections();
					from.Connect(to);
				}
			}
			usingTransitionMode = true;
		}

		public override void OnCreateConnection(NodePort from, NodePort to) {
			if (to.node is StateNode)
			{
				StateMode(from, to);
				return;
			}

			else if (to.node is TransitionNode)
			{
				TransitionMode(from, to);
				return;
			} else
			{
				//Invalid connection
				Debug.LogError("Invalid connection");
				from.ClearConnections();
			}
		}

		#endregion

		public override object GetValue(NodePort port) {
			return thisState;
		}

		#region State Controls

		private void MoveNextImmediateState(NodePort exitPort) {
			StateNode node = exitPort.Connection.node as StateNode;
			StateMachineGraph fsmGraph = graph as StateMachineGraph;
			fsmGraph.TransitionToState(node);
		}

		private void MoveNextTransition(NodePort exitPort) {
			var connectedPorts = exitPort.GetConnections();

			foreach(NodePort connection in connectedPorts)
			{
				TransitionNode node = connection.node as TransitionNode;
				if(node.ShouldTransition())
				{
					StateMachineGraph fsmGraph = graph as StateMachineGraph;
					fsmGraph.TransitionToState(node.GetTransitionNode());
				}
			}
		}

		public void MoveNext() {
			StateMachineGraph fsmGraph = graph as StateMachineGraph;

			if (fsmGraph.currentState != this)
			{
				Debug.LogError("This node is not the current one.");
				return;
			}

			NodePort exitPort = GetPort("exitState");

			if (!exitPort.IsConnected)
			{
				Debug.LogError("State exit port is not connected");
				return;
			}

			if(!usingTransitionMode)
			{
				MoveNextImmediateState(exitPort);
			} else
			{
				MoveNextTransition(exitPort);
			}
		}

		public void OnEnter() {
			StateMachineGraph fsmGraph = graph as StateMachineGraph;
			if(this.thisState != null)
			{
				this.thisState.OnEnterState(fsmGraph.stateControlledObject);
			}
		}

		#endregion

	}
}