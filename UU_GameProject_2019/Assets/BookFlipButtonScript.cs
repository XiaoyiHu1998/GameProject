using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BookFlipButtonScript : MonoBehaviour
{
    public Graphic randomButton, randomButtonText;

    public void HideButton()
    {
        randomButton.DOFade(0, 5);
        randomButtonText.DOFade(0, 5);
    }
}
