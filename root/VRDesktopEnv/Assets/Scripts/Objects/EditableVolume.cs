using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(ScalableGrid))]
public class EditableVolume : MonoBehaviour
{
    [SerializeField]
    public bool m_CanBeMoved = false;

    [SerializeField]
    public bool m_isScalable = false;

    [SerializeField]
    public bool m_hideTiles = false;

    [SerializeField]
    public List<ThreeDimensionalTile> ThreeDimensionalTiles = new List<ThreeDimensionalTile>();

    [SerializeField]
    public ScalableGrid m_scalableGrid;

    public void Setup()
    {
        m_scalableGrid = GetComponent<ScalableGrid>();
    }

}
