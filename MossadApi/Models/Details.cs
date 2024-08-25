namespace MossadApi.Models
{
    public class Details
    {
        public int Total_Agents { get; set; }
        public int Active_Agents { get; set; }
        public int Total_Targets { get; set; }
        public int Dead_Targets { get; set; }
        public int Total_Missions { get; set; }
        public int Active_Missions { get; set; }
        public double Ratio_Agents_To_Taegets { get; set; }
        public double Possibal_Agents_To_Targets { get; set; }

    }
}
