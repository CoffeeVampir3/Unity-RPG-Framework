using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework {
	[CreateAssetMenu]
	public class SquiggleState : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
		}

		private float mew = 0f;
		public override void OnUpdate(GameObject thisObject) {
			Transform t = thisObject.transform;
			mew += Mathf.PI / 60f;
			float newX = .1f * Mathf.Cos(mew);
			float newY = .1f * Mathf.Sin(mew);
			t.localPosition += new Vector3(newX, newY);
		}
	}
}
