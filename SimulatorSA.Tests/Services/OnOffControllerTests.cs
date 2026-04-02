using SimulatorSA.Core.Services;
using System;
using Xunit;

namespace SimulatorSA.Tests.Services;

public class OnOffControllerTests
{
    [Fact]
    public void CalculateOutput_ShouldTurnOn_WhenMeasuredValueIsBelowLowerLimit()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            outputWhenOn: 100,
            outputWhenOff: 0,
            hysteresis: 2,
            minOnTime: 0,
            minOffTime: 0,
            initialState: false);

        // lowerLimit = 21
        double measuredValue = 20.5;
        double deltaTime = 1.0;

        // Act
        double output = controller.CalculateOutput(measuredValue, deltaTime);

        // Assert
        Assert.True(controller.IsOn);
        Assert.Equal(100, output);
    }

    [Fact]
    public void CalculateOutput_ShouldRemainOff_WhenMeasuredValueIsInsideHysteresisBand()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            hysteresis: 2,
            initialState: false);

        // Banda: 21 a 23
        double measuredValue = 21.5;
        double deltaTime = 1.0;

        // Act
        double output = controller.CalculateOutput(measuredValue, deltaTime);

        // Assert
        Assert.False(controller.IsOn);
        Assert.Equal(0, output);
    }

    [Fact]
    public void CalculateOutput_ShouldTurnOff_WhenMeasuredValueIsAboveUpperLimit_AndMinOnTimeSatisfied()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            outputWhenOn: 100,
            outputWhenOff: 0,
            hysteresis: 2,
            minOnTime: 2,
            minOffTime: 0,
            initialState: true);

        // Primeiro passo: acumula tempo ligado, sem desligar ainda
        controller.CalculateOutput(22.0, 2.0);

        // upperLimit = 23
        double measuredValue = 23.5;
        double deltaTime = 1.0;

        // Act
        double output = controller.CalculateOutput(measuredValue, deltaTime);

        // Assert
        Assert.False(controller.IsOn);
        Assert.Equal(0, output);
    }

    [Fact]
    public void CalculateOutput_ShouldNotTurnOn_BeforeMinOffTime()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            hysteresis: 2,
            minOffTime: 5,
            initialState: false);

        // lowerLimit = 21
        double measuredValue = 20.0;

        // Act
        double output = controller.CalculateOutput(measuredValue, 2.0);

        // Assert
        Assert.False(controller.IsOn);
        Assert.Equal(0, output);
    }

    [Fact]
    public void CalculateOutput_ShouldTurnOn_AfterMinOffTime()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            hysteresis: 2,
            minOffTime: 5,
            initialState: false);

        double measuredValue = 20.0;

        // Act
        controller.CalculateOutput(measuredValue, 2.0);
        controller.CalculateOutput(measuredValue, 3.0);
        double output = controller.CalculateOutput(measuredValue, 0.1);

        // Assert
        Assert.True(controller.IsOn);
        Assert.Equal(100, output);
    }

    [Fact]
    public void CalculateOutput_ShouldThrowArgumentOutOfRangeException_WhenDeltaTimeIsNegative()
    {
        // Arrange
        var controller = new OnOffController(setpoint: 22);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            controller.CalculateOutput(measuredValue: 20, deltaTime: -1));
    }

    [Fact]
    public void Reset_ShouldTurnControllerOff()
    {
        // Arrange
        var controller = new OnOffController(
            setpoint: 22,
            hysteresis: 2,
            initialState: true);

        // Act
        controller.Reset();

        // Assert
        Assert.False(controller.IsOn);
    }
}