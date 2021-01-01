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

        public bool isOff()
        {
            return color.red == 0 && color.green == 0 && color.blue == 0;
        }

        public void dimBy(int percent)
        {
            var newColor = new PixelColor(
                 (int)((1 - (percent / 100.0)) * color.red),
                 (int)((1 - (percent / 100.0)) * color.green),
                 (int)((1 - (percent / 100.0)) * color.blue)
                );
            updateColor(newColor, null);
        }

        public void updateColor(PixelColor color, EffectInstance action)
        {
            if (action == null || action.startedTimetamp >= lastUpdate) // Only update if it's more recent.
            {
                this.color = color;
                needsUpdate = true;
                if (action != null)
                {
                    lastUpdate = action.startedTimetamp;
                }
            }
        }
    }
}