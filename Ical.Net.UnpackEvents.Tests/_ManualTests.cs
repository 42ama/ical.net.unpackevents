using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Ical.Net.UnpackEvents.Tests
{
    [TestClass]
    public class _ManualTests
    {
        private readonly string _icsFilePath = "C:\\Users\\Maxim.Alonov\\AppData\\Local\\ExportOutlookCalendarToExcel\\outlook-export.ics";

        [TestMethod]
        public void ManualTest()
        {
            using var reader = new StreamReader(_icsFilePath);
            var calendar = Calendar.Load(reader);
            var unpackedEvents = calendar.Events.UnpackEvents();
            ;
        }
    }
}
