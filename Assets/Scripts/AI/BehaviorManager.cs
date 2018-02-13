using Assets.Scripts.AI.Components;
using Assets.Scripts.AI.Decorators;
using Assets.Scripts.AI.Nodes;
using Assets.Scripts.AI.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class BehaviorManager : MonoBehaviour
    {
        /// <summary>
        /// Primary Runner for this manager. 
        /// Runs all sub-behaviors/trees at the same time using the specified parallelrunner attributes.
        /// </summary>
        public ParallelRunner Runner = new ParallelRunner("Main Root", -1, 0);

        public List<TreeElement> BehaviorTreeList;

        public float SecondsBetweenTicks = 0.1f;

        public int TimesToTick = 10;

        private bool initialized = false;

        void OnEnable()
        {
            Init();
        }

        void OnStart()
        {
            initialized = true;
        }

        
        public void Init()
        {
            if(initialized == false)
            {

                initialized = LoadTree(); ;
            }
        }

        //TODO: Add ILogger *(perhaps Observer pattern? This is our "singleton")*
        //Dispatch messages to observed classes and receive that information here...
        //How to store? List? Dictionary? My face? Cat Pictures?

        /// <summary>
        /// Ticks on the aggregate ParallelRunner then continues ticking for as long as the runner is in running state
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            WaitForSeconds wfs = new WaitForSeconds(SecondsBetweenTicks);

            Debug.Log("Starting ticks on Runner: \n\t" + Runner.ToString());
            yield return Runner.Tick();
            while (Runner.CurrentState == BehaviorState.Running || TimesToTick > 0)
            {
                yield return StartCoroutine(Runner.Tick(wfs));
                --TimesToTick;
            }

            Debug.Log("All Coroutines Should be DONE now! Ending all to make sure....");
            StopAllCoroutines();
        }


        public TreeModel<TreeElement> GetTreeModel()
        {
            return new TreeModel<TreeElement>(BehaviorTreeList);
        }

        bool LoadTree()
        {
            //This will act as the treeModel's root element. It will be hidden in the treeview.
            Runner = new ParallelRunner("Main Root", -1, 0)
            {
                BehaviorTreeManager = this
            };

            var selector = new Selector("Selector 1", 0, 1);
            var debugNode = new DebugOutNode("parallel", 0, 5);
            var meowNode = new DebugOutNode("meow", 1, 2);
            var inverter = new Inverter("inverter", 1, 3);
            var invertedNode = new DebugOutNode("invertedShouldFail", 2, 4);

            Runner.AddChild(selector);
            selector.AddChild(inverter);
            inverter.SetChild(invertedNode);

            selector.AddChild(meowNode);

            Runner.AddChild(debugNode);
            return true;
        }

        void SaveTree(string filepath)
        {
        }
    }
}
