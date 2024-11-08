namespace DecisionMaking.States
{
    public class ChillingState : MainState
    {
        public override float CurrentRank => scoreCurve.Evaluate(0);

    }
}