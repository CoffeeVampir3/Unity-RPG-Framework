using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGFramework {
	[CreateAssetMenu]
	public class PickNewDirectionState : BaseState {

		public Vector3 GetFreeDirection(GameObject thisObject) {
			float angle = 0;
			Transform t = thisObject.transform;
			for (int i = 0; i < 16; i++)
			{
				float y = Mathf.Cos(angle);
				angle += 2 * Mathf.PI / 16;

				Vector3 dir = new Vector3(0, t.position.y + y, 0);
				RaycastHit hit;
				Debug.DrawLine(t.position, dir, Color.red, 10.0f);
				if (Physics.Raycast(t.position, dir, out hit))
				{
					Debug.Log(dir);
					return dir;
				}
			}
			return Vector3.zero;
		}

		public override void OnEnterState(GameObject thisObject) {
		
		}

		public override void OnExitState(GameObject thisObject) {

		}

		public override void OnUpdate(GameObject thisObject) {


			Debug.Log("Changed directions!");
			Rigidbody rb = thisObject.GetComponent<Rigidbody>();

			rb.velocity = Vector3.zero;
			Vector3 dir = GetFreeDirection(thisObject);

			thisObject.transform.LookAt(new Vector3(thisObject.transform.rotation.eulerAngles.x + dir.y,
				thisObject.transform.rotation.eulerAngles.y,
				thisObject.transform.rotation.eulerAngles.z), thisObject.transform.up);


			//StateMachine sm = thisObject.GetComponent<StateMachine>();
			//sm.NextState();
		}
	}
}
