namespace FishORamaEngineLibrary
{
    public interface ITokenManager
    {
        ChickenLeg ChickenLeg { get; }

        void SetChickenLeg(ChickenLeg pChickenLeg);
        void RemoveChickenLeg();
    }
}
