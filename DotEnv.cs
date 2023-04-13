namespace TurtleBot
{
    using System;
    using System.IO;
    
    public static class DotEnv
    {
        public static void SetEnv()
        {
            foreach (string line in File.ReadAllLines(".env"))
            {
                string[] parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}
