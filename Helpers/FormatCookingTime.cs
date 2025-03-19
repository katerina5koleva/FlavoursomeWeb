namespace FlavoursomeWeb.Helpers
{
    public class FormatCookingTime
    {
        public static string FormatTime(int totalMinutes)
        {
            if (totalMinutes < 60)
                return $"{totalMinutes} min";

            int hours = totalMinutes / 60;
            int minutes = totalMinutes % 60;

            return minutes == 0 ? $"{hours} hr" : $"{hours} hr {minutes} min";
        }
    }
}
