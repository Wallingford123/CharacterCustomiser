using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade
{
    public IEnumerator FadeOut(Renderer _rend, float _time, float _startDelay)
    {
        yield return new WaitForSeconds(_startDelay);
        float t = 0;
        Color c = _rend.material.color;
        while (t < 1) {
            _rend.material.color = new Color(c.r, c.g, c.b, 1-t);
            t += Time.deltaTime/_time;
            yield return new WaitForEndOfFrame();
        }
        _rend.material.color = new Color(c.r, c.g, c.b, 0);
    }

    public IEnumerator FadeOut(CanvasGroup _UI, float _time, float _startDelay) 
    {
        yield return new WaitForSeconds(_startDelay);
        float t = 0;
        while (t < 1)
        {
            _UI.alpha = Mathf.Lerp(1, 0, t);
            t += Time.deltaTime / _time;
            yield return new WaitForEndOfFrame();
        }
        _UI.alpha = 0;
        _UI.interactable = false;
        _UI.blocksRaycasts = false;
    }
    public IEnumerator FadeIn(CanvasGroup _UI, float _time, float _startDelay)
    {
        yield return new WaitForSeconds(_startDelay);
        float t = 0;
        while (t < 1)
        {
            _UI.alpha = Mathf.Lerp(0, 1, t);
            t += Time.deltaTime / _time;
            yield return new WaitForEndOfFrame();
        }
        _UI.interactable = true;
        _UI.blocksRaycasts = true;
    }
}
