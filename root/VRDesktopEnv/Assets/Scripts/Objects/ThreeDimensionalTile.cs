using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using static TileBehavior;

[RequireComponent(typeof(Collider))]
public class ThreeDimensionalTile : MonoBehaviour, TileBehavior
{
    public bool m_registerTapEvents;

    [Header("items to go into global settings")]

    [SerializeField]
    private Material m_material;
    Material TileBehavior.buttonMaterial 
    {
        get
        {
            return m_material;
        }
        set
        {
            m_material = value;
        }
    }

    [SerializeField]
    private Material m_pressedMaterial;
    Material TileBehavior.pressedButtonMaterial
    {
        get
        {
            return m_pressedMaterial;
        }
        set
        {
            m_pressedMaterial = value;
        }
    }

    private MeshRenderer m_renderer;

    [SerializeField]
    private float m_scaleOnInteract;
    [SerializeField]
    private float m_scaleTime;
    private float m_originalScale;

    [Header("Events")]

    [SerializeField]
    private TapEvent m_tapEvent = new();
    public TapEvent tapEvent 
    { 
        get
        {
            return m_tapEvent;
        }
        set
        {
            m_tapEvent = value;
        }
    }

    [SerializeField]
    private ExitTapEvent m_exitTapEvent = new();
    public ExitTapEvent exitTapEvent
    {
        get
        {
            return m_exitTapEvent;
        }
        set
        {
            m_exitTapEvent = value;
        }
    }

    #region Tile Actions

    public virtual void TapTile()
    {
        //nothing here rn, but its ready to be inherited
    }

    public virtual void CloseTile()
    {
        //nothing here rn, but its ready to be inherited
    }

    public virtual void LaunchTile()
    {
        //nothing here rn, but its ready to be inherited
    }

    #endregion

    #region Afforadances
    protected IEnumerator ScaleOverSeconds(float scaleIncrease, bool reversion, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = transform.localScale;
        Vector3 scaleToScaleTo;

        scaleToScaleTo = transform.localScale + new Vector3(scaleIncrease, scaleIncrease, scaleIncrease);
        if (reversion)
        {
            scaleToScaleTo = new Vector3(m_originalScale, m_originalScale, m_originalScale);
        }
        
        while (elapsedTime < seconds)
        {
            transform.localScale = Vector3.Lerp(startingScale, scaleToScaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.localScale = scaleToScaleTo;
    }

    protected IEnumerator TransitionToMaterial(Material startingMaterial, Material endingMaterial, float seconds)
    {
        float elapsedTime = 0;
        while (elapsedTime < seconds)
        {
            m_renderer.material.Lerp(startingMaterial, endingMaterial, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Ensure the target material is assigned at the end
        m_renderer.material = endingMaterial;
    }

    #endregion

    #region Monobehavior Elements

    protected void Awake()
    {
        //setup
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.material = m_material;
        m_originalScale = transform.localScale.x;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if we are registering events, dont do anything
        if (!m_registerTapEvents) { return; }
        Vector3 scaledVector = new Vector3(m_scaleOnInteract, m_scaleOnInteract, m_scaleOnInteract);
        StartCoroutine(ScaleOverSeconds(m_scaleOnInteract, false, m_scaleTime));

        //transition to new color
        StartCoroutine(TransitionToMaterial(m_material, m_pressedMaterial, m_scaleTime));

        //invoke the event
        TapEventData eventData = new TapEventData(other.gameObject, this);
        m_tapEvent.Invoke(eventData);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //if we are registering events, dont do anything
        if (!m_registerTapEvents) { return; }

        //nothing here rn, but its ready to be inherited
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        //if we are registering events, dont do anything
        if (!m_registerTapEvents) { return; }
        StartCoroutine(ScaleOverSeconds(m_originalScale, true, m_scaleTime));

        //invert and go back to normal
        StartCoroutine(TransitionToMaterial(m_pressedMaterial, m_material, m_scaleTime));

        //invoke the event
        m_exitTapEvent.Invoke();
    }

    #endregion
}

/// <summary>
/// Interface defining the behavior expected for a tile object.
/// </summary>
public interface TileBehavior
{
    /// <summary>
    /// Material used for the tile's button when not pressed.
    /// </summary>
    [SerializeField]
    public Material buttonMaterial { get; set; }

    /// <summary>
    /// Material used for the tile's button when pressed.
    /// </summary>
    [SerializeField]
    public Material pressedButtonMaterial { get; set; }

    /// <summary>
    /// Event triggered when the tile is tapped.
    /// </summary>
    [SerializeField]
    public TapEvent tapEvent { get; set; }

    /// <summary>
    /// Event triggered when a tap exits the tile.
    /// </summary>
    [SerializeField]
    public ExitTapEvent exitTapEvent { get; set; }

    /// <summary>
    /// Called when the tile is tapped. 
    /// (Implement specific functionality for tile interaction here)
    /// </summary>
    public void TapTile();

    /// <summary>
    /// Called to potentially launch functionality associated with the tile.
    /// (Implement specific functionality for launching tile actions here)
    /// </summary>
    public void LaunchTile();

    /// <summary>
    /// Called to potentially close or deactivate functionality associated with the tile.
    /// (Implement specific functionality for closing tile actions here)
    /// </summary>
    public void CloseTile();

    /// <summary>
    /// Event data class containing information about a tap on a tile.
    /// </summary>
    [System.Serializable]
    public class TapEventData
    {
        /// <summary>
        /// The GameObject that interacted with the tile (e.g., player, cursor).
        /// </summary>
        [SerializeField]
        public GameObject interactingObject;

        /// <summary>
        /// The TileBehavior instance that was interacted with.
        /// </summary>
        [SerializeField]
        public TileBehavior interactedTile;

        /// <summary>
        /// Creates a new TapEventData object.
        /// </summary>
        /// <param name="interactingObject">The GameObject that interacted with the tile.</param>
        /// <param name="interactedTile">The TileBehavior instance that was interacted with.</param>
        public TapEventData(GameObject interactingObject, TileBehavior interactedTile)
        {
            this.interactingObject = interactingObject;
            this.interactedTile = interactedTile;
        }
    }

    /// <summary>
    /// Event class used to trigger actions when a tile is tapped.
    /// </summary>
    [System.Serializable]
    public class TapEvent : UnityEvent<TapEventData> { }

    /// <summary>
    /// Event class used to trigger actions when a tap exits a tile.
    /// </summary>
    [System.Serializable]
    public class ExitTapEvent : UnityEvent { }
}