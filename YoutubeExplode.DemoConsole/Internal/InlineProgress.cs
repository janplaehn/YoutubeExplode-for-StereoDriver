using System;

namespace YoutubeExplode.DemoConsole.Internal
{
    internal class InlineProgress : IProgress<double>, IDisposable
    {
        private readonly int _posX;
        private readonly int _posY;

        public InlineProgress()
        {
            _posX = 0;
            _posY = 0;
        }

        public void Report(double progress)
        {
            //Console.SetCursorPosition(_posX, _posY);
            Console.WriteLine($"{progress:P1}");
        }

        public void Dispose()
        {
            //Console.SetCursorPosition(_posX, _posY);
            Console.WriteLine("Completed ✓");
        }
    }
}