namespace MyProFile.Server.DTOs;

public class StudentOverviewDto
{
    public StudentDto Student { get; set; } = new StudentDto(); 

    public List<CreditDto> Credits { get; set; } = new List<CreditDto>();

    public List<AchievementDto> Achievements { get; set; } = new List<AchievementDto>();

    public List<GoalDto> Goals { get; set; } = new List<GoalDto>();

    public List<EventDto> Events { get; set; } = new List<EventDto>();

    public List<SanctionDto> Sanctions { get; set; } = new List<SanctionDto>();

    public List<InterestDto> Interests { get; set; } = new List<InterestDto>();
}
