using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrizeSequence
{
    public List<int> GetPrizeSequence { get; }

    public int GetSelectedPrize {  get; }

    public string GetPrizeAmount { get; }

    public void ShufflePrizes(int selectedPrize);
}
