using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    private IPrizeSequence prizeShuffler;
    [SerializeField] private TextMeshProUGUI prizeAmount;

    private void Start()
    {
        prizeShuffler = FindObjectOfType<PrizeShuffler>().GetComponent<IPrizeSequence>();
        prizeAmount.text = prizeShuffler.GetPrizeAmount;
    }
}
