using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeManager : MonoBehaviour
{
    public Processor furnace;
    public void BuyFurnaceUpg()
    {
        BuyUpgrade(UpgradeType.furnaceSpeed);
    }
    public void BuyPickAxeUpg()
    {
        BuyUpgrade(UpgradeType.pickDmg);
    }
    public void BuyBatUpg()
    {
        BuyUpgrade(UpgradeType.batDmg);
    }
    public void BuyWrenchUpg()
    {
        BuyUpgrade(UpgradeType.wrenchDmg);
    }
    public void BuySpeedUpg()
    {
        BuyUpgrade(UpgradeType.playerSpeed);
    }
    public void BuyWeightUpg()
    {
        BuyUpgrade(UpgradeType.weightLimit);
    }

    public void BuyUpgrade(UpgradeType uType)
    {
        switch (uType)
        {
            case UpgradeType.pickDmg:
                if (GameStateManager.instance.gold >= pickDmgCost)
                {
                    GameStateManager.instance.gold -= pickDmgCost;
                    PlayerItemUser.instance.pickAxeDamage += 1;
                    pickDmgCost += 1;
                    pickDmgCostText.text = "Buy: " + pickDmgCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;

            case UpgradeType.batDmg:
                if (GameStateManager.instance.gold >= batDmgCost)
                {
                    GameStateManager.instance.gold -= batDmgCost;
                    PlayerItemUser.instance.batDamage += 1;
                    batDmgCost += 1;
                    batDmgCostText.text = "Buy: " + batDmgCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;

            case UpgradeType.wrenchDmg:
                if (GameStateManager.instance.gold >= wrenchDmgCost)
                {
                    GameStateManager.instance.gold -= wrenchDmgCost;
                    PlayerItemUser.instance.wrenchDamage -= 1;
                    wrenchDmgCost += 1;
                    wrenchDmgCostText.text = "Buy: " + wrenchDmgCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;

            case UpgradeType.playerSpeed:
                if (GameStateManager.instance.gold >= playerSpeedCost)
                {
                    GameStateManager.instance.gold -= playerSpeedCost;
                    PlayerController.instance.maxSpeed += 0.5f;
                    PlayerController.instance.acc += 0.25f;
                    PlayerController.instance.deacc += 0.25f;
                    playerSpeedCost += 1;
                    playerSpeedCostText.text = "Buy: " + playerSpeedCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;


            case UpgradeType.weightLimit:
                if (GameStateManager.instance.gold >= weightLimitCost)
                {
                    GameStateManager.instance.gold -= weightLimitCost;
                    WeightManager.instance.maxWeight += 1;
                    weightLimitCost += 1;
                    weightLimitCostText.text = "Buy: " + weightLimitCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;


            case UpgradeType.furnaceSpeed:
                if (GameStateManager.instance.gold >= furnaceSpeedCost)
                {
                    GameStateManager.instance.gold -= furnaceSpeedCost;
                    furnace.recipes[0].workTime -= 2f;
                    furnaceSpeedCost += 1;
                    furnaceCostText.text = "Buy: " + furnaceSpeedCost.ToString() + " gold";
                    if (buySound != null)
                    {
                        buySound.Play();
                    }
                }
                break;


            default:
                break;
        }

    }


    public enum UpgradeType
    {
        pickDmg, wrenchDmg, batDmg, playerSpeed, weightLimit, furnaceSpeed
    };


    public int pickDmgCost = 1;
    [SerializeField] private TMP_Text pickDmgCostText;
    public int wrenchDmgCost = 1;
    [SerializeField] private TMP_Text wrenchDmgCostText;
    public int batDmgCost = 1;
    [SerializeField] private TMP_Text batDmgCostText;
    public int playerSpeedCost = 1;
    [SerializeField] private TMP_Text playerSpeedCostText;

    public int weightLimitCost = 1;
    [SerializeField] private TMP_Text weightLimitCostText;

    public int furnaceSpeedCost = 1;
    [SerializeField] private TMP_Text furnaceCostText;

    [SerializeField] private AudioSource buySound;


   

}
