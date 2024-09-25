namespace T8WPFTest
{
    public partial class MainWindow
    {
        public class OpticalSegment
        {
            public double Attenuation { get; set; }
            public double Gain { get; set; }
            public double Noise { get; set; }

            public OpticalSegment(double attenuation, double gain, double noise)
            {
                Attenuation = attenuation;
                Gain = gain;
                Noise = noise;
            }
        }
    }
}
