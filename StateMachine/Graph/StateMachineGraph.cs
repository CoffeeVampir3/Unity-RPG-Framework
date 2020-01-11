using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace RT {
	[CreateAssetMenu]
	public class StateMachineGraph : NodeGraph {

		// The current "active" node
		[SerializeField]
		public StateNode currentState { get; private set; }
		public StateNode defaultState;
		public GameObject stateControlledObject;

		public void Init(GameObject owningGameObject) {
			currentState = defaultState;
			stateControlledObject = owningGameObject;
		}

		public void NextState() {
			currentState.MoveNext();
		}

		public void TransitionToState(StateNode nextState) {
			//currentState.onExit(); TODO::
			if(nextState != null)
			{
				currentState = nextState;
				nextState.OnEnter();
			} else
			{
				Debug.LogError("Tried to transition to null state!");
			}
		}

	}
}