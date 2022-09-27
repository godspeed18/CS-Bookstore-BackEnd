namespace ITPLibrary.Application.Features.Books.ViewModels
{
    public class RecentlyAddedAndPopularBookVm
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int Price { get; set; }

        public byte[]? Thumbnail { get; set; }

        public bool RecentlyAdded { get; set; }

        public bool Popular { get; set; }

    }
}
