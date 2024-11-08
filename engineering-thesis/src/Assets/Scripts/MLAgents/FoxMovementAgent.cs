using System;
using Unity.MLAgents;

public class FoxMovementAgent : MovementAgent
{
    private float fox_each_episode_fixed;
    public override void Initialize()
    {
        base.Initialize();
        fox_each_episode_fixed = Academy.Instance.EnvironmentParameters.GetWithDefault("fox_each_episode_fixed", 0.0f);
    }

    public override void KillAgent(DeathCause deathCause)
    {
        if (simulationController != null)
        {
            switch (deathCause)
            {
                case DeathCause.Hunger:
                    simulationController.FoxesDiedOfHunger++;
                    break;
                case DeathCause.Thirst:
                    simulationController.FoxesDiedOfThirst++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(deathCause), deathCause, null);
            } 
        }
        base.KillAgent(deathCause);
    }

    protected override void ModifyRewardOnActionReceived()  //todo move adding reward to training settings to get rid shild classes and use MovementAgent for all agents
    {
        AddReward(fox_each_episode_fixed / MaxStep);
    }
}
