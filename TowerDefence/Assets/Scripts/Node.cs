using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Node : MonoBehaviour
{
    public Color hoverOKColor;
    public Color hoverErrorColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject tower;
    [HideInInspector]
    public TowerBlueprint towerBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPostion()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (tower != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTower(buildManager.GetTowerToBuild());
    }

    void BuildTower(TowerBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
            return;

        PlayerStats.Money -= blueprint.cost;
        GameObject _tower = (GameObject)Instantiate(blueprint.prefab, GetBuildPostion(), blueprint.prefab.transform.rotation);
        tower = _tower;
        towerBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPostion(), Quaternion.identity);

        Destroy(effect, 5f);
    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.upgradeCost)
            return;

        PlayerStats.Money -= towerBlueprint.upgradeCost;

        // Destroy prev non-upgraded tower
        Destroy(tower);

        // Build new upgraded tower
        GameObject _tower = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, GetBuildPostion(), towerBlueprint.upgradedPrefab.transform.rotation);
        tower = _tower;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPostion(), Quaternion.identity);
        Destroy(effect, 5f);
 
        isUpgraded = true;
    }

    public void SellTower()
    {
        PlayerStats.Money += towerBlueprint.sellCost;

        Destroy(tower);

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPostion(), Quaternion.identity);
        Destroy(effect, 5f);

        towerBlueprint = null;
        isUpgraded = false;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (tower != null)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverOKColor;
        }
        else
        {
            rend.material.color = hoverErrorColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
