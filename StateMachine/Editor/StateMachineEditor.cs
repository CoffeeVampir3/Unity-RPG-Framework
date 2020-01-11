using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;

namespace RT {
	[CustomNodeGraphEditor(typeof(StateMachineGraph))]
	public class StateMachineEdtior : NodeGraphEditor {

		public override string GetNodeMenuName(System.Type type) {
			if (type.Namespace.Contains("RT"))
			{
				return base.GetNodeMenuName(type).Replace("FSM/", "");
			}
			else return null;
		}

	}
}