using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using XNode;
using System.Diagnostics;
using XNodeEditor;

namespace RPGFramework {
	[System.AttributeUsage(AttributeTargets.Method)]
	public class Condition : System.Attribute { /* Empty */ }

	[AttributeUsage(AttributeTargets.Class)]
	public class TargetGraphAttribute : Attribute,
	XNodeEditor.Internal.NodeEditorBase<NodeGraphEditor, NodeGraphEditor.CustomNodeGraphEditorAttribute, XNode.NodeGraph>.INodeEditorAttrib {
		private Type inspectedType;
		public string editorPrefsKey;
		/// <summary> Tells a NodeGraphEditor which Graph type it is an editor for </summary>
		/// <param name="inspectedType">Type that this editor can edit</param>
		/// <param name="editorPrefsKey">Define unique key for unique layout settings instance</param>
		public TargetGraphAttribute(Type inspectedType) {
			this.inspectedType = inspectedType;
		}

		public Type GetInspectedType() {
			return inspectedType;
		}
	}

	//I want AttributeTarget <-> GraphEditor
	public static class GraphLink<C, A, T>	where C : BaseCondition //The base conditional class
											where A : TargetGraphAttribute //Graph target attribute
											where T : NodeGraphEditor //XNode graph editor
		{

		public static Dictionary<C, T> graphAssociations;
		public static List<Type> baseAssociations;
		public static string dbgLogString = "";

		private static void LinkCustomEditors() {
			graphAssociations = new Dictionary<C, T>();
			dbgLogString = "Wow!\n";

			Type[] baseConditions = typeof(C).GetDerivedTypes();
			dbgLogString += baseConditions.Length;
			for (int i = 0; i < baseConditions.Length; i++)
			{
				dbgLogString += baseConditions[i].ToString();

				if (baseConditions[i].IsAbstract) continue;
				var attribs = baseConditions[i].GetCustomAttributes(typeof(A), false);
				if (attribs == null || attribs.Length == 0) continue;
				dbgLogString += attribs.GetType();
				//A attrib = attribs[0] as A;
				//graphAssociations.Add(baseConditions[i] as C, attrib.GetInspectedType() as T);
				//dbgLogString += baseConditions[i].ToString() + attrib.GetInspectedType().ToString() + '\n';
			}
		}

		public static string GetDebug() {
			return dbgLogString;
		}

		public static void InitializeLinks() {
			LinkCustomEditors();
		}

		/*
		private static T GetEditorFor<target>() {
			T editor = null;
			if (graphAssociations == null)
				return;


		}
		*/
	}

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
