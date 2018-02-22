using UnityEngine;

namespace Assets.Scripts.Actions
{
    public abstract class Action : MonoBehaviour
    {
        public Sprite image;
        public float cooldown;
        public float initialCooldown;

        public virtual void DoAction()
        {
            cooldown = initialCooldown;
        }

        public void Update()
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
    }
}
