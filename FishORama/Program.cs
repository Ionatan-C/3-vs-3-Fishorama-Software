using System;
using FishORamaEngineLibrary;

namespace FishORama
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            // Create Kernel object (using statement used as Game implements IDisposable)
            using (var kernel = new Kernel())
            {
                // Create Simulation object
                Simulation sim = new Simulation(kernel);
                // Inject Simulation into Kernel
                kernel.Simulation = sim;


                // Create TokenManager object
                TokenManager tokenMan = new TokenManager();
                // Inject TokenManager into Kernel
                kernel.TokenManager = tokenMan;
                // Inject TokenManager into Simulation
                sim.TokenManager = tokenMan;

                // Instruct game engine to run simulation
                kernel.Run();
            }
        }
    }
}
