using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageGlow : MonoBehaviour
{
    [SerializeField]
    private Color startColor, endColor;
    [SerializeField]
    private float glowTime, timeBetweenGlows;
    Image img;
    float t = 0;
    bool reverse = false, glowFinished = false;
    private void Update()
    {
        if(!reverse)
            img.color = Color.Lerp(startColor, endColor, t);
        else
            img.color = Color.Lerp(endColor, startColor, t);
        t += Time.deltaTime/glowTime;
        if (t >= 1 && !glowFinished)
        {
            glowFinished = true;
            if (reverse) StartCoroutine(DelayReverse(timeBetweenGlows));
            else { glowFinished = false; reverse = !reverse; t = 0; }
        }
    }

    IEnumerator DelayReverse(float _time)
    {
        yield return new WaitForSeconds(_time);
        reverse = !reverse;
        t = 0;
        glowFinished = false;
    }

    private void OnEnable()
    {
        img = GetComponent<Image>();
        t = 0; 
        glowFinished = false;
        reverse = false;
        img.color = startColor;
    }
    private void OnDisable()
    {
        GetComponent<Image>().color = Color.white;
    }
}
