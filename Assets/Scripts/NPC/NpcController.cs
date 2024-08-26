using System;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private GameObject interactButton;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
                ShowInteractButton();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerTag>())
                HideInteractButton();
        }

        private void ShowInteractButton()
        {
            interactButton.SetActive(true);
        }
        private void HideInteractButton()
        {
            interactButton.SetActive(false);
        }
    }
}

