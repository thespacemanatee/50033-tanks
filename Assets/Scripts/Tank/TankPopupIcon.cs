using System.Collections;
using UnityEngine;

namespace Tank
{
    public class TankPopupIcon : MonoBehaviour
    {
        public GameObject scanIcon;
        public GameObject chaseIcon;

        public void LoadScanIcon()
        {
            scanIcon.SetActive(true);
            chaseIcon.SetActive(false);
        }

        public void LoadChaseIcon()
        {
            scanIcon.SetActive(false);
            chaseIcon.SetActive(true);
        }

        public void DisableAllIcons()
        {
            scanIcon.SetActive(false);
            chaseIcon.SetActive(false);
        }

        private static IEnumerator ShowIconForDuration(GameObject icon, int duration)
        {
            icon.SetActive(true);
            if (duration <= 0) yield break;

            yield return new WaitForSeconds(duration);

            icon.SetActive(false);
        }
    }
}