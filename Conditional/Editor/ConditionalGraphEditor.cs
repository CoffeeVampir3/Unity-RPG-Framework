using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using XNode;
using UnityEditor;
using RPGFramework.CndGraph;

namespace RPGFramework.CndGraph {
	[CustomNodeGraphEditor(typeof(ConditionalGraph))]
	public class ConditionalGraphEditor : NodeGraphEditor {

		public override string GetNodeMenuName(System.Type type) {
			if (type.Namespace.Contains("RPGFramework.CndGraph"))
			{
				return base.GetNodeMenuName(type).Replace("Conditions/", "");
			}
			else return null;
		}

		/// <summary>
		/// Removes our condition graph automatically if we delete the root node.
		/// </summary>
		public void DeleteIfNoRootNode() {
			ConditionalGraph cur = this.target as ConditionalGraph;
			if (cur.rootNode == null)
			{
				cur.Clear();
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(cur));
			}
		}

		/// <summary>
		/// Allows the window to be repainted so our states update correctly.
		/// </summary>
		public override void OnGUI() {
			base.OnGUI();
			window.Repaint();

			DeleteIfNoRootNode();
		}

	}
}