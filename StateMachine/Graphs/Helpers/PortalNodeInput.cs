using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.Helpers {
	public class PortalNodeInput : Node, IForwardingNode {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public Any enteringValue;
		[SerializeField]
		public PortalNodeOutput outputNode = null;

		public NodePort GetForwardingPort() {
			if (outputNode == null)
				return null;
			return outputNode.GetOutputPort("outputValue");
		}

		public override void OnCreateConnection(NodePort from, NodePort to) {
			if(this.name.Contains("Node Input"))
			{
				this.name = from.node.name + " Input Portal";
			}
		}
	}
}
