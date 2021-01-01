using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace Pin80Server.Models
{
    internal class PixelTarget : Target
    {
        public int leds { get; set; }
        private readonly object _valueLock = new object();

        private List<Pixel> pixels;

        public override bool hasUpdate => pixels.Count(pixel => pixel.needsUpdate) > 0;

        public PixelTarget(JSONSerializer.TargetSerializer target) : base(target)
        {
            leds = target.leds;
            InitPixels();
        }

        private void InitPixels()
        {
            pixels = new List<Pixel>(leds);
            for (int i = 0; i < leds; i++)
            {
                pixels.Add(new Pixel(i, PixelColor.Black));
            }
        }

        public void updatePixel(int number, PixelColor color, long effectStarted)
        {
            lock (_valueLock)
            {
                pixels[number].updateColor(color, effectStarted);
            }
        }

        public void updateAllPixels(PixelColor color, long effectStarted)
        {
            foreach (var pixel in pixels)
            {
                updatePixel(pixel.num, color, effectStarted);
            }
        }

        private void markAllAsUpdated()
        {
            lock (_valueLock)
            {
                foreach (var pixel in pixels)
                {
                    pixel.needsUpdate = false;
                }
            }
        }

        private int updateCount()
        {
            return pixels.Count(pixel => pixel.needsUpdate);
        }

        private bool updateAll()
        {
            return updateCount() == leds;
        }

        private bool allSameColor()
        {
            PixelColor firstColor = pixels[0].color;
            int count = pixels.Count(pixel => pixel.color.Equals(firstColor));
            return count == leds;
        }

        public override void Run(SerialPort serialPort)
        {
            if (updateAll() && allSameColor())
            {
                int startRange = 0;
                int endRange = leds - 1;
                PixelColor firstColor = pixels[0].color;

                // TODO this could be one serial command.
                var OnCmd = string.Format("{0} PX{1}-{2} {3}\n", port, startRange, endRange, firstColor.hexValue);
                serialPort.Write(string.Format("{0} PXSTART\n", port));
                serialPort.Write(OnCmd);
                serialPort.Write(string.Format("{0} PXEND\n", port));
            }
            else
            {
                // Each pixel has to be sent solo
                serialPort.Write(string.Format("{0} PXSTART\n", port));

                foreach (var p in pixels.Where(p => p.needsUpdate))
                {
                    var OnCmd = string.Format("{0} PX{1} {2}\n", port, p.num, p.color.hexValue);
                    serialPort.Write(OnCmd);
                }
                serialPort.Write(string.Format("{0} PXEND\n", port));
            }

            // Set all pixels as not needing updates
            markAllAsUpdated();
        }
    }
}
