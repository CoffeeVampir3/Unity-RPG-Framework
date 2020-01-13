using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

namespace RPGFramework.CndGraph {
	public class StatThresholdConditionNode : ConditionNode {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public int inputValue;
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public bool evaluation;
		public int thresholdPercent; //percentage of maxValue
		[SerializeField]
		private int convertedMeetsValue = 0; //Display only

		[SerializeField]
		[HideInInspector]
		private int inputStatValue;
		[SerializeField]
		[HideInInspector]
		private int maxValue = int.MinValue; //Set once on initialization... Never updates if the max changes.

		protected override void Init() {
			base.Init();

			NodePort fromNode = GetInputPort("inputValue");
			if(fromNode.IsConnected)
			{
				GetNodeValues();
				Evaluate();
			}
		}

		private void GetNodeValues() {
			inputStatValue = GetInputValue<int>("inputValue");
			if(inputStatValue > maxValue)
			{
				maxValue = inputStatValue;
			}
		}

		public override void OnCreateConnection(NodePort from, NodePort to) {
			GetNodeValues();
			Evaluate();
		}

		public override void OnRemoveConnection(NodePort port) {
			GetNodeValues();
			Evaluate();
		}

		private void CheckThresholds() {
			float q = (100f / (float)thresholdPercent);
			float m = ((float)maxValue / (float)inputStatValue);

			convertedMeetsValue = Mathf.RoundToInt(maxValue / q);
			evaluation = (q >= m);
		}

		public override void Evaluate() {
			var inputPort = GetInputPort("inputValue");
			if(inputPort.IsConnected)
			{
				GetNodeValues();
				CheckThresholds();
			} else
			{
				evaluation = false;
			}
		}

		public override object GetValue(NodePort port) {
			Evaluate();
			return evaluation;
		}


	}
}