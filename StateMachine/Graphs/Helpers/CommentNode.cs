using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using RPGFramework.Properties;
using RPGFramework.CndGraph;

namespace RPGFramework.Helpers {
	public class CommentNode : Node {
		[TextArea(5, 30)]
		[SerializeField]
		private string comment;
	}
}
