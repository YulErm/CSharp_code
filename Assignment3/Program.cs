using Assignment3.Boarding;

namespace Assignment3
{
    internal class Program
    {
        static double CalculateAverageSteps(AbstractBoarding boarding, int planeLength, int simulationNo)
        {
            List<int> results = boarding.TestBoardingMethod(planeLength, simulationNo);

            return (double)results.Sum() / results.Count;
        }

        static void Main(string[] args)
        {
            AbstractBoarding test = new BoardingFTB();

            System.Console.WriteLine(CalculateAverageSteps(test, 8, 100));

            // averages with plane length 8 and 100 runs:
            // FTB - 297,77
            // BTF - 253,04
            // WTA - 181,56
            // ATW - 201,33
            // Random - 197,44
            // Steffen - 134,56
        }
    }
}