using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using XNode;
using UnityEngine;

/// <summary>
/// For debugging, primarily.
/// </summary>

namespace RPGFramework.Helpers {
	public class RandomIntNode : Node {
		[Output(ShowBackingValue.Never, ConnectionType.Multiple)] public int output;
		[SerializeField]
		private int lastValue = 0;
		[SerializeField]
		private int inclusiveMin = 0; //Inclusive
		[SerializeField]
		private int inclusiveMax = 0; //Inclusive

		private static RNGCryptoServiceProvider randomIntService = new RNGCryptoServiceProvider();
		// Return a random integer between a min and max value.
		private int RandomInteger(int min, int max) {
			uint scale = uint.MaxValue;
			while (scale == uint.MaxValue)
			{
				// Get four random bytes.
				byte[] four_bytes = new byte[4];
				randomIntService.GetBytes(four_bytes);

				// Convert that into an uint.
				scale = BitConverter.ToUInt32(four_bytes, 0);
			}

			// Add min to the scaled difference between max and min.
			return (int)(min + (max - min) *
				(scale / (double)uint.MaxValue));
		}


		public override object GetValue(NodePort port) {
			lastValue = RandomInteger(inclusiveMin, inclusiveMax+1);
			return lastValue;
		}

	}
}
