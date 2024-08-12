using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Collider))]
public class BetterVRButton : ThreeDimensionalTile
{
    private float m_originalScale;
    private MeshRenderer m_renderer;
    private bool m_useScaleAffordance, m_useColorAffordance;

    #region Tile Actions

    private void TapWrapper(ActivateEventArgs args)
    {
        Tap(args.interactorObject.transform.gameObject);
    }
    private void ExitTapWrapper(DeactivateEventArgs args)
    {
        ExitTap();
    }

    public override void Tap(GameObject interactor)
    {
        //invoke the event
        TileEvents.TapEventData eventData = new TileEvents.TapEventData(interactor.gameObject, this);
        m_tapEvent.Invoke(eventData);

        //if we are registering events, dont do anything
        if (!m_registerTapEvents) { return; }

        if (m_useScaleAffordance)
        {
            Vector3 scaledVector = new Vector3(m_uiSettings.ScaleOnInteract, m_uiSettings.ScaleOnInteract, m_uiSettings.ScaleOnInteract);
            StartCoroutine(ScaleOverSeconds(m_uiSettings.ScaleOnInteract, false, m_uiSettings.ScaleTime));
        }

        if (m_useColorAffordance)
        {
            //transition to new color
            StartCoroutine(TransitionToMaterial(m_uiSettings.Material, m_uiSettings.PressedMaterial, m_uiSettings.ScaleTime));
        }
    }

    public override void ExitTap()
    {
        //if we are registering events, dont do anything
        if (!m_registerTapEvents) { return; }
        if (m_useScaleAffordance)
        {
            StartCoroutine(ScaleOverSeconds(m_originalScale, true, m_uiSettings.ScaleTime));
        }

        if (m_useColorAffordance)
        {
            //invert and go back to normal
            StartCoroutine(TransitionToMaterial(m_uiSettings.PressedMaterial, m_uiSettings.Material, m_uiSettings.ScaleTime));
        }

        //invoke the event
        m_exitTapEvent.Invoke();
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

    protected override void Awake()
    {
        base.Awake();
        GetComponent<Rigidbody>().isKinematic = true;
        //setup
        m_renderer = GetComponent<MeshRenderer>();
        m_renderer.material = m_uiSettings.Material;
        m_originalScale = transform.localScale.x;
        
        //setup events
        activated.AddListener(TapWrapper);
        deactivated.AddListener(ExitTapWrapper);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        //deregister events
        activated.RemoveListener(TapWrapper);
        deactivated.RemoveListener(ExitTapWrapper);
    }

    #endregion
}
