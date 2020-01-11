using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

namespace RPGFramework {
	public class StatThresholdConditionNode : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public int inputValue;
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public bool evaluation;
		public int inputDisplayStatValue; //Display only.
		public int threshold; //percentage of maxValue
		[SerializeField]
		[HideInInspector]
		private int maxValue; //Set once on initialization... Never updates if the max changes.

		protected override void Init() {
			base.Init();

			NodePort fromNode = GetInputPort("inputValue");
			if(fromNode.IsConnected )
			{
				InitializeNode();
			}
		}

		private void InitializeNode() {
			inputDisplayStatValue = GetInputValue<int>("inputValue");
			maxValue = inputDisplayStatValue;
		}

		public override void OnCreateConnection(NodePort from, NodePort to) {
			InitializeNode();
		}

		private void EvaluateCondition() {
			float q = (100f / (float)threshold);
			float m = ((float)maxValue / (float)inputDisplayStatValue);
			evaluation = (q >= m);
		}

		public void Condition() {
			var inputPort = GetInputPort("inputValue");
			if(inputPort.IsConnected)
			{
				inputDisplayStatValue = inputPort.GetInputValue<int>();
				EvaluateCondition();
			}
		}

		public override object GetValue(NodePort port) {
			Condition();
			return evaluation;
		}


	}
}