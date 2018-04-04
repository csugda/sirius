using UnityEngine;

namespace Assets.Scripts.Experience
{
    class ExperienceDrop : MonoBehaviour
    {
        public int ammount;
        public void OnDestroy()
        {
            Events.XPDrop.Invoke(ammount);
        }
    }
}
