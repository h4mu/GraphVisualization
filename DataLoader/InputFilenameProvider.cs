using System;
using System.IO;

namespace DataLoader
{
    internal class InputFilenameProvider : IInputFilenameProvider
    {
        private string directory;

        internal InputFilenameProvider(string[] args)
        {
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                directory = args[0];
            }
        }

        public string[] GetInputFileNames()
        {
            return Directory.GetFiles(directory ?? Directory.GetCurrentDirectory(), "*.xml");
        }
    }
}