using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    [SerializeField] private Animator tokenAnimator;
    [SerializeField] private Animator tokenGameObjectAnimator;
    [SerializeField] private GameObject token;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button tokenButton;

    [SerializeField] private Sprite[] idlePrizes;

    private bool isSelected = false;
    private int tokenPrize;
    public bool Selected => isSelected;
    public int GetTokenPrize => tokenPrize;

    private void Start()
    {
        var anims = GetComponentsInChildren<Animator>().ToList();
        tokenAnimator = anims.First(x => x.runtimeAnimatorController.name == "Token");
    }

    public void TokenSelect(int tokenValue)
    {
        isSelected = true;

        switch (tokenValue)
        {
            case 0:
                tokenPrize = 0;
                tokenAnimator.SetTrigger("isRevealingMini"); 
            break;

            case 1:
                tokenPrize = 1;
                tokenAnimator.SetTrigger("isRevealingMinor");
            break;

            case 2:
                tokenPrize = 2;
                tokenAnimator.SetTrigger("isRevealingMajor");
            break;

            case 3:
                tokenPrize = 3;
                tokenAnimator.SetTrigger("isRevealingMaxi");
            break;
        }
    }

    public void AnimateWinner()
    {
        tokenGameObjectAnimator.SetTrigger("isWinner");
    }

    public void SetTokenInactive(int tokenValue)
    {
        buttonImage.color = new Color32(120, 120, 120, 255);

        TokenSelect(tokenValue);
    }
}
