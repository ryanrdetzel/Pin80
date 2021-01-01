namespace Pin80Server.Models
{
    public class Pixel
    {
        public PixelColor color;
        public int num;

        public bool needsUpdate = false;

        private long lastUpdate;

        public Pixel(int num, PixelColor color)
        {
            this.color = color;
            this.num = num;
        }

        public void updateColor(PixelColor color, long effectStarted)
        {
            if (effectStarted >= lastUpdate) // Only update if it's more recent.
            {
                this.color = color;
                needsUpdate = true;
                lastUpdate = effectStarted;
            }
        }
    }
}