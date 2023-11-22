using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CardStyles : MonoBehaviour
{
    public Color dashColor;
    public Color doubleJumpColor;
    public Color wallJumpColor;
    public Color bounceColor;
    public Color teleportColor;
    [SerializeField]
    private Sprite dashSprite;
    [SerializeField]
    private Sprite doubleJumpSprite;
    [SerializeField]
    private Sprite wallJumpSprite;
    [SerializeField]
    private Sprite bounceSprite;
    [SerializeField]
    private Sprite teleportSprite;

    public Dictionary<PowerType, Color> cardColors;
    public Dictionary<PowerType, Sprite> cardSprites;

    private void Awake()
    {
        cardColors = new Dictionary<PowerType, Color>
        {
            [PowerType.Dash] = dashColor,
            [PowerType.DoubleJump] = doubleJumpColor,
            [PowerType.WallJump] = wallJumpColor,
            [PowerType.Bounce] = bounceColor,
            [PowerType.Teleport] = teleportColor
        };
        cardSprites = new Dictionary<PowerType, Sprite>
        {
            [PowerType.Dash] = dashSprite,
            [PowerType.DoubleJump] = doubleJumpSprite,
            [PowerType.WallJump] = wallJumpSprite,
            [PowerType.Bounce] = bounceSprite,
            [PowerType.Teleport] = teleportSprite
        };
    }
}
