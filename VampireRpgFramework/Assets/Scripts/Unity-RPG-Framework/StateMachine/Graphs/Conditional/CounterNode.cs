using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;
using UnityEngine;

/// <summary>
/// For debugging, primarily.
/// </summary>

namespace RPGFramework.CndGraph {
	public class CounterNode : ConditionNode {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public int output;
		[SerializeField]
		private int count = 0;
		[SerializeField]
		private int resetAt = 10;

		public override void Evaluate() {
			count++;
		}

		public override object GetValue(NodePort port) {
			Evaluate();
			bool temp = false;
			if(count >= resetAt)
			{
				count = 0;
				temp = true;
			}
			return temp;
		}

	}
}
