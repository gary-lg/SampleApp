using System;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Attributes;
using NUnit.Framework;

namespace Sample.Core.Tests.TimeSpanTests;

public class WhenTestingDays : WhenTestingTheBehaviourOf
{
    private int _numberOfDays;
    private TimeSpan _result;

    [Given]
    public void WeKnowNumberOfDays()
    {
        _numberOfDays = 12;
    }


    [When]
    public void WhenTimeSpanCreated()
    {
        _result = _numberOfDays.Days();
    }

    [Then]
    public void TimeSpanShouldHaveExpectedValue()
    {
        Assert.That(_result.TotalDays, Is.EqualTo(_numberOfDays));
    }
}