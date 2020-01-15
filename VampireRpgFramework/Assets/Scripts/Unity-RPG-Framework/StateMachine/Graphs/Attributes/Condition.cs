using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using XNode;
using System.Diagnostics;
using XNodeEditor;

namespace RPGFramework {
	[System.AttributeUsage(AttributeTargets.Method)]
	public class Condition : System.Attribute { /* Empty */ }

	public abstract class BaseCondition {
		bool result = false;
		protected List<Node> createdNodes = new List<Node>();

		public BaseCondition(){
			result = false;

			var conditionMethods = ReflectionGetMethodList(this);
			for (int i = 0; i < conditionMethods.Count; i++)
			{
				createdNodes.Add(CreateNodeFromMethod(conditionMethods[i] as MethodInfo));
			}

			UnityEditor.GenericMenu menu = new UnityEditor.GenericMenu();
			for (int i = 0; i < createdNodes.Count; i++) {
				menu.AddCustomContextMenuItems("yay");

			}

			//nodeGraphEditor.AddContextMenuItems(menu);
		}

		public static List<MethodBase> ReflectionGetMethodList(BaseCondition cond) {
			Type t = cond.GetType();
			var classMethods = t.GetMethods();

			List<MethodBase> methods = new List<MethodBase>();
			for (int i = 0; i < classMethods.Length; i++)
			{

				Attribute[] attribs = classMethods[i].GetCustomAttributes(true) as Attribute[];
				for (int j = 0; j < attribs.Length; j++)
				{
					Attribute curAtr = attribs[j];
					if (curAtr is Condition)
					{
						methods.Add(classMethods[i]);
					}
				}
			}

			return methods;
		}

		public static Node CreateNodeFromMethod(MethodInfo m) {
			Node newNode = Node.CreateInstance(typeof(BaseNode)) as Node;

			ParameterInfo[] parameters = m.GetParameters();

			for (int i = 0; i < parameters.Length; i++)
			{
				Type paramType = parameters[i].ParameterType;

				newNode.AddDynamicInput(paramType, Node.ConnectionType.Override, Node.TypeConstraint.None);
			}

			Type returnType = m.ReturnType;
			newNode.AddDynamicOutput(returnType, Node.ConnectionType.Multiple, Node.TypeConstraint.None);
			return newNode;
		}

		public class BaseNode : Node { /* empTY */ }

	}
}
