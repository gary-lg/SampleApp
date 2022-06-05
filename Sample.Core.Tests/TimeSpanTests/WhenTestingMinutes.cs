using System;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Attributes;
using NUnit.Framework;

namespace Sample.Core.Tests.TimeSpanTests;

public class WhenTestingMinutes : WhenTestingTheBehaviourOf
{
    private int _numberOfMinutes;
    private TimeSpan _result;

    [Given]
    public void WeKnowNumberOfMinutes()
    {
        _numberOfMinutes = 18;
    }


    [When]
    public void WhenTimeSpanCreated()
    {
        _result = _numberOfMinutes.Minutes();
    }

    [Then]
    public void TimeSpanShouldHaveExpectedValue()
    {
        Assert.That(_result.TotalMinutes, Is.EqualTo(_numberOfMinutes));
    }
}