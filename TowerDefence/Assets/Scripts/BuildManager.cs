using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TowerBlueprint towerToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return towerToBuild != null; } }

    public bool HasMoney { get { return PlayerStats.Money >= towerToBuild.cost;  } }

    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }    

        if(node.isUpgraded)
        {
            NodeUI.UpgradeButton.SetActive(false);
            node.towerBlueprint.sellCost = (node.towerBlueprint.cost + node.towerBlueprint.upgradeCost) / 2;
        }
        else 
        {
            NodeUI.UpgradeButton.SetActive(true);
            node.towerBlueprint.sellCost = node.towerBlueprint.cost / 2;
        }

        selectedNode = node;
        towerToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTowerToBuild(TowerBlueprint tower)
    {
        towerToBuild = tower;

        DeselectNode();
    }

    public TowerBlueprint GetTowerToBuild() => towerToBuild;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            towerToBuild = null;

            DeselectNode();
        }
    }
}
