using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenAnimationOffset : MonoBehaviour
{
    [SerializeField] Animator[] tokenAnimators;
    private WaitForSeconds animOffsetTime;

    private void Start()
    {
        animOffsetTime = new WaitForSeconds(.075f);
        StartCoroutine(AnimationOffset());
    }

    private IEnumerator AnimationOffset()
    {
        int count = 0;

        while(count < tokenAnimators.Length)
        {
            tokenAnimators[count].SetTrigger("isRotating");
            count++;
            yield return animOffsetTime;
        }
    }
}
