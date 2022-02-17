namespace GibGameDatabase.Models;

public class GameEntry
{
    public int Id { get; set; }
    public int? HolId { get; set; }
    public string? IgdbGameAndPlatformId { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public int NoOfDisks { get; set; }
    public List<string> Hardware { get; set; }
    public List<string> Languages { get; set; }
    public string BoxImage { get; set; }
    public string Platform { get; set; }
    public string? IgdbUrl { get; set; }
    public string HardwareEntries => string.Join(", ", Hardware ?? new List<string>());
    public string LanguagesEntries => string.Join(", ", Languages ?? new List<string>());

    public string Url
    {
        get
        {
            if ((HolId ?? 0) > 0)
                return $"http://hol.abime.net/{HolId}";
            return IgdbUrl ?? "";
        }
    }

    public DateTime? DatePlayed { get; set; }
}

public class FailedEntry
{
    public int Id { get; set; }
    public int HolId { get; set; }
}