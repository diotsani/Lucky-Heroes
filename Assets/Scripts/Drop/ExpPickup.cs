namespace Drop
{
    public class ExpPickup : Pickup
    {
        public override int Value { get; set; }
        protected override void OnCollected()
        {
            Player.Level.GainExp(Value);
        }
    }
}