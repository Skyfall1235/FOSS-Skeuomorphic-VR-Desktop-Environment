using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class ThreeDimensionalTile : XRGrabInteractable, TileBehavior, TileEvents
{
    public bool m_registerTapEvents;
    [SerializeField]
    protected SO_GlobalUISettings m_uiSettings;

    [Header("Events")]

    [SerializeField]
    protected TileEvents.TapEvent m_tapEvent = new();
    public TileEvents.TapEvent tapEvent 
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
    protected TileEvents.ExitTapEvent m_exitTapEvent = new();
    public TileEvents.ExitTapEvent exitTapEvent
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

    public virtual void Tap(GameObject interactor)
    {
        //invoke the event
        TileEvents.TapEventData eventData = new TileEvents.TapEventData(interactor.gameObject, this);
        m_tapEvent.Invoke(eventData);
    }

    public virtual void ExitTap()
    {
        //invoke the event
        m_exitTapEvent.Invoke();
    }

    public virtual void CloseTile()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Monobehavior Elements

    protected override void Awake()
    {
        base.Awake();
        GetComponent<Rigidbody>().isKinematic = true;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    #endregion
}

/// <summary>
/// Interface defining the behavior expected for a tile object.
/// </summary>
public interface TileBehavior
{
    /// <summary>
    /// Called when the tile is tapped. 
    /// (Implement specific functionality for tile interaction here)
    /// </summary>
    public void Tap(GameObject interactor);

    /// <summary>
    /// Called to potentially close or deactivate functionality associated with the tile.
    /// (Implement specific functionality for closing tile actions here)
    /// </summary>
    public void CloseTile();
}

public interface TileVisuals
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
}

public interface TileEvents
{
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
