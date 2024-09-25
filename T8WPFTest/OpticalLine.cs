using System;
using System.Collections.Generic;

namespace T8WPFTest
{
    public partial class MainWindow
    {
        public class OpticalLine
        {
            private List<OpticalSegment> _segments;
            private double _inputPower;

            public OpticalLine(double inputPower, List<OpticalSegment> segments)
            {
                _inputPower = inputPower;
                _segments = segments;
            }
            private double DbmToMilliwatts(double dbm)
            {
                return Math.Pow(10, dbm / 10);
            }
            private double MilliwattsToDbm(double milliwatts)
            {
                return 10 * Math.Log10(milliwatts);
            }
            public double CalculateSignalPower()
            {
                double signalPower = DbmToMilliwatts(_inputPower);
                foreach (var segment in _segments)
                {
                    signalPower /= Math.Pow(10, segment.Attenuation / 10);
                    signalPower *= Math.Pow(10, segment.Gain / 10);
                }
                return MilliwattsToDbm(signalPower);
            }
            public double CalculateNoisePower()
            {
                double noisePower = 0;
                foreach (var segment in _segments)
                {
                    double currentNoisePower = DbmToMilliwatts(segment.Noise);
                    noisePower /= Math.Pow(10, segment.Attenuation / 10);
                    noisePower += currentNoisePower;
                    noisePower *= Math.Pow(10, segment.Gain / 10);
                }
                return MilliwattsToDbm(noisePower);
            }
            public double CalculateOSNR()
            {
                double signalPower = CalculateSignalPower();
                double noisePower = CalculateNoisePower();
                return signalPower - noisePower;
            }
        }
    }
}
