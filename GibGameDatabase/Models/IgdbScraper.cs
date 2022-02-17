using IGDB;
using IGDB.Models;

namespace GibGameDatabase.Models;

public class IgdbScraper
{
    private const string NoImage =
        "data:image/jpeg;base64,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";

    public IgdbScraper(GameDbContext ctx, IConfiguration iConfig)
    {
        Context = ctx;
        Configuration = iConfig;
        Client = new IGDBClient(
            Configuration.GetValue<string>("IGDBSettings:TwitchClient"),
            Configuration.GetValue<string>("IGDBSettings:TwitchSecret"));
    }

    private GameDbContext Context { get; }
    private IConfiguration Configuration { get; }
    private IGDBClient Client { get; }

    private Dictionary<long, string> PlatForms { get; } = new()
    {
        {24, "GBA"},
        {20, "NDS"},
        {4, "N64"},
        {19, "SNES"},
        {30, "SEGA32X"},
        {29, "MEGADRIVE"},
        {78, "SEGACD"},
        {32, "SEGASATURN"},
        {159, "NDSi"},
        {7, "PSX"},
        {23, "DREAMCAST"}
    };

    public async Task Scrape()
    {
        try
        {
            var update = true;
            //    context.GameEntries.RemoveRange(context.GameEntries.Where(x => x.IgdbGameAndPlatformId != null));
            //    await context.SaveChangesAsync();

            //    var totalCountQuery = $"where platforms=({string.Join(",", (Enum.GetValues<IgdbPlatformEnum>()).Select(x => ((int)x).ToString()))});";
            //    var total = await _client.QueryAsync<dynamic>("games/count", totalCountQuery);

            var offset = 0;
            var platformsQuery = $"where platforms=({string.Join(",", PlatForms.Keys)});";
            var moreResults = true;

            while (moreResults)
            {
                var query =
                    $"fields id,name,url,platforms.*,release_dates.platform,artworks.*,cover.*,release_dates.*; {platformsQuery} limit 500; offset {offset};";
                var games = await Client.QueryAsync<Game>(IGDBClient.Endpoints.Games, query);

                if (games.Any())
                {
                    offset += 500;
                    foreach (var game in games)
                    {
                        string boxImage;
                        if (game.Cover?.Value?.ImageId != null)
                            boxImage = await GetImageAsBase64Url(ImageHelper.GetImageUrl(game.Cover.Value.ImageId,
                                ImageSize.CoverBig));
                        else if (game.Artworks?.Values?.FirstOrDefault()?.ImageId != null)
                            boxImage = await GetImageAsBase64Url(
                                ImageHelper.GetImageUrl(game.Artworks.Values.First().ImageId, ImageSize.CoverBig));
                        else
                            boxImage = NoImage;

                        foreach (var platform in game.Platforms.Values.Where(x => PlatForms.ContainsKey(x.Id ?? 0)))
                        {
                            var existing = Context.GameEntries.FirstOrDefault(x =>
                                x.IgdbGameAndPlatformId.Equals($"{game.Id}:{platform.Id}"));
                            if (existing == null)
                            {
                                var newGame = new GameEntry
                                {
                                    Name = game.Name,
                                    ReleaseYear = game.ReleaseDates.Values
                                        .FirstOrDefault(x => x.Platform.Id.Equals(platform.Id))?.Year ?? 0,
                                    Languages = new List<string> {"English"},
                                    IgdbGameAndPlatformId = $"{game.Id}:{platform.Id}",
                                    BoxImage = boxImage,
                                    Hardware = new List<string>(),
                                    NoOfDisks = 1,
                                    //HolId = holId,
                                    Platform = platform.Name ?? "Unknown",
                                    IgdbUrl = game.Url
                                };

                                Context.GameEntries.Add(newGame);
                                await Context.SaveChangesAsync();
                                Console.WriteLine(
                                    $"Added Game Entry - {game.Name}, Platform - {platform.Name ?? "Unknown"} ");
                            }
                            else
                            {
                                if (update)
                                {
                                    existing.Name = game.Name;
                                    existing.ReleaseYear = game.ReleaseDates.Values
                                        .FirstOrDefault(x => x.Platform.Id.Equals(platform.Id))?.Year ?? 0;
                                    existing.Languages = new List<string> {"English"};
                                    existing.IgdbGameAndPlatformId = $"{game.Id}:{platform.Id}";
                                    existing.BoxImage = boxImage;
                                    existing.Hardware = new List<string>();
                                    existing.NoOfDisks = 1;
                                    //HolId = holId,
                                    existing.Platform = platform.Name ?? "Unknown";
                                    existing.IgdbUrl = game.Url;

                                    await Context.SaveChangesAsync();
                                    Console.WriteLine(
                                        $"Updated Game Entry - {game.Name}, Platform - {platform.Name ?? "Unknown"} ");
                                }
                                else
                                {
                                    Console.WriteLine($"{game.Id}:{platform.Id} Already processed");
                                }
                            }
                        }
                    }
                }
                else
                {
                    moreResults = false;
                }
            }
        }
        catch (Exception ex)
        {
            var kjh = ex.Message;
        }
    }

    private async Task<string> GetImageAsBase64Url(string url)
    {
        using (var httpClient = new HttpClient())
        {
            var bytes = await httpClient.GetByteArrayAsync($"https:{url}");
            return "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
        }
    }
}