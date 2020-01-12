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
				StateNode transitionNode = node.GetTransitionNode(shouldTransition);
				StateMachineGraph fsmGraph = graph as StateMachineGraph;
				fsmGraph.TransitionToState(transitionNode);
			}
		}

		private void MoveNextPortal(Node connectingNode) {
			if (connectingNode is Portals.PortalNodeInput)
			{
				Portals.PortalNodeInput inNode = connectingNode as Portals.PortalNodeInput;
				Portals.PortalNodeOutput outNode = inNode.outputNode;
				NodePort outNodeExitPort = outNode.GetOutputPort("outputValue");

				//Recur through the portal
				MoveNextImmediateState(outNodeExitPort.Connection.node);
				MoveNextTransition(outNodeExitPort.Connection.node);
				MoveNextPortal(outNodeExitPort.Connection.node);
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
			MoveNextPortal(exitPort.Connection.node);
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