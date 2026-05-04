using System;

namespace StatisticsAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] data = { 115, 182, 191, 31, 196, 1099, 5, 172, 10, 179, 83, 21, 20, 21, 186, 177, 195, 193, 188, 199, 62, 109, 105, 183, 110 };
            int n = data.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        double temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }

            double sum = 0;
            for (int i = 0; i < n; i++) sum += data[i];
            double mean = sum / n;

            double mode = data[0];
            int maxCount = 0;
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n; j++)
                {
                    if (data[j] == data[i]) count++;
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    mode = data[i];
                }
            }

            double p20 = GetPercentile(data, 20);
            double p50 = GetPercentile(data, 50); 
            double q1 = GetPercentile(data, 25);
            double q2 = GetPercentile(data, 50);
            double q3 = GetPercentile(data, 75);

            double sumSquaredDiff = 0;
            for (int i = 0; i < n; i++) sumSquaredDiff += Math.Pow(data[i] - mean, 2);
            double variance = sumSquaredDiff / n;

            double stdDev = Math.Sqrt(variance);

            double range = data[n - 1] - data[0];
            double iqr = q3 - q1;

            double sumDev = 0;
            for (int i = 0; i < n; i++) sumDev += (data[i] - mean);

            Console.WriteLine("--- Statistics (Basics) ---");
            Console.WriteLine($"(i) Mean: {mean:F2}");
            Console.WriteLine($"(ii) Mode: {mode}");
            Console.WriteLine($"(iii) Median: {p50}");
            Console.WriteLine($"(iv) Variance: {variance:F2}");
            Console.WriteLine($"(v) P20: {p20}");
            Console.WriteLine($"(vi) P50: {p50}");
            Console.WriteLine($"(vii/ix) Third Quartile: {q3}");
            Console.WriteLine($"(viii) Second Quartile: {q2}");
            Console.WriteLine($"(x) Range: {range}");
            Console.WriteLine($"(xi) Interquartile Range: {iqr}");
            Console.WriteLine($"(xii) Standard Deviation: {stdDev:F2}");
            Console.WriteLine($"(xiii) Summation of Deviations: {sumDev:F10}");

            Console.WriteLine("\n--- Outlier Detection ---");
            double lowerBound = q1 - 1.5 * iqr;
            double upperBound = q3 + 1.5 * iqr;
            for (int i = 0; i < n; i++)
            {
                string status = (data[i] < lowerBound || data[i] > upperBound) ? "Outlier" : "Normal";
                Console.WriteLine($"{data[i]}: {status}");
            }
        }

        static double GetPercentile(double[] sortedData, double p)
        {
            double rank = (p / 100.0) * (sortedData.Length - 1);
            int index = (int)rank;
            double fraction = rank - index;
            if (index + 1 < sortedData.Length)
                return sortedData[index] + fraction * (sortedData[index + 1] - sortedData[index]);
            return sortedData[index];
        }
    }
}

