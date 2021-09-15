using System;
using System.Timers;

namespace Playground.Core
{
    public class CoffeePot
    {
        private Timer _timer;
        public bool IsBrewing { get; private set; } = false;

        public bool IsBrewScheduled => _timer != null;

        public void StartBrew()
        {
            IsBrewing = true;
        }

        public void StopBrew()
        {
            IsBrewing = false;
        }

        public void ScheduleBrew(DateTimeOffset brewTime)
        {
            var now = DateTimeOffset.Now;
            if (brewTime.Subtract(now).Ticks <= 0)
            {
                throw new ArgumentException("Brewing time must be in the future", nameof(brewTime));
            }

            if (IsBrewing)
            {
                throw new InvalidOperationException("Cannot schedule a brew when already brewing");
            }

            var interval = brewTime.Subtract(DateTimeOffset.Now).TotalMilliseconds;
            _timer = new Timer(interval);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
            StartBrew();
        }
    }
}
