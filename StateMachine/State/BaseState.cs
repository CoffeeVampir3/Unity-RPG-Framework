using System;
using System.Collections.Generic;
using UnityEngine;

namespace RT {
	[CreateAssetMenu]
	public class BaseState : ScriptableObject {
		public void OnEnterState(GameObject thisObject) {
			thisObject.transform.position += new Vector3(3f, 3f, 0);
		}
	}
}
