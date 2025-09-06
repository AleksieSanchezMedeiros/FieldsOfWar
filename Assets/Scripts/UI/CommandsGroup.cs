using UnityEngine;
using UnityEngine.UI;

public class CommandsGroup : MonoBehaviour
{
    [Header("Attack Button")]
    public Image attackButton;
    public Sprite attackNormal;
    public Sprite attackSelected;

    [Header("Defend Button")]
    public Image defendButton;
    public Sprite defendNormal;
    public Sprite defendSelected;

    [Header("Retreat Button")]
    public Image retreatButton;
    public Sprite retreatNormal;
    public Sprite retreatSelected;

    private Image currentSelected; 

    public void ClickAttack()
    {
        SelectButton(attackButton, attackSelected);
    }

    public void ClickDefend()
    {
        SelectButton(defendButton, defendSelected);
    }

    public void ClickRetreat()
    {
        SelectButton(retreatButton, retreatSelected);
    }

    private void SelectButton(Image clickedImage, Sprite selectedSprite)
    {
        attackButton.sprite = attackNormal;
        defendButton.sprite = defendNormal;
        retreatButton.sprite = retreatNormal;

        clickedImage.sprite = selectedSprite;

        currentSelected = clickedImage;
    }
}