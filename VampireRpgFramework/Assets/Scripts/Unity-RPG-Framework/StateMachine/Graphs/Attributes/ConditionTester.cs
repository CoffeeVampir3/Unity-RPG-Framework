using System;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;
using RPGFramework.CndGraph;

namespace RPGFramework {
	public class ConditionTester : MonoBehaviour {
		public void Start() {
			Moose m = new Moose();

			m.DoThing();
		}

		
		[TargetGraph(typeof(ConditionalGraph))]
		public class Moose : BaseCondition {
			public void DoThing() {
				Debug.Log(this.createdNodes.Count);
			}

			[Condition]
			public bool Eval(int a, int b) {
				return true;
			}

			/*This should become:
			 * 
			 * public class EvalNode {
			 *		[Input] int a;
			 *		[Input] int b;
			 *		[Output] bool ret;
			 *	}
			 * 
			 * */

			[Condition]
			public bool KillTeacher(bool c, int f, float w) {
				return true;
			}

			/*This should become:
			* 
			* public class KillTeacherNode {
			*		[Input] bool c;
			*		[Input] int f;
			*		[Input] float w;
			*		[Output] bool ret;
			*	}
			* 
			* */
		}
	}
}
