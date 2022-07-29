
namespace Game.Weapon
{
    public class MachineGun : Weapon
    {
        private void Start()
        {
            base.Start();
            FillingPoolShell();
        }

        public override void Shuting()
        {
            base.Shuting();
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