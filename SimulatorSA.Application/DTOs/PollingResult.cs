using SimulatorSA.Application.DTOs;

namespace SimulatorSA.Application.DTOs;

public class PollingResult
{
    public bool HasSample { get; set; }
    public bool RecoveredFromBuffer { get; set; }
    public bool GapDetected { get; set; }

    public int ExpectedSequenceNumber { get; set; }
    public int? ActualSequenceNumber { get; set; }

    public string Status { get; set; } = string.Empty;

    public TrendSampleDto? Sample { get; set; }
}