using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ScalableGrid : MonoBehaviour
{
    //define cell sizes and cell gap
    [SerializeField]
    private Vector3 CellSize = Vector3.one;
    [SerializeField]
    private Vector3 CellPadding = Vector3.one;

    public Vector3 ColliderBounds
    {
        get
        {
            return GetComponent<BoxCollider>().bounds.size;
        }
    }
    private Vector3 ColliderCenter
    {
        get
        {
            return GetComponent<BoxCollider>().bounds.center;
        }
    }

    private int TileXAmount
    {
        get
        {
            float val = CellSize.x + CellPadding.x;
            return (int)(ColliderBounds.x / val);
        }
    }
    private int TileYAmount
    {
        get
        {
            float val = CellSize.y + CellPadding.y;
            return (int)(ColliderBounds.y / val);
        }
    }

    public VRGridData Grid;

    public void SetupGrid(int x, int y, GameObject[] objects)
    {
        if(x < 0 || y < 0) { return; }
        Grid = new VRGridData(x, y, objects);
    }

    public void SetGridItem(GameObject cell, Vector2Int location = default)
    {
        if(location == default)
        {
            //add cell as child
            cell.transform.parent = transform;
            //create new grid
            GameObject[] children = new GameObject[transform.childCount - 1];
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                children[i] = transform.GetChild(i).gameObject;
            }

            //setup grid again
            SetupGrid(TileXAmount, TileYAmount, children);
        }
        else
        {
            Grid.tiles[location.x * location.y] = cell;
        }
        
    }

    private void Awake()
    {
        GameObject[] children = new GameObject[transform.childCount - 1];
        for (int i = 0; i < transform.childCount - 1; i ++) 
        {
            children[i] = transform.GetChild(i).gameObject;
        }

        SetupGrid(TileXAmount, TileYAmount, children);
    }

    public void Start()
    {
        Debug.Log(ColliderBounds);
    }

    private void SetObjects()
    {
        //find center
        //find offeset to start at
        //extents minus padding?

        //didivde volume to find location for each cell
        //place child cells at locations
    }

    public struct VRGridData
    {
        [SerializeField]
        public GameObject[] tiles;
        [SerializeField]
        public Vector2Int gridSize;

        public VRGridData(int x, int y)
        {
            tiles = new GameObject[x * y];
            gridSize = new Vector2Int(x, y);
        }
        public VRGridData(int x, int y, GameObject[] objects)
        {
            //tiles are organised in the Grid
            tiles = objects;
            gridSize = new Vector2Int(x, y);
        }
    }

}
