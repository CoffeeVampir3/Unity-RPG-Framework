using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace RPGFramework.SMGraph {
	[CreateAssetMenu]
	public class StateMachineGraph : NodeGraph {

		// The current "active" node
		[SerializeField]
		public StateNode currentState { get; private set; }
		[SerializeField]
		[HideInInspector]
		private StateNode _defaultState;
		[SerializeField]
		public StateNode defaultState {
			get { return _defaultState; }
			set {
				_defaultState = value;
				if(currentState == null)
				{
					currentState = value;
				}
			}
		}

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