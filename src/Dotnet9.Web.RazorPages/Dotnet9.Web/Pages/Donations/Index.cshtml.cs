namespace Dotnet9.Web.Pages.Donations;

public class IndexModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly IDonationRepository _repository;

    public IndexModel(IDonationRepository repository,
        IDistributedCacheHelper cacheHelper)
    {
        _repository = repository;
        _cacheHelper = cacheHelper;
    }

    public string? ContentHtml { get; set; }

    public async Task OnGet()
    {
        string cacheKey = "Privacy";

        async Task<string?> GetDataFromDb()
        {
            var data = await _repository.GetAsync();
            return data?.Content.Convert2Html();
        }

        ContentHtml = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetDataFromDb());
    }
}