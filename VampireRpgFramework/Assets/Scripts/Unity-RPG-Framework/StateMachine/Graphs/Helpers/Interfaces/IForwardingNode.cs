using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace RPGFramework.Helpers {
	public interface IForwardingNode {
		NodePort GetForwardingPort();
	}
}
