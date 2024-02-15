using System;
using System.Collections.Generic;
using ShooterGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Guns
{
    public string id;
    public bool purchased;
    public int cost;
}

public class GunSelection : MonoBehaviour
{
    public int currentGunIndex;
    public Guns[] availableGuns;
    
    [SerializeField] private GameObject[] guns;
    [SerializeField] private Animator[] gunsUI;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TMP_Text gunCost;
    [SerializeField] private TMP_Text[] coins;

    private void Start()
    {
        currentGunIndex = Bridge.GetInstance().thisPlayerInfo.data.saveData.currentGun;
        
        UpdateGun();
        SetInitialShip();
    }

    public void SaveWeaponData()
    {
        Bridge.GetInstance().thisPlayerInfo.data.saveData.currentGun = currentGunIndex;
        Bridge.GetInstance().SaveData();
    }

    private void SetInitialShip()
    {
        for (int i = 0; i < gunsUI.Length; i++)
        {
            if (i != currentGunIndex)
                gunsUI[i].Play("Hide");
            else
                gunsUI[i].Play("Show");
        }
    }
    
    public void UpdatePurchasedGuns()
    {
        var dataAssets = Bridge.GetInstance().thisPlayerInfo.data.assets;
        
        for (int i = 0; i < availableGuns.Length; i++)
        {
            for (int j = 0; j < dataAssets.Count; j++)
            {
                if (availableGuns[i].id == dataAssets[j].id)
                    availableGuns[i].purchased = true;
            }
        }
        
        CheckPurchase();
    }
    
    public void PurchaseGun()
    {
        Bridge.GetInstance().BuyGun(availableGuns[currentGunIndex].id);
        availableGuns[currentGunIndex].purchased = true;
        CheckPurchase();
        Bridge.GetInstance().thisPlayerInfo.coins -= availableGuns[currentGunIndex].cost;
        foreach (var coin in coins)
        {
            coin.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
        }
    }
    
    public void Up()
    {
        if (currentGunIndex > 0)
        {
            gunsUI[currentGunIndex].Play("DeselectUp", -1, 0f);
            gunsUI[currentGunIndex - 1].Play("SelectUp", -1, 0f);
            currentGunIndex--;
        }
        else
        {
            gunsUI[currentGunIndex].Play("DeselectUp", -1, 0f);
            gunsUI[guns.Length - 1].Play("SelectUp", -1, 0f);
            currentGunIndex = guns.Length - 1;
        }
        
        UpdateGun();
    }

    public void Down()
    {
        if (currentGunIndex < guns.Length - 1)
        {
            gunsUI[currentGunIndex].Play("DeselectDown", -1, 0f);
            gunsUI[currentGunIndex + 1].Play("SelectDown", -1, 0f);
            currentGunIndex++;
        }
        else
        {
            gunsUI[currentGunIndex].Play("DeselectDown", -1, 0f);
            gunsUI[0].Play("SelectDown", -1, 0f);
            currentGunIndex = 0;
        }
        
        UpdateGun();
    }

    private void CheckPurchase()
    {
        if (availableGuns[currentGunIndex].purchased)
        {
            playButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            playButton.SetActive(false);
            if (Bridge.GetInstance().thisPlayerInfo.coins >= availableGuns[currentGunIndex].cost)
                buyButton.GetComponent<Button>().interactable = true;
            else
                buyButton.GetComponent<Button>().interactable = false;
                
            buyButton.SetActive(true);
            gunCost.text = availableGuns[currentGunIndex].cost.ToString();
        }
    }

    private void UpdateGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == currentGunIndex)
                guns[i].SetActive(true);
            else
                guns[i].SetActive(false);
        }
        
        CheckPurchase();
    }
}
