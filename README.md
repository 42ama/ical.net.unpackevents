# Ical.Net UnpackEvents
This is an extension for [Ical.Net](https://www.nuget.org/packages/Ical.Net), which enables the exportation of recurring events into a plain list of events. The exported events retain information enriched from their parent event.

## Usage examples
```c#
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
```

## Links
- [GitHub](https://github.com/42ama/ical.net.unpackevents)
- [NuGet](https://www.nuget.org/packages/Ical.Net.UnpackEvents/)