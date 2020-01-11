using System;
using System.Collections.Generic;
using UnityEngine;
using RT.Properties;

namespace RT.PropertiesInternal {

	/// <summary>
	/// This is essentially a helper class to allow us to easily coupled all the components involved in damaging together at a central
	/// point.
	/// </summary>
	internal static class DamageApplier {

		static void ApplyFirstNonZero(StatBlock stats, DamageType damageType, int amount) {
			for(int i = 0; i < damageType.damagesStats.Count; i++)
			{
				Stat stat = null;
				if (stats.TryGetValue(damageType.damagesStats[i], out stat))
				{
					if (stat.value > 0)
					{
						stat.value = Mathf.Clamp(stat - amount, 0, int.MaxValue);
						return;
					}
				}
			}
		}

		static void ApplyFirstNonZeroSpillover(StatBlock stats, DamageType damageType, int amount) {
			int newAmount = amount;
			for (int i = 0; i < damageType.damagesStats.Count; i++)
			{
				Debug.Log("yeet");
				Stat stat = null;
				if (stats.TryGetValue(damageType.damagesStats[i], out stat))
				{
					if (stat.value - newAmount >= 0)
					{
						stat.value -= newAmount;
						return;
					} else
					{
						newAmount -= stat.value;
						stat.value = 0;
					}
				}
			}
		}

		static void ApplyAll(StatBlock stats, DamageType damageType, int amount) {
			for (int i = 0; i < damageType.damagesStats.Count; i++)
			{
				Stat stat = null;
				if (stats.TryGetValue(damageType.damagesStats[i], out stat))
				{
					if (stat.value > 0)
					{
						stat.value = Mathf.Clamp(stat - amount, 0, int.MaxValue);
					}
				}
			}
		}

		internal static void Apply(StatBlock stats, DamageType damageType, int amount) {
			DamageType.DamageModes mode = damageType.damageMode;

			switch(mode)
			{
				case DamageType.DamageModes.FirstNonZero:
					ApplyFirstNonZero(stats, damageType, amount);
					break;
				case DamageType.DamageModes.FirstNonZeroSpillover:
					ApplyFirstNonZeroSpillover(stats, damageType, amount);
					break;
				case DamageType.DamageModes.All:
					ApplyAll(stats, damageType, amount);
					break;
				default:
					return;
			}
		}

	}
}
