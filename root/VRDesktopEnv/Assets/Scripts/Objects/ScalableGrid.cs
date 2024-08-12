using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

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

    private int TileZAmount
    {
        get
        {
            float val = CellSize.z + CellPadding.z;
            return (int)(ColliderBounds.z / val);
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

    public VRGridData VRGrid;

    [SerializeField]
    private List<ThreeDimensionalTile> m_tiles = new List<ThreeDimensionalTile>();

    public List<ThreeDimensionalTile> Tiles
    {
        get => m_tiles;
        set
        {
            m_tiles = value;
            SetObjects();
        }
    }

    [SerializeField]
    private GameObject SocketPrefab;


    public void SetupGrid(int x, int y, GameObject[] objects)
    {
        if(x < 0 || y < 0) { return; }
        VRGrid = new VRGridData(x, y, objects);
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
            SetupGrid(TileZAmount, TileYAmount, children);
        }
        else
        {
            VRGrid.tiles[location.x * location.y] = cell;
        }
        
    }

    private void Awake()
    {
        GameObject[] children = new GameObject[m_tiles.Count];

        SetupGrid(TileZAmount, TileYAmount, children);

        SetObjects();
    }

    private void SetObjects()
    {
        // Get the extents (half size) of the collider
        Vector3 extentsMin = GetComponent<BoxCollider>().bounds.min;
        //Debug.Log(extentsMin);

        // Calculate starting area
        Vector3 placementPosition = new Vector3(0f, extentsMin.y + CellPadding.y, extentsMin.z + (CellPadding.z * 2));

        Vector3 sizeOfCell = CellSize + CellPadding;

        int counter = 0;

        // Loop through each cell position in the grid
        for (int y = TileYAmount - 1; y >= 0 && counter < VRGrid.tiles.Length; y--)
        {
            for (int z = 0; z < TileZAmount && counter < VRGrid.tiles.Length; z++)
            {
                //set location for socket
                Vector3 setPlacement = new Vector3(0f, y * sizeOfCell.y, z * sizeOfCell.z);

                //instatiate socket
                GameObject socket = InstantiateSocket();
                socket.transform.parent = transform;

                // Place a socket at the calculated position
                socket.transform.position = placementPosition + setPlacement;

                //confirms if the objects can be enabled or disabled
                if (IsPositionWithinCollider(m_tiles[counter].transform.position))
                {
                    m_tiles[counter].gameObject.SetActive(true);
                }

                counter++;

                // Break out of the loop if counter exceeds tile length
                if (counter >= VRGrid.tiles.Length)
                {
                    break;
                }                    
            }
        }

        // Disable remaining objects
        for (int i = counter; i < VRGrid.tiles.Length; i++)
        {
            VRGrid.tiles[i].gameObject.SetActive(false);
        }
    }

    private GameObject InstantiateSocket()
    {
        return Instantiate(SocketPrefab);
    }

    private bool IsPositionWithinCollider(Vector3 position)
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.Contains(position);
        }
        return false;
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
