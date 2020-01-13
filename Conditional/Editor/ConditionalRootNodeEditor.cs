using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using RPGFramework.SMGraph;

namespace RPGFramework.CndGraph {
	[CustomNodeEditor(typeof(ConditionalRootNode))]
	public class ConditionalRootNodeEditor : NodeEditor {

		private int evaluationCounter = 0;
		private int evaluateEveryNFrames = 30; //Recalculate every N frames
		private bool lastCachedEvaluation = false;

		private void EvaulateGraphCached(ConditionalGraph graph) {
			if (evaluationCounter++ >= evaluateEveryNFrames)
			{
				lastCachedEvaluation = graph.EvaluateGraph();
				evaluationCounter = 0;
			}
		}

		public override void OnHeaderGUI() {
			GUI.color = Color.white;
			ConditionalRootNode node = target as ConditionalRootNode;
			ConditionalGraph graph = node.graph as ConditionalGraph;

			EvaulateGraphCached(graph);
			if (lastCachedEvaluation)
			{
				GUI.color = Color.green;
			}
			else
			{
				GUI.color = Color.red;
			}

			string title = target.name;
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
			GUI.color = Color.white;
		}

		public override void OnBodyGUI() {
			base.OnBodyGUI();
			ConditionalRootNode node = target as ConditionalRootNode;
			ConditionalGraph condGraph = node.graph as ConditionalGraph;

			if (GUILayout.Button("Back To State Graph"))
			{
				StateMachineGraph fsmgraph = condGraph.parentNode.graph as StateMachineGraph;

				NodeEditorWindow.Open(fsmgraph);
			}

		}
	}
}