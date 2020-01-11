using System;
using System.Collections.Generic;
using UnityEngine;
using RT.Properties;

namespace RT.PropertiesInternal {

	/// <summary>
	/// This is essentially a helper class to allow us to easily coupled all the components involved in damaging together at a central
	/// point.
	/// </summary>
	internal static class CostApplier {

		static bool CostModeAll(StatBlock costStatBlock, StatBlock paysCostStatBlock, CostType costType, bool apply = false) {
			bool retVal = true;
			for (int i = 0; i < costType.costStats.Count; i++)
			{
				CostType.CostAssociation costAssoc = costType.costStats[i];
				Stat costStat = null;
				Stat payStat = null;

				if (!costStatBlock.TryGetValue(costAssoc.cost, out costStat))
				{
					Debug.LogError("No cost stat associated with cost for obj" + costStatBlock.gameObject.name);
					return false;
				}

				if (!paysCostStatBlock.TryGetValue(costAssoc.payedWith, out payStat))
				{
					Debug.LogError("No cost stat associated with cost for obj" + paysCostStatBlock.gameObject.name);
					return false;
				}

				if (costStat > payStat)
				{
					if(!apply)
					{
						//Our cost is more expensive than we can afford and we're not applying it
						retVal = false;
					}
				}
				else
				{
					//We can afford our cost and we're applying it
					if(apply)
					{
						payStat.value -= costStat;
					}
				}
			}
			return retVal;
		}

		internal static bool CanPay(StatBlock costStatBlock, StatBlock paysCostStatBlock, CostType costType) {
			CostType.CostModes mode = costType.costMode;

			switch (mode)
			{
				case CostType.CostModes.PayEachPair:
					return CostModeAll(costStatBlock, paysCostStatBlock, costType, false);
				default:
					return false;
			}
		}

		internal static void Apply(StatBlock costStatBlock, StatBlock paysCostStatBlock, CostType costType) {
			CostType.CostModes mode = costType.costMode;

			switch (mode)
			{
				case CostType.CostModes.PayEachPair:
					CostModeAll(costStatBlock, paysCostStatBlock, costType, true);
					break;
				default:
					return;
			}
		}

	}
}
