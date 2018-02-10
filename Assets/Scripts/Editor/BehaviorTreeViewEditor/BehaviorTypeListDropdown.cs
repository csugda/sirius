using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace BehaviorTreeViewEditor
{
    public class BehaviorTypeListDropdown
    {
        private GenericMenu Menu = new GenericMenu();

        public string TypeString = "";

        public GenericMenu GetMenu()
        {
            Menu = new GenericMenu();
            foreach (var elType in GetEnumerableOfType<BehaviorTreeElement>())
            {
                Menu.AddItem(new GUIContent(elType.ToString()), TypeString == elType.ToString(), OnTypeSelected, elType.ToString());
            }

            return Menu;
        }

        private void OnTypeSelected(object typeName)
        {
            TypeString = (string)typeName;
        }

        public static IEnumerable<string> GetEnumerableOfType<T>() where T : class
        {
            List<string> objects = new List<string>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add(type.Name);
            }
            return objects;
        }
    }
}
