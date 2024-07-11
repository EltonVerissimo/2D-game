using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MenuButtonsAnim : MonoBehaviour
{
    public List<GameObject> buttons;

    [Header("Animation")]
    public float duration = .2f;
    public float delay = .05f;
    public Ease ease = Ease.OutBack;

    void OnEnable()
    {
        HideAllButtons();
        showButtons();
    }

    private void HideAllButtons()
    {
        foreach (var b in buttons)
        {
            b.transform.localScale = Vector3.zero;
            b.SetActive(false);
        }
    }

    private void showButtons()
    {
        for(int i =0; i < buttons.Count; i++)
        {
            var b = buttons[i];
            b.SetActive(true);
            b.transform.DOScale(1, duration).SetDelay(1 * delay).SetEase(ease);
        }
    }
}
