using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGFramework.Properties {
	[Serializable]
	public class StatBlock : MonoBehaviour {
		[SerializeField]
		private List<StatInitializer> unitStats = new List<StatInitializer>();
		private List<Stat> statsActual = new List<Stat>();
		private Dictionary<ModularStat, Stat> originalToStatDictionary = new Dictionary<ModularStat, Stat>();

		#region LazyInitialization

		private void Awake() {
			LazyInit();
		}

		private void Init() {
			originalToStatDictionary.Clear();
			for (int i = 0; i < unitStats.Count; i++)
			{
				StatInitializer currentStat = unitStats[i];
				currentStat.stat = new Stat(currentStat.startingValue);

				statsActual.Add(currentStat.stat);
				originalToStatDictionary.Add(currentStat.originalStat, currentStat.stat);
			}
		}

		/// <summary>
		/// Allows us to set the stat block as copiable, we copy the currrent stat values so
		/// our copy is initialized with them when it's created.
		/// </summary>
		public void MakeCopiable() {
			for (int i = 0; i < unitStats.Count; i++)
			{
				StatInitializer currentStat = unitStats[i];
				currentStat.startingValue = statsActual[i].value;
			}
		}

		[System.NonSerialized]
		bool initialized = false;
		private void LazyInit() {
			if (!initialized)
				Init();
			initialized = true;
		}

		#endregion

		#region DictionaryIndexing

		public bool TryGetValue(ModularStat originalStat, out Stat statWrapper) {
			LazyInit();
			return originalToStatDictionary.TryGetValue(originalStat, out statWrapper);
		}

		public Stat this[ModularStat originalStat] {
			get
			{
				Stat outStat = null;
				if (originalToStatDictionary.TryGetValue(originalStat, out outStat))
				{
					return outStat;
				}
				else
				{
					Debug.LogError("Could not find stat by name + " + originalStat.name);
				}
				return null;
			}
			set
			{
				Stat outStat = null;
				if (originalToStatDictionary.TryGetValue(originalStat, out outStat))
				{
					outStat.value = value;
				}
				else
				{
					Debug.LogError("Could not find stat by name + " + originalStat.name);
				}
			}
		}

		#endregion

		#region Foreach

		public void ForeachStat(Action<Stat> statAction) {
			LazyInit();
			for (int i = 0; i < statsActual.Count; i++)
			{
				statAction.Invoke(statsActual[i]);
			}
		}

		public void ForeachStat<T1>(Action<Stat, T1> statAction, T1 arg1) {
			LazyInit();
			for (int i = 0; i < statsActual.Count; i++)
			{
				statAction.Invoke(statsActual[i], arg1);
			}
		}

		public void ForeachStat<T1, T2>(Action<Stat, T1, T2> statAction, T1 arg1, T2 arg2) {
			LazyInit();
			for (int i = 0; i < statsActual.Count; i++)
			{
				statAction.Invoke(statsActual[i], arg1, arg2);
			}
		}

		public void ForeachStat<T1, T2, T3>(Action<Stat, T1, T2, T3> statAction, T1 arg1, T2 arg2, T3 arg3) {
			LazyInit();
			for (int i = 0; i < statsActual.Count; i++)
			{
				statAction.Invoke(statsActual[i], arg1, arg2, arg3);
			}
		}

		#endregion

	}
}