using UnityEngine;

public class EnemyWall : FactionWall
{
    public int garrison = 0;
    public int maxGarrison = 10;

    public void AllyEnters(GameObject ally)
    {
        if (garrison < maxGarrison) {
            garrison++;
            Stop(ally);
        }
    }

    public void AllyDiesOrLeaves()
    {
        garrison--;
        AskForReinforcements();
    }

    private void Stop(GameObject ally)
    {
        // Uncomment when we have the movement for the soliders
        //var move = ally.GetComponent<Movement>();
        //if (move != null) {
        //    move.Stop();
        //}
    }

    private void AskForReinforcements()
    {
        EnemyManager.Instance.RequestReinforcements(this);
    }
}
