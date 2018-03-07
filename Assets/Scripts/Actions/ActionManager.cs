using UnityEngine;
using System;
namespace Assets.Scripts.Actions
{
    public class ActionManager : MonoBehaviour
    {
        public Action[] actions;

        public void ExecuteAction(int action)
        {
            if (CanExecuteAction(action))
            {
                actions[action].DoAction();
            }
        }
        public bool CanExecuteAction(int action)
        {
            if (action < 0 || action > 3)
                throw new Exception("Action must be between 1 and 4");
            if (actions[action] == null)
                return false;
            return (actions[action].cooldown <= 0);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                ExecuteAction(0);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ExecuteAction(1);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                ExecuteAction(2);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExecuteAction(3);
            }
        }
    }
}
