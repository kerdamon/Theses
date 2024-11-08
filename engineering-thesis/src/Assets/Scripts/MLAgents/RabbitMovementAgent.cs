using System;
using Unity.MLAgents;

public class RabbitMovementAgent : MovementAgent
{
    private float rabbit_each_episode_fixed;
    public override void Initialize()
    {
        base.Initialize();
        rabbit_each_episode_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("rabbit_each_episode_fixed ", 0.0f);
    }

    public override void KillAgent(DeathCause deathCause)
    {
        if (simulationController != null)
        {
            switch (deathCause)
            {
                case DeathCause.Hunger:
                    simulationController.RabbitsDiedOfHunger++;
                    break;
                case DeathCause.Thirst:
                    simulationController.RabbitsDiedOfThirst++;
                    break;
                case DeathCause.Eaten:
                    simulationController.RabbitsDiedOfBeingEaten++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deathCause), deathCause, null);
            } 
        }
        base.KillAgent(deathCause);
    }

    protected override void ModifyRewardOnActionReceived()
    {
        AddReward(rabbit_each_episode_fixed / MaxStep);
    }
}
