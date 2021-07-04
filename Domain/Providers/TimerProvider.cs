using System.Diagnostics;

namespace Domain.Providers
{
    public class TimerProvider : ITimerProvider
    {
        public Stopwatch Timer { get; set; }
        public long ElapsedTimeInMilliseconds { get; set; }

        public void StartNew()
            => Timer = Stopwatch.StartNew();

        public void Stop()
        {
            Timer.Stop();
            ElapsedTimeInMilliseconds = Timer.ElapsedMilliseconds;
        }

        public long GetElapsedTimeInMilliseconds()
            => Timer.ElapsedMilliseconds;
    }
}