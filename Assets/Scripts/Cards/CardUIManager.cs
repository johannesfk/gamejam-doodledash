using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public Image currentCardImage;
    public Image nextCardImage;

    public CardStack cardStack;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {

    }

    public void OnCardStackChanged()
    {
        UpdateUI();
    }
}