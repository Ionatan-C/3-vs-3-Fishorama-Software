namespace FishORamaEngineLibrary
{
    public interface IKernel
    {
        Screen Screen { get; }

        void InsertToken(IDraw pToken);
        void RemoveToken(IDraw pToken);
    }
}
