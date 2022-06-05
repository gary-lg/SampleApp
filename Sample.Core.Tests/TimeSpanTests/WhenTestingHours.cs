using System;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Attributes;
using NUnit.Framework;

namespace Sample.Core.Tests.TimeSpanTests;

public class WhenTestingHours : WhenTestingTheBehaviourOf
{
    private int _numberOfHours;
    private TimeSpan _result;

    [Given]
    public void WeKnowNumberOfHours()
    {
        _numberOfHours = 65;
    }


    [When]
    public void WhenTimeSpanCreated()
    {
        _result = _numberOfHours.Hours();
    }

    [Then]
    public void TimeSpanShouldHaveExpectedValue()
    {
        Assert.That(_result.TotalHours, Is.EqualTo(_numberOfHours));
    }
}