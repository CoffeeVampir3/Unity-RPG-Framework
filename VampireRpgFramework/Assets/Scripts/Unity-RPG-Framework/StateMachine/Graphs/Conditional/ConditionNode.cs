using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework.CndGraph {
	public abstract class ConditionNode : XNode.Node {
		/// <summary>
		/// Updates the condition node.
		/// </summary>
		public abstract void Evaluate();
	}
}
