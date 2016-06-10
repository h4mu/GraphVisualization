using System;
using System.IO;

namespace DataLoader
{
    internal class InputFilenameProvider : IInputFilenameProvider
    {
        public string[] GetInputFileNames()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
        }
    }
}