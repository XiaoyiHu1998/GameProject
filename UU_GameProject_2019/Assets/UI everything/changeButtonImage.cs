using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class changeButtonImage : MonoBehaviour
{
    public Sprite firstSprite, secondSprite;
    public Button buttonToBeChanged;
    public RectTransform textToBeMoved;
    protected bool IsPressed;
    protected float timer;

    public void Awake()
    {
        buttonToBeChanged.onClick.AddListener(ChangeImage);
    }

    public void ChangeImage()
    {
        //when the button is pressed, the sprite changes to a second image of the button to create the effect of the button being pressed down

        if (buttonToBeChanged.image.sprite == firstSprite)
        {
            buttonToBeChanged.image.sprite = secondSprite;
            textToBeMoved.DOLocalMove(new Vector3(0,-4,0), 0.01f, false);
            IsPressed = true;
        }
        else
        {
            buttonToBeChanged.image.sprite = firstSprite;
            textToBeMoved.DOLocalMove(new Vector3(0, 4, 0), 0.01f, false);
        }
    }

    void Update()
    {
        //the button returns up after a short time

        if (IsPressed)
        {
            timer += Time.deltaTime;
            if(timer > 0.3)
            {
                //Button returns up
                buttonToBeChanged.image.sprite = firstSprite;
                textToBeMoved.DOLocalMove(new Vector3(0, 4, 0), 0.01f, false);
                IsPressed = false;
            }
        } 
        else
        {
            timer = 0;
        }
    }

}
