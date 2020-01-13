using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGFramework {
	[CreateAssetMenu]
	public class PatrolRight : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
		}

		private float mew = 0f;
		public override void OnUpdate(GameObject thisObject) {
			Transform t = thisObject.transform;
			t.localPosition += new Vector3(-.05f, 0);

			StateMachine sm = thisObject.GetComponent<StateMachine>();
			sm.NextState();
		}
	}
}
