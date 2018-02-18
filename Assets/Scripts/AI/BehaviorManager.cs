using Assets.Scripts.AI;
using Assets.Scripts.AI.Components;
using Assets.Scripts.AI.Decorators;
using Assets.Scripts.AI.Nodes;
using Assets.Scripts.AI.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        public List<BehaviorTreeElement> BehaviorTreeList;

        public BehaviorTreeAsset BehaviorTree;

        public float SecondsBetweenTicks = 0.1f;

        public int TimesToTick = 10;

        private bool initialized = false;

        void OnEnable()
        {
            Init();
        }

        void OnStart()
        {
            Init();
        }     

        public void Init()
        {
            if(initialized == false)
            {
                initialized = LoadTree();
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

        public bool LoadTree()
        {
            Runner = new ParallelRunner("Main Root", -1, -1)
            {
                BehaviorTreeManager = this
            };
            if (BehaviorTree != null)
            {
                var newList = new List<BehaviorTreeElement>();
                foreach(var behavior in BehaviorTree.treeElements)
                {
                    var newBehavior = Activator.CreateInstance(Type.GetType(behavior.ElementType), 
                                                               behavior.name, behavior.depth, behavior.id);

                    if (((BehaviorTreeElement)newBehavior).depth == -1 || ((BehaviorTreeElement)newBehavior).name == "root") continue;

                    ((BehaviorTreeElement)newBehavior).BehaviorTreeManager = this;

                    newList.Add(newBehavior as BehaviorTreeElement);
                }
                newList.Insert(0, Runner);
                Runner = TreeElementUtility.ListToTree(newList) as ParallelRunner;
                Debug.Log(Runner);
                //LOAD TREE
                return true;
            }
            else
            {
                //TEST TREE
                //This will act as the treeModel's root element. It will be hidden in the treeview.
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
            }

            return true;
        }
    }
}
