using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;

namespace RPGFramework.SMGraph {
	[CustomNodeEditor(typeof(StateNode))]
	public class StateNodeEditor : NodeEditor {

		public override void OnHeaderGUI() {
			GUI.color = Color.white;
			StateNode node = target as StateNode;
			StateMachineGraph graph = node.graph as StateMachineGraph;

			if (graph.currentState == node)
			{
				GUI.color = Color.red;
			}

			if(graph.defaultState == node)
			{
				GUI.color = Color.blue;
			}

			if (graph.defaultState == node && graph.currentState == node)
			{
				GUI.color = Color.cyan;
			}

			string title = target.name;
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
			GUI.color = Color.white;
		}

		/// <summary>
		/// Finds the owner of our state machine if one exists.
		/// </summary>
		/// <param name="fsmgraph"></param>
		private void FindOwner_Editor(StateMachineGraph fsmgraph) {
			var stateMachines = GameObject.FindObjectsOfType<StateMachine>();

			for(int i = 0; i < stateMachines.Length; i++)
			{
				if(stateMachines[i].CompareCurrentGraph(fsmgraph))
				{
					fsmgraph.SetStateMachineOwner(stateMachines[i].gameObject);
				}
			}
		}

		public override void OnBodyGUI() {
			base.OnBodyGUI();
			StateNode node = target as StateNode;
			StateMachineGraph fsmgraph = node.graph as StateMachineGraph;

			if(fsmgraph.GetStateMachineOwner() == null)
			{
				FindOwner_Editor(fsmgraph);
			}

			if (GUILayout.Button("Proceed State"))
			{
				fsmgraph.NextState();
			}

			if (GUILayout.Button("Set as default state"))
			{
				node.name = "Starting State";
				fsmgraph.defaultState = node;
			}
		}
	}
}