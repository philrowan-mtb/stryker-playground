using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Playground.Core.Tests
{
    public class CoffeePotTests
    {
        [Fact]
        public void NotBrewingByDefault()
        {
            var coffee = new CoffeePot();
            coffee.IsBrewing.Should().BeFalse();
        }

        [Fact]
        public void NoBrewScheduledByDefault()
        {
            var coffee = new CoffeePot();
            coffee.IsBrewScheduled.Should().BeFalse();
        }

        [Fact]
        public void CanStartBrew()
        {
            var coffee = new CoffeePot();
            coffee.StartBrew();
            coffee.IsBrewing.Should().BeTrue();
        }

        [Fact]
        public void CanScheduleBrew()
        {
            var coffee = new CoffeePot();
            coffee.ScheduleBrew(DateTimeOffset.Now.AddSeconds(10));
            coffee.IsBrewScheduled.Should().BeTrue();
        }

        //[Fact]
        //public async Task ScheduledBrewStartsAfterScheduleTime()
        //{
        //    var coffee = new CoffeePot();
        //    coffee.ScheduleBrew(DateTimeOffset.Now.AddSeconds(10));
        //    await Task.Delay(13 * 1000);
        //    coffee.IsBrewing.Should().BeTrue();
        //}

        //[Fact]
        //public async Task ScheduledBrewOnlyRunsOnce()
        //{
        //    var coffee = new CoffeePot();
        //    coffee.ScheduleBrew(DateTimeOffset.Now.AddSeconds(10));
        //    await Task.Delay(13 * 1000);
        //    coffee.IsBrewing.Should().BeTrue();
        //    coffee.IsBrewScheduled.Should().BeFalse();
        //    await Task.Delay(13 * 1000);
        //    coffee.IsBrewing.Should().BeTrue();
        //    coffee.IsBrewScheduled.Should().BeFalse();
        //}

        [Fact]
        public void CannotScheduleIfAlreadyBrewing()
        {
            var coffee = new CoffeePot();
            coffee.StartBrew();
            Action action = () => coffee.ScheduleBrew(DateTimeOffset.Now.AddSeconds(10));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CannotScheduleInThePast()
        {
            var coffee = new CoffeePot();
            coffee.StartBrew();
            Action action = () => coffee.ScheduleBrew(DateTimeOffset.Now.AddSeconds(-10));
            action.Should().Throw<ArgumentException>();
        }
    }
}
