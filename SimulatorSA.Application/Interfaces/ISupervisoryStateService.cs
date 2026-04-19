// Responsavel por fornecer a HMI uma visao simplificada de supervisao do estado atual

namespace SimulatorSA.Application.Interfaces;

using SimulatorSA.Application.DTOs;

public interface ISupervisoryStateService
{
    SupervisoryStateDto GetCurrentState();
}