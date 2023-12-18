using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    private IPrizeSequence prizeShuffler;
    private int selectedPrize;
    private List<int> prizeSequence = new();

    private int sequenceIndex = 0;
    private int winningTokens = 0;

    [SerializeField] private List<Token> tokens;
    [SerializeField] private List<Button> tokenButtons;

    [SerializeField] private AnimationClip revealClip;

    public void AddTokenToList(Token token) => tokens.Add(token);

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject idlePanel;

    [SerializeField] private TextMeshProUGUI timerText;
    private float maxTimer = 10f;
    private float idleTimer;
    private float autoSelectTimer;
    private bool isIdle = false;

    void Start()
    {
        prizeShuffler = FindObjectOfType<PrizeShuffler>().GetComponent<IPrizeSequence>();
        prizeSequence = prizeShuffler.GetPrizeSequence;
        selectedPrize = prizeShuffler.GetSelectedPrize;

        idleTimer = maxTimer;
        autoSelectTimer = maxTimer;
    }

    private void Update()
    {
        if(Input.anyKeyDown) ResetIdleTimers();

        if (!isIdle)
        {
            if (idleTimer > 0) idleTimer -= Time.deltaTime;
            else
            {
                isIdle = true;
                idlePanel.SetActive(true);
            }
        }
        else if(autoSelectTimer > 0)
        {
            autoSelectTimer -= Time.deltaTime;
            timerText.text = Mathf.CeilToInt(autoSelectTimer).ToString();

            if (autoSelectTimer < 0)
            {
                SelectRandomToken();
                autoSelectTimer = maxTimer;
            }
        }
    }

    public void SelectToken(Token token)
    {
        if (token.Selected)
        {
            Debug.Log("Token already selected");
            return;
        }

        StartCoroutine(DisableButtonsDuringTokenReveal());

        token.TokenSelect(prizeSequence[sequenceIndex]);

        if (prizeSequence[sequenceIndex] == selectedPrize) winningTokens++;
        sequenceIndex++;

        if(winningTokens == 3) RevealEndGamePanel();
    }

    public void SelectRandomToken()
    {
        var unselectedTokens = tokens.FindAll(t => !t.Selected);

        if(winningTokens == 3)
        {
            victoryPanel.GetComponent<Button>().onClick.Invoke();
            return;
        }

        SelectToken(unselectedTokens[Random.Range(0, unselectedTokens.Count)]);
    }

    private void RevealEndGamePanel()
    {
        DisableTokenButtons();
        if(idlePanel.activeSelf) idlePanel.SetActive(false);

        StartCoroutine(EndSequence());
    }

    private void EnableTokenButtons()
    {
        foreach (var button in tokenButtons)
        {
            button.interactable = true;
        }
    }

    private void DisableTokenButtons()
    {
        foreach (var button in tokenButtons)
        {
            button.interactable = false;
        }
    }

    private void ResetIdleTimers()
    {
        isIdle = false;
        idleTimer = maxTimer;
        autoSelectTimer = maxTimer;
        if(idlePanel.activeSelf) idlePanel.SetActive(false);
        timerText.text = Mathf.CeilToInt(autoSelectTimer).ToString();
    }

    private IEnumerator DisableButtonsDuringTokenReveal()
    {
        DisableTokenButtons();
        yield return new WaitForSeconds(revealClip.length);

        EnableTokenButtons();
    }

    private IEnumerator EndSequence()
    {
        foreach(Token token in tokens)
        {
            if (!token.Selected)
            {
                token.SetTokenInactive(prizeSequence[sequenceIndex]);
                sequenceIndex++;
            }
            else if (token.GetTokenPrize == selectedPrize) token.AnimateWinner();
        }
        yield return new WaitForSeconds(1.5f);

        victoryPanel.SetActive(true);
    }
}
