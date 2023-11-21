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

    public Dictionary<PowerType, Color> cardColors;

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
    }
}
