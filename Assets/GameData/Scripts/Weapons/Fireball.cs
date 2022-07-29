
namespace Game.Weapon
{
    public class Fireball : Weapon
    {
        private void Start()
        {
            base.Start();
            FillingPoolShell();
        }

        public override void Shuting()
        {
            base.Shuting();
            countInHands = maxCountInHands;
            countInBag = maxCountInBag;

        }

        public override void StopShut()
        {
            base.StopShut();
        }

        public override void FillingPoolShell()
        {
            base.FillingPoolShell();
        }
    }
}