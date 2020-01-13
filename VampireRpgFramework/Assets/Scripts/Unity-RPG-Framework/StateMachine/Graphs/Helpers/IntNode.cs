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

namespace RPGFramework.Helpers {
	public class IntNode : Node {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public int output;
		[SerializeField]
		private int outputValue = 0;

		public override object GetValue(NodePort port) {
			return outputValue;
		}

	}
}
