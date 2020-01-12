using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;

namespace RPGFramework.SMGraph {
	[CustomNodeGraphEditor(typeof(StateMachineGraph))]
	public class StateMachineEdtior : NodeGraphEditor {

		public override string GetNodeMenuName(System.Type type) {
			if (type.Namespace.Contains("RPGFramework.SMGraph"))
			{
				return base.GetNodeMenuName(type).Replace("State Machine/", "");
			}
			else return null;
		}

		/// <summary>
		/// Allows the window to be repainted so our states update correctly.
		/// </summary>
		public override void OnGUI() {
			base.OnGUI();
			window.Repaint();
		}

	}
}