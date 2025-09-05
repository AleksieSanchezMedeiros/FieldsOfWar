using UnityEngine;
using UnityEngine.UI;

public class IndependentButton : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite normalSprite;   
    public Sprite pressedSprite;  

    private Image buttonImage;
    private bool isPressed = false;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = normalSprite;

        GetComponent<Button>().onClick.AddListener(ToggleButton);
    }

    void ToggleButton()
    {
        isPressed = !isPressed;
        buttonImage.sprite = isPressed ? pressedSprite : normalSprite;
    }
}