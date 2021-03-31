using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation
{
    public Sprite[] Frames { get; set; }

    public float Speed { get; set; }

    public bool Looping { get; set; }
}

public class AnimationManager : MonoBehaviour
{
    private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    public Animation curAnim;
    public bool isAnimating = false;
    [SerializeField] private SpriteRenderer spriteR;

    public void PopulateDictionary(string[] keys, Animation[] values)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            animations.Add(keys[i], values[i]);
        }
    }

    public void PlayAnimation(string name)
    {
        if (isAnimating)
        {
            StopCoroutine("AnimationInternal");
            isAnimating = false;
        }

        StartCoroutine("AnimationInternal", animations[name]);
    }

    public IEnumerator AnimationInternal(Animation anim)
    {
        isAnimating = true;
        curAnim = anim;
        for (int i = 0; i < anim.Frames.Length; i++)
        {
            spriteR.sprite = anim.Frames[i];
            yield return new WaitForSeconds(anim.Speed);
        }

        while (anim.Looping)
        {
            for (int i = 0; i < anim.Frames.Length; i++)
            {
                spriteR.sprite = anim.Frames[i];
                yield return new WaitForSeconds(anim.Speed);
            }
        }
        isAnimating = false;
    }
}
