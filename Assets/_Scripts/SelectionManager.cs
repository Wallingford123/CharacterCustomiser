using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boxLight, spriteDarkener, area2D, area3D, cam, creator2D, creator3D;

    [SerializeField]
    private CanvasGroup selectionUI, creator2DUI, creator3DUI;

    [SerializeField]
    private float cameraXChange3D, cameraXChange2D, transitionTime, fadeTime, disableDelayTime;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Renderer middlePart;

    [SerializeField]
    private Material transparentMat;

    private int currentSelection = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSelection = 1;
            boxLight.SetActive(true);
            spriteDarkener.SetActive(true);
            animator.SetBool("selected", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSelection = 0;
            boxLight.SetActive(false);
            spriteDarkener.SetActive(false);
            animator.SetBool("selected", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeSelection();
        }
    }

    void MakeSelection()
    {
        Fade fader = new Fade();
        StartCoroutine(fader.FadeOut(selectionUI, fadeTime, disableDelayTime));
        switch (currentSelection)
        {
            case 0:
                middlePart.material = transparentMat;
                StartCoroutine(fader.FadeOut(middlePart, fadeTime, disableDelayTime));
                StartCoroutine(DelayedDisable(area3D, disableDelayTime));
                StartCoroutine(CamMoveTransition(cameraXChange2D, transitionTime, cam, creator2D));
                StartCoroutine(fader.FadeIn(creator2DUI, fadeTime, transitionTime));
                break;
            case 1:
                StartCoroutine(DelayedDisable(area2D, disableDelayTime));
                StartCoroutine(CamMoveTransition(cameraXChange3D,transitionTime, cam, creator3D));
                StartCoroutine(fader.FadeIn(creator3DUI, fadeTime, transitionTime));
                break;
            default:
                Debug.Log("Something has gone very wrong");
                break;
        }
        this.enabled = false;
    }
    IEnumerator CamMoveTransition(float _dist, float _transitionTime, GameObject _cam, GameObject _creator)
    {
        animator.SetBool("selected", false);
        Vector3 camStart = _cam.transform.position;
        float t = 0;
        while (t <= 1) {
            cam.transform.position = new Vector3(Mathf.Lerp(camStart.x, camStart.x + _dist, t), camStart.y, camStart.z);
            t += Time.deltaTime/_transitionTime;
            yield return new WaitForEndOfFrame();
        }
        _creator.SetActive(true);
    }

    IEnumerator DelayedDisable(GameObject _object, float _delay)
    {
        yield return new WaitForSeconds(_delay);
        _object.SetActive(false);
    }
}
