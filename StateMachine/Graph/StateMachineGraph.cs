using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace RPGFramework {
	[CreateAssetMenu]
	public class StateMachineGraph : NodeGraph {

		// The current "active" node
		[SerializeField]
		public StateNode currentState { get; private set; }
		public StateNode defaultState;
		[SerializeField]
		private GameObject stateControlledObject;

		public void SetStateMachineOwner(GameObject owner) {
			stateControlledObject = owner;
		}

		public GameObject GetStateMachineOwner() {
			return stateControlledObject;
		}

		public void Init(GameObject owningGameObject) {
			currentState = defaultState;
			stateControlledObject = owningGameObject;
		}

		public void NextState() {
			currentState.MoveNext();
		}

		public void TransitionToState(StateNode nextState) {
			if(nextState != null)
			{
				currentState.OnExit();
				currentState = nextState;
				nextState.OnEnter();
			} else
			{
				Debug.LogError("Tried to transition to null state!");
			}
		}

	}
}