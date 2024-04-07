using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour
{
    private Dictionary<string, ScrollBarTweener> Tweeners;

    private class ScrollBarTweener
    {
        public Scrollbar Scrollbar;
        public TMP_Text Text;
        public Tweener ScrollBarDOTweener;
        public bool Running;
    }
        
    public void GiveScrollBar(string key, Scrollbar scrollBar, TMP_Text text)
    {
        if (Tweeners == null)
        {
            Tweeners = new Dictionary<string, ScrollBarTweener>();
        }
        
        Tweeners.Add(key, new ScrollBarTweener()
        {
            Scrollbar = scrollBar,
            Text = text
        });
    }

    public void ShakeBad(string key)
    {
        if (Tweeners == null)
        {
            Tweeners = new Dictionary<string, ScrollBarTweener>();
        }

        if (Tweeners.TryGetValue(key, out ScrollBarTweener tweenerGroup))
        {
            bool canRunAgain = !tweenerGroup.Running;
            if (!canRunAgain) return;
            tweenerGroup.Running = true;
            
            tweenerGroup.Scrollbar.transform.rotation = new Quaternion();
            tweenerGroup.Scrollbar.transform.DOShakeRotation(0.25f,
                new Vector3(0, 0, 5), 10, 90F, true, ShakeRandomnessMode.Harmonic);
            
            tweenerGroup.Text.color = Color.red;
            tweenerGroup.ScrollBarDOTweener = tweenerGroup.Text.DOColor(Color.black, 0.5f);
            
            // For some reason the OnComplete in the Tweener itself was not giving the value I wanted. this was the
            // next best thing and would work when null.
            tweenerGroup.ScrollBarDOTweener.OnComplete(() =>
            {
                if (Tweeners.TryGetValue(key, out ScrollBarTweener innerGroup))
                {
                    innerGroup.Scrollbar.transform.rotation = new Quaternion();
                    innerGroup.Running = false;
                }
            });
        }
    }
    
    public void ShakeGood(string key)
    {
        if (Tweeners == null)
        {
            Tweeners = new Dictionary<string, ScrollBarTweener>();
        }

        if (Tweeners.TryGetValue(key, out ScrollBarTweener tweenerGroup))
        {
            bool canRunAgain = !tweenerGroup.Running;
            if (!canRunAgain) return;
            tweenerGroup.Running = true;
            
            tweenerGroup.Scrollbar.transform.rotation = new Quaternion();
            tweenerGroup.Scrollbar.transform.DOShakeRotation(0.25f,
                new Vector3(0, 0, 5), 10, 90F, true, ShakeRandomnessMode.Harmonic);

            tweenerGroup.Text.color = new Color32(48, 224, 24, 255 );
            tweenerGroup.ScrollBarDOTweener = tweenerGroup.Text.DOColor(Color.black, 0.5f);
            
            // For some reason the OnComplete in the Tweener itself was not giving the value I wanted. this was the
            // next best thing and would work when null.
            tweenerGroup.ScrollBarDOTweener.OnComplete(() =>
            {
                if (Tweeners.TryGetValue(key, out ScrollBarTweener innerGroup))
                {
                    innerGroup.Scrollbar.transform.rotation = new Quaternion();
                    innerGroup.Running = false;
                }
            });
        }
    }
}
