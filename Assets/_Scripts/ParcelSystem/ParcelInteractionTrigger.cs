using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ParcelInteractionTrigger : MonoBehaviour
{
    Parcel currentParcel = null;
    float interactionButtonTimer = 0;

    Controls controls;

    bool isInteracting = false;

    [SerializeField] Image fillImage;

    void Awake()
    {
        controls = new();
    }

    void OnEnable()
    {
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
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Parcel>(out Parcel _parcel))
        {
            if (_parcel == currentParcel)
            {
                currentParcel = null;
                interactionButtonTimer = 0;
            }
        }
    }

    void Update()
    {
        if (currentParcel == null) return;
        if (GameManager.Instance.GameIsPaused()) return;

        if (!isInteracting)
        {
            ResetInteractionTimer();
            return;
        }

        interactionButtonTimer += Time.deltaTime;
        UpdateFillUI(currentParcel.timeToCollect);

        if (interactionButtonTimer >= currentParcel.timeToCollect)
        {
            currentParcel.CollectParcel();
            ResetInteractionTimer();
        }
    }

    void ResetInteractionTimer()
    {
        interactionButtonTimer = 0;
        UpdateFillUI(10);
    }

    void OnPressInteract(InputAction.CallbackContext ctx)
    {
        if (currentParcel == null) return;

        isInteracting = true;
    }

    void OnReleaseInteract(InputAction.CallbackContext ctx)
    {
        if (currentParcel == null) return;

        isInteracting = false;
    }

    void UpdateFillUI(float _pickUpTime)
    {
        if (fillImage == null) return;

        float percentage = interactionButtonTimer / _pickUpTime;

        fillImage.fillAmount = percentage;
    }
}