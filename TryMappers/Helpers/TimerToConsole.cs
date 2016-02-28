#region licence
// ======================================================================================
// TryMappers - compare AutoMapper and ExpressMapper for LINQ and develop flattener for ExpressMapper
// Filename: TimerToConsole.cs
// Date Created: 2016/02/25
// 
// Under the MIT License (MIT)
// 
// Written by Jon Smith : GitHub JonPSmith, www.thereformedprogrammer.net
// ======================================================================================
#endregion

using System;
using System.Diagnostics;

namespace TryMappers.Helpers
{
    class TimerToConsole : IDisposable
    {
        private readonly string _message;

        private readonly Stopwatch _timer;

        public TimerToConsole(string message)
        {
            _message = message;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void Dispose()
        {
            Console.WriteLine("{0} took {1:f2} ms", _message, 1000.0 * _timer.ElapsedTicks / Stopwatch.Frequency);
        }
    }
}
