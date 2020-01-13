using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.SMGraph;

namespace RPGFramework.CndGraph {
	[CreateAssetMenu]
	public class ConditionalGraph : NodeGraph {

		[HideInInspector]
		[SerializeField]
		public TransitionNode parentNode = null;
		[HideInInspector]
		[SerializeField]
		public ConditionalRootNode rootNode = null;

		[SerializeField]  
		private GameObject stateControlledObject;

		public GameObject GetStateMachineOwner() {
			StateMachineGraph graph = parentNode.graph as StateMachineGraph;

			return graph.GetStateMachineOwner();
		}

		public void Init(GameObject owningGameObject) {
			stateControlledObject = owningGameObject;
		}

		/// <summary>
		/// Performs a full evaluation of the graph and returns wether or not you should transition.
		/// </summary>
		/// <returns>If you should transition states (true) or not (false) </returns>
		public bool EvaluateGraph() {
			return rootNode.ShouldStateTransition();
		}


	}
}