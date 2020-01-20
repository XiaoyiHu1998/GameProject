using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopUIManagerScript : MonoBehaviour
{
    public RectTransform shopPanel;

    public Button openShopButton, closeShopButton;

    public float animationSpeed;

    public void Awake()
    {
        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
    }

    public void OpenShop()
    {
        shopPanel.DOAnchorPos(Vector2.zero, animationSpeed);
    }

    public void CloseShop()
    {
        shopPanel.DOAnchorPos(new Vector2(0, 600), animationSpeed);
    }
}
