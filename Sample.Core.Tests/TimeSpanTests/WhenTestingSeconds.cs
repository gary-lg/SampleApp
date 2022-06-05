using System;
using LeapingGorilla.Testing.Core.Attributes;
using LeapingGorilla.Testing.NUnit;
using LeapingGorilla.Testing.NUnit.Attributes;
using NUnit.Framework;

namespace Sample.Core.Tests.TimeSpanTests;

public class WhenTestingSeconds : WhenTestingTheBehaviourOf
{
    private int _numberOfSeconds;
    private TimeSpan _result;

    [Given]
    public void WeKnowNumberOfSeconds()
    {
        _numberOfSeconds = 668;
    }


    [When]
    public void WhenTimeSpanCreated()
    {
        _result = _numberOfSeconds.Seconds();
    }

    [Then]
    public void TimeSpanShouldHaveExpectedValue()
    {
        Assert.That(_result.TotalSeconds, Is.EqualTo(_numberOfSeconds));
    }
}