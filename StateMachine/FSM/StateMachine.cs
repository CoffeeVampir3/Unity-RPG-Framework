using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGFramework.SMGraph;

namespace RPGFramework {

	/// <summary>
	/// Add onto any monobehaviour to control it with a behaviour graph.
	/// </summary>
	public class StateMachine : MonoBehaviour {
		[SerializeField]
		private StateMachineGraph stateGraph = null; //Our objects behaviour graph

		protected void Awake() {
			if (stateGraph == null)
			{
				return;
			}

			stateGraph.Init(this.gameObject);
		}

		protected void Start() {
			//Start the default start immediately after the graph initialization.
			GetCurrentState()?.OnEnterState(this.gameObject);
		}

		protected void Update() {
			if (stateGraph != null)
			{
				//If we have a current state call its OnUpdate
				GetCurrentState()?.OnUpdate(this.gameObject);
			}
		}

		public BaseState GetCurrentState() {
			if(stateGraph == null)
			{
				return null;
			}
			return stateGraph.currentState.thisState;
		}

		public void NextState() {
			if (stateGraph == null)
			{
				return;
			}
			stateGraph.NextState();
		}

		public bool CompareCurrentGraph(StateMachineGraph comparingTo) {
			if (stateGraph == null)
				return false;

			return stateGraph.Equals(comparingTo);
		}


	}
}
