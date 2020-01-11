using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework.Properties {
	[Serializable]
	public class Stat {
		public int value;

		public Stat() {
			//Empty
		}

		public Stat(int newVal) {
			this.value = newVal;
		}

		#region Sugar

		public override string ToString() {
			return value.ToString();
		}

		public static implicit operator int(Stat stat) => stat.value;

		#endregion

	}
}