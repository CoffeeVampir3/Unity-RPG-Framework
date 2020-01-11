using System;
using System.Collections.Generic;
using UnityEngine;

namespace RT {
	[CreateAssetMenu]
	public class WiggleState : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
		}

		private float mew = 0f;
		public override void OnUpdate(GameObject thisObject) {
			Transform t = thisObject.transform;
			mew += Mathf.PI / 60f;
			float newZ = 2 * Mathf.Cos(mew);
			t.localPosition += new Vector3(0, 0, newZ);
		}
	}
}
