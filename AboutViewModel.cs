namespace APPR6312PART2.Models
{
    public class AboutViewModel
    {
        public string Mission { get; set; }
        public string Vision { get; set; }
        public string History { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Achievement> Achievements { get; set; }
    }

    public class TeamMember
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    public class Achievement
    {
        public string Year { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}