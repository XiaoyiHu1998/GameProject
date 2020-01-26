using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ShopUIManagerScript : MonoBehaviour
{
    public RectTransform shopPanel;

    public Button openShopButton, closeShopButton, purchaseButton1;

    public float animationSpeed;

    //public Weapon bow;

    public PlayerInventory playerInventory;

    public void Awake()
    {
        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        purchaseButton1.onClick.AddListener(PurchaseArrows);

        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public void OpenShop()
    {
        shopPanel.DOAnchorPos(Vector2.zero, animationSpeed);
    }

    public void CloseShop()
    {
        shopPanel.DOAnchorPos(new Vector2(0, 600), animationSpeed);
    }

    public void PurchaseArrows()
    {
        //Debug.Log("test");
        playerInventory.buyWeapon(Weapon.Bow);
    }
}
