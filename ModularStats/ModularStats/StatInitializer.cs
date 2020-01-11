using System;
using System.Collections.Generic;
using UnityEngine;

namespace RT.Properties {
	[Serializable]
	public class StatInitializer {
		public ModularStat originalStat;
		public int startingValue;

		public Stat stat;
	}
}
