using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PrizeShuffler : MonoBehaviour, IPrizeSequence
{
    private List<int> prizeSequence = new();
    private int selectedPrize;
    private string prizeAmount;

    public List<int> GetPrizeSequence => prizeSequence;
    public int GetSelectedPrize => selectedPrize;
    public string GetPrizeAmount => prizeAmount;

    private bool hasWon;
    private int prizeCount = 12;

    public void ShufflePrizes(int selectedPrize)
    {
        this.selectedPrize = selectedPrize;

        if (selectedPrize == 0) prizeAmount = "$250";
        else if (selectedPrize == 1) prizeAmount = "$1,250";
        else if (selectedPrize == 2) prizeAmount = "$5,000";
        else if (selectedPrize == 3) prizeAmount = "20,000";

        hasWon = false;
        int rolledPrize;
        List<int> preWinPrizePool = new() {0, 1, 2, 3};
        List<int> postWinPrizePool = new() {0, 1, 2, 3};

        /*  Key represents the prize Id:
            mini = 0
            minor = 1
            major = 2
            maxi = 3

        Value represents number of that prize in the prize sequence
        */
        Dictionary<int, int> prizeTracker = new()
    {
        {0, 0},
        {1, 0},
        {2, 0},
        {3, 0}
    };

        if(prizeSequence.Count != 0) prizeSequence.Clear();

        for(int i = 0; i < prizeCount; i++)
        {
            if (!hasWon)
            {
                rolledPrize = preWinPrizePool[Random.Range(0, preWinPrizePool.Count)];

                if (!PrizeExists(rolledPrize, prizeTracker)) return;

                prizeSequence.Add(rolledPrize);
                prizeTracker[rolledPrize]++;

                if (prizeTracker[rolledPrize] == 3 && rolledPrize == selectedPrize)
                {
                    hasWon = true;
                    postWinPrizePool.Remove(rolledPrize);
                }
                else if (prizeTracker[rolledPrize] == 2 && rolledPrize != selectedPrize) preWinPrizePool.Remove(rolledPrize);
                

            }
            else
            {
                if(postWinPrizePool.Count == 1)
                {
                    prizeSequence.Add(postWinPrizePool[0]);
                    continue;
                }

                rolledPrize = postWinPrizePool[Random.Range(0, postWinPrizePool.Count)];

                if (!PrizeExists(rolledPrize, prizeTracker)) return;

                prizeSequence.Add(rolledPrize);
                prizeTracker[rolledPrize]++;

                if (prizeTracker[rolledPrize] == 3) postWinPrizePool.Remove(rolledPrize);
            }
        }
    }

    private bool PrizeExists(int rolledPrize, Dictionary<int, int> prizeTracker)
    {
        if (!prizeTracker.ContainsKey(rolledPrize))
        {
            Debug.Log("Prize does not exist in Dictionary, Prize Sequence incomplete");
            return false;
        }

        return true;
    }
}
