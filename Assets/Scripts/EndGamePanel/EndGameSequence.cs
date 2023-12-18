using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameSequence : MonoBehaviour
{
    private ISceneLoader sceneLoader;
    private IPrizeSequence prizeShuffler;
    [SerializeField] private TextMeshProUGUI prizeAmount;

    [SerializeField] private Animator prizeTextAnim;
    [SerializeField] private Animator victoryBannerAnim;
    [SerializeField] private Animator canvasAnim;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<ISceneLoader>();
        prizeShuffler = FindObjectOfType<PrizeShuffler>().GetComponent<IPrizeSequence>();
        prizeAmount.text = prizeShuffler.GetPrizeAmount;
    }

    public void LoadMenu()
    {
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        prizeTextAnim.SetTrigger("isFadingOut");
        victoryBannerAnim.SetTrigger("isFadingOut");
        yield return new WaitForSeconds(1);

        canvasAnim.SetTrigger("isFadingOut");
        yield return new WaitForSeconds(1);

        sceneLoader.LoadLevel(0);
    }
}
