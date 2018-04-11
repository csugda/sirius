using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Actions
{
    public abstract class Action : MonoBehaviour
    {
        public Color useColor;
        public Vector3 colorDiff;
        private Vector3 colorRamp;
        private Vector3 white = new Vector3(1f, 1f, 1f);
        public Sprite image;
        public GameObject UI;
        private Image UiImage;
        public float cooldown;
        public float initialCooldown;
        public void Start()
        {
            if (initialCooldown <= 0)
            {
                initialCooldown = 0.1f;
            }
            UiImage = UI.GetComponent<Image>();
            Debug.Log(this.gameObject.name);
            Debug.Log(UiImage);
            colorDiff = new Vector3(useColor.r, useColor.g, useColor.b);
        }
        public virtual void DoAction()
        {
            colorRamp = colorDiff;
            cooldown = 0;
        }
        public Color temp;
        public void Update()
        {
            if (cooldown < initialCooldown)
            {
                
                temp = new Color(colorRamp.x, colorRamp.y, colorRamp.z);
                UiImage.color = temp;
                colorRamp = Vector3.Lerp(colorDiff, white, (cooldown / initialCooldown) );
                cooldown += Time.deltaTime;
                Debug.Log(colorRamp);
            }
        }
    }
}
