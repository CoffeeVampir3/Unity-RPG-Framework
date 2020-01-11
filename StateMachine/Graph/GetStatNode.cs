using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;

/// <summary>
/// TODO:: Conditions
/// </summary>

namespace RPGFramework {
	public class GetStatNode : Node {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public int outputValue; //Variable hard reference by string name in MoveNext()
		private Stat statValue;
		public int currentValue = 0; //Display only.
		public ModularStat targetStat;
		private StatBlock statBlock;

		protected override void Init() {
			base.Init();

			GetStat();
		}

		public Stat GetStat() {
			if (statBlock == null)
			{
				StateMachineGraph fsmGraph = graph as StateMachineGraph;
				if (fsmGraph.GetStateMachineOwner() == null)
					return null;

				statBlock = fsmGraph.GetStateMachineOwner().GetComponent<StatBlock>();
				if (statBlock == null)
				{
					return null;
				}
			}

			Stat retreivedStat = null;
			if (statBlock.TryGetValue(targetStat, out retreivedStat))
			{
				currentValue = retreivedStat.value;
				outputValue = retreivedStat.value;
				statValue = retreivedStat;
			}
			return null;
		}

		public override object GetValue(NodePort port) {
			GetStat();
			if (statValue != null)
			{
				return statValue.value;
			}
			return int.MinValue;
		}
		
	}
}