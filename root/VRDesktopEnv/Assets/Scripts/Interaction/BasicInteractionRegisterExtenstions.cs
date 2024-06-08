using System;
using UnityEngine;

public class BasicInteractionRegisterExtenstions : BC_InteractionRegister
{

    public void RegisterActionsToEventDesignated(Action enter, Action exit, RegisterEventType eventType)
    {
        //null check
        if(m_associatedInteractable == null)
        {
            Debug.LogWarning("You are trying to register events to a null Interactable. \nPlease Stop.");
            return;
        }

        //Switch case for speed, using delegates because addListener was horribly created
        switch(eventType)
        {
            case RegisterEventType.Activate:
                m_associatedInteractable.activated.AddListener(delegate { enter(); });
                m_associatedInteractable.deactivated.AddListener(delegate { exit(); });
                return;
            case RegisterEventType.Hover:
                m_associatedInteractable.hoverEntered.AddListener(delegate { enter(); });
                m_associatedInteractable.hoverExited.AddListener(delegate { exit(); });
                return;
            case RegisterEventType.Select:
                m_associatedInteractable.selectEntered.AddListener(delegate { enter(); });
                m_associatedInteractable.selectExited.AddListener(delegate { exit(); });
                return;
            case RegisterEventType.Focus:
                m_associatedInteractable.focusEntered.AddListener(delegate { enter(); });
                m_associatedInteractable.focusExited.AddListener(delegate { exit(); });
                return;
        }
    }

    public enum RegisterEventType
    {
        Hover,
        Select, 
        Activate,
        Focus
    }

}
