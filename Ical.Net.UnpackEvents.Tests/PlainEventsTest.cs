using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.UnpackEvents;

namespace Ical.Net.UnpackEvents.Tests
{
    /// <summary>
    /// Simple events without any RRULE's.
    /// </summary>
    [TestClass]
    public class PlainEventsTest
    {

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public void PlainEvents_NoChangesAfterUnpack(int eventsDepth)
        {
            var calendar = new Calendar();
            PlainEventsTestHelper.AddPlainEvents(calendar, eventsDepth);

            var actualEvents = calendar.Events.UnpackEvents();

            for (int i = 0; i < calendar.Events.Count; i++)
            {
                Assert.AreEqual(calendar.Events[i], actualEvents[i]);
            }
        }
    }
}
