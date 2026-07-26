namespace Drop
{
    public class GoldPickup : Pickup
    {
        public override int Value { get; set; }

        protected override void OnCollected()
        {
            Player.Resources.GainGold(Value);
        }
    }
}