using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>
/// TODO:: Conditions
/// </summary>

namespace RPGFramework.SMGraph {
	public class StateNode : Node {

		[Input(ShowBackingValue.Never, ConnectionType.Multiple)] public BaseState enterState = null;
		[Output(ShowBackingValue.Never, ConnectionType.Override)] public BaseState exitState; //Variable hard reference by string name in MoveNext()

		public BaseState thisState;

		public override object GetValue(NodePort port) {
			return thisState;
		}

		#region MovingStates

		private void MoveNextImmediateState(Node connectingNode) {
			if(connectingNode is StateNode)
			{
				StateNode node = connectingNode as StateNode;
				StateMachineGraph fsmGraph = graph as StateMachineGraph;
				fsmGraph.TransitionToState(node);
			}
		}

		private void MoveNextTransition(Node connectingNode) {
			if(connectingNode is TransitionNode)
			{
				TransitionNode node = connectingNode as TransitionNode;
				bool shouldTransition = node.ShouldTransition();
				NodePort branchPort = node.GetTransitionPath(shouldTransition);
				Node nextNode = branchPort.node;

				if (nextNode == null)
				{
					return;
				}
				MoveNextImmediateState(nextNode);
				MoveNextTransition(nextNode);
				MoveNextForwarded(nextNode);

			}
		}

		private void MoveNextForwarded(Node connectingNode) {
			if (connectingNode is Helpers.IForwardingNode)
			{
				Helpers.IForwardingNode inNode = connectingNode as Helpers.IForwardingNode;
				NodePort outNodeExitPort = inNode.GetForwardingPort();

				Node nextNode = outNodeExitPort.Connection.node;
				if(nextNode == null)
				{
					return;
				}
				//Recur through, including forwarding nodes.
				MoveNextImmediateState(nextNode);
				MoveNextTransition(nextNode);
				MoveNextForwarded(nextNode);
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

			MoveNextImmediateState(exitPort.Connection.node);
			MoveNextTransition(exitPort.Connection.node);
			MoveNextForwarded(exitPort.Connection.node);
		}

		#endregion

		#region StateMachine functions

		public void OnEnter() {
			StateMachineGraph fsmGraph = graph as StateMachineGraph;
			if(this.thisState != null)
			{
				GameObject owner = fsmGraph.GetStateMachineOwner();
				if(owner != null)
				{
					this.thisState.OnEnterState(owner);
				}
			}
		}

		public void OnExit() {
			StateMachineGraph fsmGraph = graph as StateMachineGraph;
			if (this.thisState != null)
			{
				GameObject owner = fsmGraph.GetStateMachineOwner();
				if (owner != null)
				{
					this.thisState.OnEnterState(owner);
				}
			}
		}

		public void OnUpdate() {
			StateMachineGraph fsmGraph = graph as StateMachineGraph;
			if (this.thisState != null)
			{
				GameObject owner = fsmGraph.GetStateMachineOwner();
				if (owner != null)
				{
					this.thisState.OnEnterState(owner);
				}
			}
		}

		#endregion

	}
}