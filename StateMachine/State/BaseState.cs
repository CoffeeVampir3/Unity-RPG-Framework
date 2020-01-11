using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework {
	public abstract class BaseState : ScriptableObject {
		public abstract void OnEnterState(GameObject thisObject);
		public abstract void OnUpdate(GameObject thisObject);
		public abstract void OnExitState(GameObject thisObject);
	}
}
