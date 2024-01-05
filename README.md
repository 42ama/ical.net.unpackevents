# Ical.Net UnpackEvents
This is an extension for [Ical.Net](https://www.nuget.org/packages/Ical.Net), which enables the exportation of recurring events into a plain list of events. The exported events retain information enriched from their parent event.

## Usage examples
```
// Create iCal calendar.
var calendar = new Calendar(); 

// Specify the number of instances of the parent recurring event that should be available.
var recurringInstancesCount = 2;

// Add a recurring event.
calendar.AddChild(new CalendarEvent
{
    Start = new CalDateTime(DateTime.Now),
    End = new CalDateTime(DateTime.Now.AddMinutes(30)),
    RecurrenceRules = new List<RecurrencePattern> {
        new RecurrencePattern(FrequencyType.Daily, 1)
        {
            Until = DateTime.Now.AddDays(2)
        }
    }
});

// Unpack the events into a plain list.
var actualEvents = calendar.Events.UnpackEvents();

// Verify that the count of unpacked events matches the expected count.
Assert.AreEqual(recurringInstancesCount, actualEvents.Count);
```

## Links
- [Github](https://github.com/42ama/ical.net.unpackevents)
- [NuGet](https://www.nuget.org/packages/Ical.Net.UnpackEvents/)
