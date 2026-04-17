// Responsavel por transformar o estado atual em uma amostra de tendencia

namespace SimulatorSA.Application.Interfaces;

using SimulatorSA.Application.DTOs;

public interface IHmiPollingService
{
    TrendSampleDto CollectSample();
}