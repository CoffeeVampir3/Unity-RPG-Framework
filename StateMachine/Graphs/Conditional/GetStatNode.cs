using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

/// <summary>
/// TODO:: Conditions
/// </summary>

namespace RPGFramework.CndGraph {
	public class GetStatNode : ConditionNode {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public int outputValue; //Variable hard reference by string name in MoveNext()


		private Stat statValue;
		public int currentValue = 0; //Display only.
		public ModularStat targetStat;
		private StatBlock statBlock;

		protected override void Init() {
			base.Init();

			Evaluate();
		}

		private void EvaluateStat() {
			if (targetStat == null)
				return;

			if (statBlock == null)
			{
				ConditionalGraph condGraph = graph as ConditionalGraph;
				if (condGraph.GetStateMachineOwner() == null)
					return;

				statBlock = condGraph.GetStateMachineOwner().GetComponent<StatBlock>();
				if (statBlock == null)
				{
					return;
				}
			}

			Stat retreivedStat = null;
			if (statBlock.TryGetValue(targetStat, out retreivedStat))
			{
				currentValue = retreivedStat.value;
				outputValue = retreivedStat.value;
				statValue = retreivedStat;
			}
		}

		public override void Evaluate() {
			EvaluateStat();
		}

		public override object GetValue(NodePort port) {
			Evaluate();
			if (statValue != null)
			{
				return statValue.value;
			}
			return int.MinValue;
		}
		
	}
}