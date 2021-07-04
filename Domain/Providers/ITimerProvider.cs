namespace Domain.Providers
{
    public interface ITimerProvider
    {
        long GetElapsedTimeInMilliseconds();
        void StartNew();
        public void Stop();
    }
}