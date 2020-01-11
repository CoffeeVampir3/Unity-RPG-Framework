using System;
using System.Collections.Generic;
using UnityEngine;

namespace RT {
	[CreateAssetMenu]
	public class SquiggleState : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
			Debug.Log("Exited");
		}

		public override void OnUpdate(GameObject thisObject) {
			Debug.Log("Updating");
		}
	}
}
