namespace _Scripts.Utilities.Interfaces
{
    public interface IMouseInteraction : ITileModifier
    {
        public void Interact();

        public void Remove();
    }
}