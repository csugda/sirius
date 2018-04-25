using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    class ActionDash : Action
    {
        //class variables
    


        public static PlayerController1 instance;
        public float dashSpeed;
        public float maxDuration;
        private float currentDuration;
        private float originalSpeed;

        private new void Start()
        {
            base.Start();
            originalSpeed = this.GetComponent<PlayerController1>().speed;


        }

        public override void DoAction()
        {
            base.DoAction();


            Debug.Log("Original Speed: " + this.GetComponent<PlayerController1>().speed);

            this.GetComponent<PlayerController1>().speed = dashSpeed;
            currentDuration = maxDuration;

            
            Debug.Log("Dash Speed: " + dashSpeed);


        }

        public new void Update()
        {
            base.Update();
            //Debug.Log("DURATION: " + base.cooldown);
            if (base.cooldown >= base.initialCooldown)
            {
                //if (currentDuration > 0)
                //{

                //    currentDuration--;
                //}
                //else
                //{
                //    
                //}

                this.GetComponent<PlayerController1>().speed = originalSpeed;
            }
        }
    }
}

