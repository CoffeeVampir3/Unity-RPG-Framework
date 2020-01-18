using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGFramework {
	[CreateAssetMenu]
	public class MovingState : BaseState {
		public override void OnEnterState(GameObject thisObject) {
		}

		public override void OnExitState(GameObject thisObject) {
		}

		public override void OnUpdate(GameObject thisObject) {
			Transform t = thisObject.transform;
			Rigidbody rb = thisObject.GetComponent<Rigidbody>();

			rb.velocity += t.forward * .02f;

			RaycastHit hit;
			Debug.DrawRay(t.position, t.forward * 5f, Color.green, .05f);
			if (Physics.Raycast(t.position, t.forward, out hit, 5f))
			{
				StateMachine sm = thisObject.GetComponent<StateMachine>();
				sm.NextState();
			}
		}
	}
}
