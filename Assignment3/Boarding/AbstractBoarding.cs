using Assignment3.Model;

namespace Assignment3.Boarding
{
    public abstract class AbstractBoarding
    {
        protected Plane plane;

        public AbstractBoarding()
        {

        }

        public abstract void GenerateBoarding(int planeLength);

        public int RunSimulation(int planeLength)
        {
            return 0;
        }

        public List<int> TestBoardingMethod(int planeLength, int simulationNo)
        {
            return new List<int>();
        }
    }
}
