using Microsoft.UI.Xaml;
using System;

namespace OOP_Lab.Helpers
{
    public class DelayedAction
    {
        private DispatcherTimer _timer;
        private Action _action;

        public DelayedAction(TimeSpan delay, Action action)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = delay;
            _timer.Tick += OnTimerTick;
            _action = action;
        }

        private void OnTimerTick(object sender, object e)
        {
            _timer.Stop();
            _action?.Invoke();
        }

        // Метод для перезапуска таймера
        public void Restart()
        {
            _timer.Stop();
            _timer.Start();
        }
    }
}
