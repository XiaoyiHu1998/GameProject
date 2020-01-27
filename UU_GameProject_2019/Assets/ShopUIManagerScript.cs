using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopUIManagerScript : MonoBehaviour
{
    public RectTransform shopPanel;

    public Button openShopButton, closeShopButton, purchaseButton1, purchaseButton2;

    public float animationSpeed;

    //public Weapon bow;

    private PlayerInventory playerInventory;

    public void Awake()
    {
        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        purchaseButton1.onClick.AddListener(PurchaseArrows);
        purchaseButton2.onClick.AddListener(PurchaseBombs);
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
        Debug.Log("button test");
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.buyWeapon(Weapon.Bow);
    }

    public void PurchaseBombs()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.buyWeapon(Weapon.Bombs);
    }
}
