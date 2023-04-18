using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint lightningTower;
    public TowerBlueprint fireTower;
    public TowerBlueprint iceTower;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectLightningTower()
    {
        buildManager.SelectTowerToBuild(lightningTower);
    }

    public void SelectFireTower()
    {
        buildManager.SelectTowerToBuild(fireTower);
    }

    public void SelectIceTower()
    {
        buildManager.SelectTowerToBuild(iceTower);
    }
}
