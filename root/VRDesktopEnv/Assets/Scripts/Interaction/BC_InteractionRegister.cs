using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This class manages the registration of XRBaseInteractors with a game object.
/// It provides functionality to register, deregister, and check for the currently registered interactor.
/// </summary>
public class BC_InteractionRegister : MonoBehaviour
{

    #region Variables

    //this is for registering the interactor, NOT the interactable
    /// <summary>
    /// The currently registered XRBaseInteractor.
    /// </summary>
    [SerializeField]
    protected XRBaseInteractor m_registeredInteractor;

    /// <summary>
    /// Gets or sets the currently registered XRBaseInteractor.
    /// Setting this property triggers the OnInteractorRegister event based on registration (true) or unregistration (false).
    /// </summary>
    public XRBaseInteractor RegisteredInteractor
    {
        get { return m_registeredInteractor; }
        set
        {
            if(value != null) 
            {
                m_registeredInteractor = value;
                OnInteractorRegister.Invoke(true);
            }
            else
            {
                m_registeredInteractor = null;
                OnInteractorRegister.Invoke(false);
            }
        }
    }

    /// <summary>
    /// The associated XRBaseInteractable object.
    /// </summary>
    [SerializeField] [HideInInspector]
    protected XRBaseInteractable m_associatedInteractable;

    /// <summary>
    /// This event is invoked whenever the RegisteredInteractor property is set.
    /// The event argument is true for registration and false for unregistration.
    /// </summary>
    public UnityEvent<bool> OnInteractorRegister = new UnityEvent<bool>();

#endregion

    /// <summary>
    /// Registers an XRBaseInteractor with the current object.
    /// </summary>
    /// <param name="args">The arguments associated with the interaction event.</param>
    protected void RegisterInteractor(BaseInteractionEventArgs args)
    {
        m_registeredInteractor = args.interactorObject as XRBaseInteractor;
    }

    /// <summary>
    /// Deregisters the currently registered XRBaseInteractor.
    /// </summary>
    protected void DeRegisterInteractor(BaseInteractionEventArgs args)
    {
        m_registeredInteractor = null;
    }

    /// <summary>
    /// Checks if an XRBaseInteractor is currently registered.
    /// </summary>
    /// <returns>True if an XRBaseInteractor is registered, False otherwise.</returns>
    public bool HasRegistered()
    {
        if(m_registeredInteractor != null)
        {
            return true;
        }
        return false;
    }
}
