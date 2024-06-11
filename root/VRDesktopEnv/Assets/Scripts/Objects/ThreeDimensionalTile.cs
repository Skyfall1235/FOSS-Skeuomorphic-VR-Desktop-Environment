using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static TileBehavior;

[RequireComponent(typeof(Collider))]
public class ThreeDimensionalTile : MonoBehaviour, TileBehavior
{
    public bool m_registerTapEvents;

    Material TileBehavior.buttonMaterial 
    { 
        get
        {
            return GetComponent<Material>();
        }
        set
        {
            Material mat = GetComponent<Material>();
            mat = value;
        }
    }

    [SerializeField]
    private TileBehavior.TapEvent m_tapEvent;
    public TileBehavior.TapEvent tapEvent 
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
    private TileBehavior.ExitTapEvent m_exitTapEvent;
    public TileBehavior.ExitTapEvent ExitTapEvent
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

    public virtual void TapTile()
    {
        
    }

    public virtual void CloseTile()
    {
        
    }

    public virtual void LaunchTile()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!m_registerTapEvents) { return; }
        TapEventData eventData = new TapEventData(other.gameObject, this);
        m_tapEvent.Invoke(eventData);
    }
    protected virtual void OnTriggerStay(Collider other)
    {

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (!m_registerTapEvents) { return; }
    }

    
}

public interface TileBehavior
{
    public Material buttonMaterial { get; set; }

    public TapEvent tapEvent { get; set; }

    public ExitTapEvent exitTapEvent { get; set; }

    public void TapTile();
    public void LaunchTile();
    public void CloseTile();

    public class ExitTapEvent : UnityEvent { }

    public class TapEvent : UnityEvent<TapEventData> { }

    public class TapEventData
    {
        [SerializeField]
        public GameObject interactingObject;
        [SerializeField]
        public TileBehavior interactedTile;

        /// <summary>
        /// Creation of a new Tap Event Data
        /// </summary>
        /// <param name="interactingObject"></param>
        /// <param name="interactedTile"></param>
        public TapEventData(GameObject interactingObject, TileBehavior interactedTile)
        {
            this.interactingObject = interactingObject;
            this.interactedTile = interactedTile;
        }
    }

}
