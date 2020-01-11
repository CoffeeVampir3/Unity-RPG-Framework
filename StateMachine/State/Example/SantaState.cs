using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework {
	[CreateAssetMenu]
	public class SantaState : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
		}

		public override void OnUpdate(GameObject thisObject) {
			Transform t = thisObject.transform;
			t.Rotate(Vector3.up, 3);
		}
	}
}
