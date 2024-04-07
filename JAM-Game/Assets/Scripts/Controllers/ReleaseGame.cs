using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ReleaseGame : MonoBehaviour
    {
        [Header("Other References")]
        public GameProject GameProject;

        [Header("lINES")]
        public List<ReleaseLinePair> Looks;
        public List<ReleaseLinePair> Gameplay;
        public List<ReleaseLinePair> Marketing;
        public List<ReleaseLinePair> Bugs;
        public List<ReleaseLinePair> Release;

        [Header("UserInterface")]
        public GameObject AllRelease;

        public GameObject RestOfGame;

        public TMP_Text ReleaseMainText;
        public TMP_Text ReleaseSubText;

        public void Reset()
        {
            RestOfGame.SetActive(true);
            AllRelease.SetActive(false);
        }
        
        public void DoReleaseGame()
        {
            int looks = this.GameProject.art;
            int gameplay = this.GameProject.gameplay;
            int marketing = this.GameProject.marketing;
            int bugs = this.GameProject.bugs;

            float gameValue = (looks / 100.0f + gameplay / 100.0f + marketing / 100.0f) - bugs / 100.0f;
            
            RestOfGame.SetActive(false);
            AllRelease.SetActive(true);

            string looksText = GetText(Looks, looks);
            string gameplayText = GetText(Gameplay, gameplay);
            string marketingText = GetText(Marketing, marketing);
            string bugsText = GetText(Bugs, bugs);
            string release = GetText(Release, gameValue);
            
            ReleaseMainText.text = $"{looksText} {gameplayText} {marketingText} {bugsText} {release}";

            if (gameValue >= 0.5f)
            {
                ReleaseSubText.text = $"This game performed well.";
            }
            else if (gameValue >= 2)
            {
                ReleaseSubText.text = $"This game performed really well";
            }
            else if (gameValue >= 2.3)
            {
                ReleaseSubText.text = $"This game performed better than ever expected! [This is the best ending]";
            }
            else
            {
                ReleaseSubText.text = $"This game did not perform well.";
            }

        }

        private string GetText(List<ReleaseLinePair> looks, float i)
        {
            ReleaseLinePair releaseLinePair = new ReleaseLinePair();
            if (looks.Any(x => x.Value <= i))
            {
                var lines = looks.Where(x => x.Value <= i);
                float maxValue = -1;
                foreach (var x in lines)
                {
                    if (x.Value >= maxValue)
                    {
                        maxValue = x.Value;
                        releaseLinePair = x;
                    }
                }
            }

            return releaseLinePair.Line;
        }
    }

    [Serializable]
    public struct ReleaseLinePair
    {
        public float Value;
        public string Line;
    }
}