using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

namespace RPGFramework.CndGraph {
	public class ConditionalRootNode : ConditionNode {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public bool shouldTransition; //Hard refereced by string name
		[SerializeField]
		[HideInInspector]
		private bool transitionValue = false;

		public override void Evaluate() {
			NodePort condPort = GetInputPort("shouldTransition");

			if(condPort.IsConnected)
			{
				transitionValue = condPort.GetInputValue<bool>();
			} else
			{
				transitionValue = false; //Don't allow the transition as a default.
			}
		}

		public bool ShouldStateTransition() {
			Evaluate();
			return transitionValue;
		}
	}
}
