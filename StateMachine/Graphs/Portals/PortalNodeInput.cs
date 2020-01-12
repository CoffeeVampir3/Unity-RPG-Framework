using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.Portals {
	public class PortalNodeInput : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public Any enteringValue;
		[SerializeField]
		public PortalNodeOutput outputNode = null;

		public override void OnCreateConnection(NodePort from, NodePort to) {
			if(this.name.Contains("Node Input"))
			{
				this.name = from.node.name + " Input Portal";
			}
		}

		[System.Serializable]
		public class Any { }
	}
}
