namespace EntityFrameWork
{
    public interface ITick
    {
        void Tick();
    }

    public interface IPreTick
    {
        void PreTick();
    }


    
    public interface ILateTick
    {
        void LateTick();
    }
}