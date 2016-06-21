using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading;

namespace TestJob
{
    public class Wait
    {
        private readonly TimeSpan _timeout;
        private readonly TimeSpan _checkInterval;
        private readonly Stopwatch _stopwatch;
        private bool _isSatisfied = true;

        private Wait(TimeSpan timeout) : this(timeout, TimeSpan.FromSeconds(1))
        {
        }

        private Wait(TimeSpan timeout, TimeSpan checkInterval)
        {
            Contract.Requires(timeout >= TimeSpan.Zero);
            Contract.Requires(checkInterval >= TimeSpan.Zero);

            _timeout = timeout;
            _checkInterval = checkInterval;
            _stopwatch = Stopwatch.StartNew();
        }

        public static Wait WithTimeout(TimeSpan timeout, TimeSpan pollingInterval)
        {
            return new Wait(timeout, pollingInterval);
        }

        public static Wait WithTimeout(TimeSpan timeout)
        {
            return new Wait(timeout);
        }

        public Wait WaitFor(Func<bool> condition)
        {
            Contract.Requires(condition != null);

            if (!_isSatisfied)
            {
                return this;
            }

            while (!condition())
            {
                var sleepAmount = Min(_timeout - _stopwatch.Elapsed, _checkInterval);

                if (sleepAmount < TimeSpan.Zero)
                {
                    _isSatisfied = false;
                    break;
                }

                Thread.Sleep(sleepAmount);
            }

            return this;
        }

        public bool IsSatisfied
        {
            get { return _isSatisfied; }
        }

        public static bool SpinWait(Func<bool> condition, TimeSpan timeout)
        {
            Browser.getDriver().Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(500));
            return SpinWait(condition, timeout, TimeSpan.FromSeconds(1));
        }

        public static bool SpinWait(Func<bool> condition, TimeSpan timeout, TimeSpan pollingInterval)
        {
            Browser.getDriver().Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(500));
            return WithTimeout(timeout, pollingInterval).WaitFor(condition).IsSatisfied;
        }

        public static bool Try(Action action)
        {
            Exception exception;

            return Try(action, out exception);
        }

        public static bool Try(Action action, out Exception exception)
        {
            Contract.Requires(action != null);

            try
            {
                action();
                exception = null;

                return true;
            }
            catch (Exception e)
            {
                exception = e;

                return false;
            }
        }

        public static Func<bool> MakeTry(Action action)
        {
            return () => Try(action);
        }

        private static T Min<T>(T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) < 0 ? left : right;
        }
    }
}