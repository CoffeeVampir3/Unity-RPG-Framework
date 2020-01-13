using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.Helpers {
	public class PortalNodeOutput : Node, IForwardingNode {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public Any outputValue;
		public PortalNodeInput inputNode; //States dont care, used to transmit values only.

		public override void OnCreateConnection(NodePort from, NodePort to) {
			if (this.name.Contains("Node Output"))
			{
				this.name = to.node.name + " Output Portal";
			}
		}

		public override object GetValue(NodePort port) {
			if (inputNode == null)
				return null;
			return inputNode.GetInputPort("enteringValue").GetInputValue();
		}

		public NodePort GetForwardingPort() {
			if (inputNode == null)
				return null;
			return inputNode.GetOutputPort("enteringValue");
		}

		[System.Serializable]
		public class Any { }
	}
}
