using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RT.Properties;

namespace RT {
	public class BooleanExpressionNode : Node {
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public bool rhs;
		[Input(ShowBackingValue.Never, ConnectionType.Override)] public bool lhs;
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public bool result;
		public bool currentResult = false;

		public ExpressionOperation operation;

		public override void OnCreateConnection(NodePort from, NodePort to) {
			CalculateResult();
		}

		public override void OnRemoveConnection(NodePort port) {
			CalculateResult();
		}

		private void CalculateResult() {
			rhs = GetInputValue<bool>("rhs");
			lhs = GetInputValue<bool>("lhs");
			switch (operation)
			{
				case ExpressionOperation.And:
					currentResult = rhs & lhs;
					break;
				case ExpressionOperation.Or:
					currentResult = rhs | lhs;
					break;
			}
		}

		public override object GetValue(NodePort port) {
			CalculateResult();
			return currentResult;
		}

		public enum ExpressionOperation {
			And,
			Or
		}
	}
}
