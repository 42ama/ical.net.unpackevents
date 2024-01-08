using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.UnpackEvents;

namespace Ical.Net.UnpackEvents.Tests
{
    /// <summary>
    /// Recurring events with single RRULE.
    /// </summary>
    [TestClass]
    public class SimpleRecurringDailyEventsTests
    {
        [TestMethod]
        public void ReadMeExample()
        {
            // Create iCal calendar.
            var calendar = new Calendar();

            // Add a recurring event.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                }
            });

            // Add plain event to expand date range to two days.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(1)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(1))
            });


            // Unpack the events into a plain list.
            var actualEvents = calendar.Events.UnpackEvents(); // returns List<CalendarEvent>
            var allEventsCount = 3; // In two days there are: two instances of recurring event, one plain event.

            // Verify that the count of unpacked events matches the expected count.
            Assert.AreEqual(allEventsCount, actualEvents.Count);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public void SingleRecurringEvent_DailyRecurrence(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // Two is added because, we always have to events: one instance of recurring and one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays + 2;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public void SingleRecurringEvent_DailyRecurrence_EndingOneDayShortly(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                    {
                        Until = DateTime.Now.AddDays(endRecurrenceAfterDays - 1)
                    }
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // We always have at least one plain event
            var recurringInstancesCount = endRecurrenceAfterDays + 1;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }

        [TestMethod]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void SingleRecurringEvent_DailyRecurrence_ExceptionDates(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                },
                ExceptionDates = new List<PeriodList>
                {
                    new PeriodList
                    {
                        new Period(
                            new CalDateTime(DateTime.Now.AddDays(1).AddMinutes(-30)),
                            new CalDateTime(DateTime.Now.AddDays(1).AddMinutes(60))
                            )
                    },
                    new PeriodList
                    {
                        new Period(
                            new CalDateTime(DateTime.Now.AddDays(2).AddMinutes(-30)),
                            new CalDateTime(DateTime.Now.AddDays(2).AddMinutes(60))
                            )
                    },
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // Decrease by number of exception days.
            // Two is added because, we always have to events: one instance of recurring and one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays - 2 + 2;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }


        [TestMethod]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(5)]
        public void SingleRecurringEvent_DailyRecurrence_ExceptionDates_EndingOneDayShortly(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                    {
                        Until = DateTime.Now.AddDays(endRecurrenceAfterDays - 1)
                    }
                },
                ExceptionDates = new List<PeriodList>
                {
                    new PeriodList
                    {
                        new Period(
                            new CalDateTime(DateTime.Now.AddDays(1).AddMinutes(-30)),
                            new CalDateTime(DateTime.Now.AddDays(1).AddMinutes(60))
                            )
                    },
                    new PeriodList
                    {
                        new Period(
                            new CalDateTime(DateTime.Now.AddDays(2).AddMinutes(-30)),
                            new CalDateTime(DateTime.Now.AddDays(2).AddMinutes(60))
                            )
                    },
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // Decrease by number of exception days (two).
            // Add one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays - 2 + 1;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }


        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public void DoubleRecurringEvents_DailyRecurrence(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring events.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                }
            });
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(30)),
                End = new CalDateTime(DateTime.Now.AddMinutes(60)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });


            // Three is added because, we always have three events: two instances of recurring and one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays * 2 + 3;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }


        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(10)]
        public void DoubleRecurringEvents_DailyRecurrence_EndingOneDayShortly(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring events.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                    {
                        Until = DateTime.Now.AddHours(1).AddDays(endRecurrenceAfterDays - 1)
                    }
                }
            });
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(30)),
                End = new CalDateTime(DateTime.Now.AddMinutes(60)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                    {
                        Until = DateTime.Now.AddHours(1).AddDays(endRecurrenceAfterDays - 1)
                    }
                }
            });

            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });


            // We always have one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays * 2 + 1;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }


        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        public void SingleRecurringEvent_DailyRecurrence_TwoMoved(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            var recurringEventUid = Guid.NewGuid().ToString();
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                },
                Uid = recurringEventUid
            });

            // Adding moved events.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(120)),
                End = new CalDateTime(DateTime.Now.AddMinutes(150)),
                Uid = recurringEventUid,
                RecurrenceId = new CalDateTime(DateTime.Now)
            });

            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(120)),
                End = new CalDateTime(DateTime.Now.AddMinutes(150)),
                Uid = recurringEventUid,
                RecurrenceId = new CalDateTime(DateTime.Now.AddDays(1))
            });


            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // Two is added because, we always have to events: one instance of recurring and one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays + 2;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }


        [TestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(10)]
        public void SingleRecurringEvent_DailyRecurrence_TwoMoved_EndingOneDayShortly(int endRecurrenceAfterDays)
        {
            var calendar = new Calendar();

            // Adding recurring event.
            var recurringEventUid = Guid.NewGuid().ToString();
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now),
                End = new CalDateTime(DateTime.Now.AddMinutes(30)),
                RecurrenceRules = new List<RecurrencePattern> {
                    new RecurrencePattern(FrequencyType.Daily, 1)
                    {
                        Until = DateTime.Now.AddDays(endRecurrenceAfterDays - 1)
                    }
                },
                Uid = recurringEventUid
            });

            // Adding moved events.
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(120)),
                End = new CalDateTime(DateTime.Now.AddMinutes(150)),
                Uid = recurringEventUid,
                RecurrenceId = new CalDateTime(DateTime.Now)
            });

            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(120)),
                End = new CalDateTime(DateTime.Now.AddMinutes(150)),
                Uid = recurringEventUid,
                RecurrenceId = new CalDateTime(DateTime.Now.AddDays(1))
            });


            // Plain events to expand date range to several dates
            calendar.AddChild(new CalendarEvent
            {
                Start = new CalDateTime(DateTime.Now.AddMinutes(60).AddDays(endRecurrenceAfterDays)),
                End = new CalDateTime(DateTime.Now.AddMinutes(90).AddDays(endRecurrenceAfterDays))
            });

            // We always have one plain event.
            var recurringInstancesCount = endRecurrenceAfterDays + 1;


            var actualEvents = calendar.Events.UnpackEvents();


            Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
        }
    }
}
