using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditor;
using RPGFramework.CndGraph;

namespace RPGFramework.SMGraph {
	[CustomNodeEditor(typeof(TransitionNode))]
	public class TransitionNodeEditor : NodeEditor {

		public override void OnBodyGUI() {
			base.OnBodyGUI();

			if (GUILayout.Button("Open Condition Graph"))
			{
				InstantiateOrOpenConditionEditor(this.target);
			}
		}

		/// <summary>
		/// Creates a new conditional graph as an asset
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public ConditionalGraph CreateNewConditionalGraph(TransitionNode node) {
			ConditionalGraph condGraph = ScriptableObject.CreateInstance<ConditionalGraph>();
			string nodePath = AssetDatabase.GetAssetPath(node);

			string condPath = nodePath.Substring(0, nodePath.LastIndexOf("/") + 1);
			condPath += node.name + "subGraph.asset";
			condGraph.name = node.name + "ConditionalGraph";
			string finalPath = AssetDatabase.GenerateUniqueAssetPath(condPath);
			AssetDatabase.CreateAsset(condGraph, finalPath);

			node.conditionGraph = condGraph;
			condGraph.parentNode = node;

			ConditionalRootNode rootnode = condGraph.AddNode<ConditionalRootNode>();
			rootnode.name = "Condition Root Node";
			AssetDatabase.AddObjectToAsset(rootnode, condGraph);

			condGraph.rootNode = rootnode;
			return condGraph;
		}

		public void InstantiateOrOpenConditionEditor(XNode.Node targetNode) {
			TransitionNode node = targetNode as TransitionNode;
			ConditionalGraph condGraph = node.conditionGraph;
			if (condGraph == null)
			{
				condGraph = CreateNewConditionalGraph(node);
			}

			NodeEditorWindow.Open(condGraph);
		}
	}
}