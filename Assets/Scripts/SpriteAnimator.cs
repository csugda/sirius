using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] walkCycle;
    public Sprite[] restingCycle;
    public float frameTime;
    private SpriteRenderer rend;
    // Use this for initialization
    void Start()
    {
        rend = this.gameObject.GetComponent<SpriteRenderer>();
        rend.sprite = walkCycle[0];
    }
    private int frame = 0;
    private float time = 0;
    // Update is called once per frame
    void Update()
    {
        if (time >= frameTime)
        {
            time = 0;
            

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                frame = frame >= walkCycle.Length - 1 ? 0 : frame + 1;
                rend.sprite = walkCycle[frame];
            }
            else
            {
                frame = frame >= restingCycle.Length - 1 ? 0 : frame + 1;
                rend.sprite = restingCycle[frame];
            }
        }
        
        time += Time.deltaTime;
    }
}
