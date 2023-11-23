using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public Text currentCardText;
    public Text nextCardText;

    public CardStack cardStack;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (cardStack.cards.Count > 0)
        {
            // Display the current card
            currentCardText.text = "Current Card: " + cardStack.cards[0].power.ToString();
        }
        else
        {
            currentCardText.text = "No cards left";
        }

        if (cardStack.cards.Count > 1)
        {
            // Display the next card
            nextCardText.text = "Next Card: " + cardStack.cards[1].power.ToString();
        }
        else
        {
            nextCardText.text = "No next card";
        }
    }

    // Call this method when you add or use a card in the CardStack
    public void OnCardStackChanged()
    {
        UpdateUI();
    }
}