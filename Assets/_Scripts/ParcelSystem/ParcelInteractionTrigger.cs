

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ParcelInteractionTrigger : MonoBehaviour
{
    Parcel currentParcel = null;
    float interactionButtonTimer = 0;

    Controls controls;
    bool isInteracting = false;

    [Header("UI Elemente")]
    [SerializeField] private Slider interactionSlider;
    [SerializeField] private GameObject sliderContainer;
    [SerializeField] GameObject indicator;
    [SerializeField] Animator anim;

    public UnityAction onParcelPicked;

    void Awake()
    {
        if (controls == null) controls = new();
        if (sliderContainer != null) sliderContainer.SetActive(false);
    }

    void OnEnable()
    {
        if (controls == null) controls = new();
        controls.Enable();
        controls.Player.Interact.performed += OnPressInteract;
        controls.Player.Interact.canceled += OnReleaseInteract;
    }

    void OnDisable()
    {
        controls.Player.Interact.performed -= OnPressInteract;
        controls.Player.Interact.canceled -= OnReleaseInteract;
        controls.Disable();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Parcel>(out Parcel _parcel))
        {
            if (!collision.gameObject.activeSelf) return;
            currentParcel = _parcel;
            if (indicator != null) indicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Parcel>(out Parcel _parcel))
        {
            if (_parcel == currentParcel)
            {
                currentParcel = null;
                if (indicator != null) indicator.SetActive(false);
                ResetInteractionTimer();
            }
        }
    }

    void Update()
    {
        if (currentParcel == null || !isInteracting)
        {
            if (sliderContainer != null && sliderContainer.activeSelf)
                sliderContainer.SetActive(false);

            interactionButtonTimer = 0;
            return;
        }

        if (sliderContainer != null && !sliderContainer.activeSelf)
            sliderContainer.SetActive(true);

        interactionButtonTimer += Time.deltaTime;
        UpdateSliderUI(currentParcel.timeToCollect);

        if (interactionButtonTimer >= currentParcel.timeToCollect)
        {
            anim.SetBool("IsPicking", false);
            currentParcel.CollectParcel();
            onParcelPicked?.Invoke();
            ResetInteractionTimer();
        }
    }

    void ResetInteractionTimer()
    {
        interactionButtonTimer = 0;
        isInteracting = false;
        if (sliderContainer != null) sliderContainer.SetActive(false);
    }

    void OnPressInteract(InputAction.CallbackContext ctx)
    {
        if (currentParcel == null) return;
        isInteracting = true;
        anim.SetBool("IsPicking", true);
    }

    void OnReleaseInteract(InputAction.CallbackContext ctx)
    {
        isInteracting = false;
        interactionButtonTimer = 0;
        anim.SetBool("IsPicking", false);
    }

    void UpdateSliderUI(float _pickUpTime)
    {
        if (interactionSlider == null) return;

        float percentage = interactionButtonTimer / _pickUpTime;
        interactionSlider.value = percentage;
    }
}