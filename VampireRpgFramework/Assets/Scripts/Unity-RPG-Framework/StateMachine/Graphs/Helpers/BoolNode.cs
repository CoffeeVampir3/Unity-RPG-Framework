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
	public class BoolNode : Node {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public bool output;
		[SerializeField]
		private bool outputValue = false;

		public override object GetValue(NodePort port) {
			return outputValue;
		}

	}
}
