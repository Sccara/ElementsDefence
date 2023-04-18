using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public static GameObject UpgradeButton;
    public GameObject upgradeButton;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI sellText;

    private Node target;

    private void Start()
    {
        UpgradeButton = upgradeButton;
    }

    private void Update()
    {
        if (target != null)
        {
            if(target.towerBlueprint != null)
            {
                upgradeText.text = $"UPGRADE\t${target.towerBlueprint.upgradeCost}";
                sellText.text = $"SELL\t${target.towerBlueprint.sellCost}";
            }  
        }
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPostion();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
