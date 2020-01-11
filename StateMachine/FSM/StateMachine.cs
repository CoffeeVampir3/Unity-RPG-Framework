using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RT {
	public class StateMachine : MonoBehaviour {
		public StateMachineGraph stateGraph;

		public void Awake() {
			if (stateGraph == null)
			{
				return;
			}

			stateGraph.Init(this.gameObject);

			if(stateGraph.currentState != null)
			{
				stateGraph.currentState.thisState.OnEnterState(this.gameObject);
			}
		}

		public void Update() {
			if(stateGraph != null)
			{
				BaseState cur = GetCurrentState();
				if(cur != null)
				{
					cur.OnUpdate(this.gameObject);
				}
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
	}
}
