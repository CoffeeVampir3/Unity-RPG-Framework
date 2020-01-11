using System;
using System.Collections.Generic;
using UnityEngine;
using RPGFramework.PropertiesInternal;

namespace RPGFramework.Properties {
	[CreateAssetMenu(menuName = "ModularStats/DamageType")]
	public class DamageType : ScriptableObject {
		[SerializeField]
		internal List<ModularStat> damagesStats = new List<ModularStat>();

		public void ApplyDamage(StatBlock stats, int amount) {
			DamageApplier.Apply(stats, this, amount);
		}

		[SerializeField]
		internal DamageModes damageMode = DamageModes.FirstNonZero;

		internal enum DamageModes {
			FirstNonZero,
			FirstNonZeroSpillover,
			All
		}
	}
}
