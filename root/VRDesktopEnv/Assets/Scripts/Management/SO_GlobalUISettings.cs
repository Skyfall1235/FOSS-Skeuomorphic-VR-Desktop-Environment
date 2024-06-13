using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GlobalUISettings")]
public class SO_GlobalUISettings : ScriptableObject
{
    [SerializeField]
    private Material m_material;
    public Material Material { get { return m_material; } }
    [SerializeField]
    private Material m_pressedMaterial;
    public Material PressedMaterial { get { return m_pressedMaterial; } }
    [SerializeField]
    private float m_scaleOnInteract;
    public float ScaleOnInteract { get { return m_scaleOnInteract; } }
    [SerializeField]
    private float m_scaleTime;
    public float ScaleTime { get { return m_scaleTime; } }
    
}