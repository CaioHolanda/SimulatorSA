## Purpose

SimulatorSA is a simplified building thermal simulation engine designed to model:

- Room temperature dynamics
- Control strategies (PID, On/Off, etc.)
- Actuator behavior (valves, fans)
- Environmental perturbations

The goal is to evolve into a small-scale digital twin for HVAC control experimentation.

## Core Architecture

The system is divided into the following main components:

### Models
Represent physical and logical entities:

- Room → thermal state and behavior
- Actuator → abstract actuator base class
- AnalogicSensor → sensor representation

### Spaces
Specializations of Room:

- OfficeA → specific parameters and perturbations

### Services
Contain logic and algorithms:

- PidController → control logic
- OnOffController → alternative control strategy

### Actuators
Represent control devices:

- ValveActuator → converts control signal into opening %

### SimulationData
Store simulation outputs:

- SimulationResult
- SimulationSnapshot
- TimeSeries
- TimeValuePoint

## Simulation Flow

At each simulation time step:

1. Apply perturbations (e.g., people entering, equipment on)
2. Read current temperature from Room
3. Controller calculates control signal (0–100%)
4. Actuator applies constraints (e.g., clamping)
5. Control signal is converted into heating effect
6. Room temperature is updated:
   - Apply heating effect
   - Apply thermal losses
7. Store results for analysis

## Thermal Model

The room temperature evolves according to:

T_next = T_current 
         + heating_effect 
         - thermal_loss

Where:

thermal_loss = (T_current - T_outdoor) * lossCoefficient

## Responsibilities

Room:
- Stores temperature
- Applies temperature changes
- Applies thermal losses

Controller:
- Calculates control output based on error

Actuator:
- Translates control signal into physical behavior

Program / SimulationService:
- Orchestrates simulation loop

## Extensibility

Controllers should implement a common interface:

IController:
- double CalculateOutput(double actualValue)

This allows comparison between:
- PID
- On/Off
- Future strategies

## Next Steps

- Introduce SimulationService to encapsulate loop
- Store results using SimulationResult
- Add plotting (ScottPlot)
- Introduce thermal inertia (capacity)
- Add humidity and CO2 models