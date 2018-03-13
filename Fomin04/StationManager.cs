using System;
using System.IO;

namespace Fomin04
{
    internal static class StationManager
    {
        internal static readonly string WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab 04");
        internal static Person CurrentPerson { get; set; }
    }
}