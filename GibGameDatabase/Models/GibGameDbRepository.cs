namespace GibGameDatabase.Models;

public class GibGameDbRepository
{
    public GibGameDbRepository(GameDbContext ctx, HolScraper amiga, IgdbScraper igdb)
    {
        Amiga = amiga;
        Igdb = igdb;
        Context = ctx;
    }

    private GameDbContext Context { get; }
    private Random Random { get; } = new();
    private HolScraper Amiga { get; }
    private IgdbScraper Igdb { get; }

    public IEnumerable<GameEntry> GetAllGames()
    {
        return Context.GameEntries.ToList();
    }

    public IEnumerable<string> GetPlatforms()
    {
        return Context.GameEntries.Select(x => x.Platform).Distinct().OrderBy(y => y);
    }

    public IEnumerable<GameEntry> GetMultiPlatformGames()
    {
        var groups = Context.GameEntries.AsEnumerable().GroupBy(x => x.Name);
        var x = groups.Where(x => x.Count() > 1);
        var y = x.SelectMany(z => z.ToList());
        return y;
    }

    public IEnumerable<GameEntry> GetPlatformExclusiveGames()
    {
        var groups = Context.GameEntries.AsEnumerable().GroupBy(x => x.Name);
        var x = groups.Where(x => x.Count() == 1);
        var y = x.SelectMany(z => z.ToList());
        return y;
    }

    public async Task RebuildHol()
    {
        await Amiga.Scrape();
    }

    public async Task RebuildIgdb()
    {
        await Igdb.Scrape();
    }

    public GameEntry GetRandom(string platform)
    {
        var gameEntries = Context.GameEntries.ToList()
            .Where(x => (x.Languages.Contains("English") || !x.Languages.Any()) && x.DatePlayed == null);
        if (!platform.Equals("ALL"))
            gameEntries = gameEntries.Where(x => x.Platform.Equals(platform));
        var game = gameEntries.ElementAt(Random.Next(gameEntries.Count()));
        return game;
    }

    public async Task SetGamePlayed(int id)
    {
        var game = Context.GameEntries.FirstOrDefault(x => x.Id.Equals(id));
        if (game != null)
        {
            game.DatePlayed = DateTime.UtcNow;
            await Context.SaveChangesAsync();
        }
    }

    public async Task UnsetGamePlayed(int id)
    {
        var game = Context.GameEntries.FirstOrDefault(x => x.Id.Equals(id));
        if (game != null)
        {
            game.DatePlayed = null;
            await Context.SaveChangesAsync();
        }
    }
}