using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public partial class Program
    {
        public static void Main()
        {
            // Run();
            Run1();
            Run2("abc");
        }

        public static async Task<int> Run1Async()
        {
            return await Task.FromResult(1);
        }
        public static async Task Run2Async(string abc)
        {
            await Task.CompletedTask;
        }
        public static async Task Run3Async(CancellationToken token)
        {
            await Task.CompletedTask;
        }
    }
}
