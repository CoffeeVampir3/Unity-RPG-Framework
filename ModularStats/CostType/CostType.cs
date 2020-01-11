using System;
using System.Collections.Generic;
using UnityEngine;
using RPGFramework.PropertiesInternal;

namespace RPGFramework.Properties {

	[CreateAssetMenu(menuName = "ModularStats/CostType")]
	public class CostType : ScriptableObject {
		[SerializeField]
		internal List<CostAssociation> costStats = new List<CostAssociation>();

		public void PayCosts(StatBlock costStatBlock, StatBlock paysCostStatBlock) {
			CostApplier.Apply(costStatBlock, paysCostStatBlock, this);
		}

		public bool CanPayCosts(StatBlock costStatBlock, StatBlock paysCostStatBlock) {
			return CostApplier.CanPay(costStatBlock, paysCostStatBlock, this);
		}

		[SerializeField]
		[System.Serializable]
		internal class CostAssociation {
			public ModularStat cost = null;
			public ModularStat payedWith = null;
		}

		[SerializeField]
		internal CostModes costMode = CostModes.PayEachPair;

		internal enum CostModes {
			PayEachPair
		}
	}
}
