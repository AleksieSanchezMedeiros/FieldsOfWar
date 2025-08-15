using UnityEngine;

public class MinerUnit : TroopCommand
{
    public int earnings = 100;
    private bool isMining = false;
    private bool returnToBase = false;

    protected override void Update()
    {
        base.Update();

        if (GameManager.currrentAction == 2) {

            if (!isMining && !returnToBase) {

                if (Vector3.Distance(transform.position, attackLocation) < 0.1f) {
                    isMining = true;
                    Invoke(nameof(FinishMining), 7f);
                }
            }
            else if (returnToBase) {
                MoveTo(baseLocation);
                
                if (Vector3.Distance(transform.position, baseLocation) < 0.1f) {
                    GameManager gm = GameManager.FindFirstObjectByType<GameManager>();
                    if (gm != null) {
                        gm.IncreaseCurrentGoldAmount(earnings);
                    }
                    returnToBase = false;
                }
            }
        }
    }

    private void FinishMining()
    {
        isMining = false;
        returnToBase = true;
    }
}
