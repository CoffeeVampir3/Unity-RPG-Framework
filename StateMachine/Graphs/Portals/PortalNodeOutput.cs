using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.Portals {
	public class PortalNodeOutput : Node {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public Any outputValue;
		public PortalNodeInput inputNode;


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

		[System.Serializable]
		public class Any { }
	}
}
