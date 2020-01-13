using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;
using UnityEngine;

namespace RPGFramework.Helpers {
	public class SelectNode : Node, IForwardingNode {
		[Input(ShowBackingValue.Never, ConnectionType.Multiple)] public int select;
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public Any input;
		[Output(ShowBackingValue.Never, ConnectionType.Multiple, dynamicPortList = true)] public Any selections;

		public NodePort GetForwardingPort() {
			NodePort selectorPort = GetOutputPort("selections");

			int selectValue = GetInputValue<int>("select");
			int numPorts = DynamicPorts.Count();
			if (selectValue > numPorts || selectValue < 0)
			{
				return null;
			}

			return DynamicPorts.ElementAt(selectValue);
		}

		public override object GetValue(NodePort port) {
			if(port == GetForwardingPort())
			{
				//Get the output of the connecting value.
				return GetInputPort("input")?.Connection?.GetOutputValue();
			}
			return null;
		}

	}
}
