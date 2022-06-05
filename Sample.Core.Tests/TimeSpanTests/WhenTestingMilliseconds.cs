using System;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Attributes;
using NUnit.Framework;

namespace Sample.Core.Tests.TimeSpanTests;

public class WhenTestingMilliseconds : WhenTestingTheBehaviourOf
{
    private int _numberOfMilliseconds;
    private TimeSpan _result;

    [Given]
    public void WeKnowNumberOfMilliseconds()
    {
        _numberOfMilliseconds = 23;
    }


    [When]
    public void WhenTimeSpanCreated()
    {
        _result = _numberOfMilliseconds.Milliseconds();
    }

    [Then]
    public void TimeSpanShouldHaveExpectedValue()
    {
        Assert.That(_result.TotalMilliseconds, Is.EqualTo(_numberOfMilliseconds));
    }
}