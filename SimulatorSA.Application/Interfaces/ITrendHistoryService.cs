//Responsavel por armazenar e servir a tendencia coletada pela HMI
// Responsável por armazenar e servir a tendência coletada pela HMI.
namespace SimulatorSA.Application.Interfaces;

using SimulatorSA.Application.DTOs;

public interface ITrendHistoryService
{
    void AddSample(TrendSampleDto sample);
    IReadOnlyList<TrendSampleDto> GetSamples();
    void Clear();
}