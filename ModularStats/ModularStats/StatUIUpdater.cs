using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RT.Properties;

namespace RT.CardGame {
	public class StatUIUpdater : MonoBehaviour {
		[SerializeField]
		private ModularStat outputStat = null;
		private StatBlock stats;
		private TextMeshProUGUI tmproText;

		public void DoSomething() {
			StatBlock stats = this.GetComponentInParent<StatBlock>();
			//empty
		}

		private int lastValue = (int.MaxValue);
		private void Start() {
			stats = GetComponentInParent<StatBlock>();
			tmproText = GetComponentInChildren<TextMeshProUGUI>();

			Stat stat = null;
			if(stats.TryGetValue(outputStat, out stat))
			{
				tmproText.text = stat.ToString();
				lastValue = stat;
			}
		}

		public void Update() {
			Stat stat = null;
			if (stats.TryGetValue(outputStat, out stat))
			{
				if (stat != lastValue)
				{
					tmproText.text = stat.ToString();
					lastValue = stat;
				}
			}
		}

	}
}
