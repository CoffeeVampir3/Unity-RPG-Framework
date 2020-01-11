using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;

namespace RT {
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

		public override void OnBodyGUI() {
			base.OnBodyGUI();
			StateNode node = target as StateNode;
			StateMachineGraph graph = node.graph as StateMachineGraph;
			if (GUILayout.Button("MoveNext Node"))
			{
				node.MoveNext();
			}

			if (GUILayout.Button("Continue Graph"))
			{
				graph.NextState();
			}

			if (GUILayout.Button("Set as default state"))
			{
				node.name = "Starting State";
				graph.defaultState = node;
			}
		}
	}
}