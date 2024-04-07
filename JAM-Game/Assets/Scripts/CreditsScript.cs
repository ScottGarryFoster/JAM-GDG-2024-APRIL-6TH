using UnityEngine;

namespace DefaultNamespace
{
    public class CreditsScript : MonoBehaviour
    {
        public GameObject MainObj;
        public GameObject CreditsObj;

        public void ShowCredits()
        {
            MainObj.SetActive(false);
            CreditsObj.SetActive(true);
        }
        
        public void HideCredits()
        {
            MainObj.SetActive(true);
            CreditsObj.SetActive(false);
        }
    }
}