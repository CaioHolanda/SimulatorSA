/*
 Essa interface será usada pelo SimulatorSA.Bacnet quando o YABE ou outro cliente BACnet quiser:
        mudar o setpoint;
        ligar/desligar o controlador;
        trocar entre PID e On-Off.
 */
namespace SimulatorSA.Application.Interfaces;

public interface ISimulationCommandService
{
    void SetSetpoint(double value);
    void SetControllerEnabled(bool enabled);
    void UsePidController();
    void UseOnOffController();
}