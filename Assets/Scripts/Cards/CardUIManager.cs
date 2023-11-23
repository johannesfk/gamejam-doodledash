using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public Card currentCard;
    public Card nextCard;

    public CardStack cardStack;

    private List<Card> cards;

    private void Awake()
    {
        cardStack = FindObjectOfType<CardStack>();
        cards = cardStack.cards;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentCard = cards[0];
        nextCard = cards[1];
    }

    public void OnCardStackChanged()
    {
        UpdateUI();
    }
}