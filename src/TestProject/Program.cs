using System;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;

namespace Project
{
    // https://github.com/matkoch/custom-source-generators
    public partial class Program
    {
        public static void Main()
        {
        }
    }

    public class Calculator
    {
        public Calculator(ILogger logger, IMath math, IUniverse universe)
        {
        }
    }
}

namespace Project.Tests
{
    public partial class CalculatorTest
    {
        public void Test01()
        {
            // A.CallTo(() => );
            Console.WriteLine(logger);
        }
    }

}


public interface ILogger
{
}

public interface IMath
{
}

public interface IUniverse
{
}
