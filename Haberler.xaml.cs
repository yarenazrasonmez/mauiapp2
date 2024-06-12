
using System.Collections.ObjectModel;

namespace MauiApp2;

public partial class Haberler : ContentPage
{

    public ObservableCollection<HaberlerItem> FilteredNewsItems { get; set; }

    public Haberler()
	{
        InitializeComponent();

        FilteredNewsItems = new ObservableCollection<HaberlerItem>();

        Kategori defaultCategory = new Kategori();

        defaultCategory.Tittle = "Man�et";
        defaultCategory.Link = "https://www.trthaber.com/manset_articles.rss";

        GetRoot(defaultCategory);

        BindingContext = this;


        newsListView.ItemsSource = FilteredNewsItems;
    }

    public async Task GetRoot(Kategori kategori)
    {
        try
        {
            Root root = await HaberlerServis.GetNews(kategori);
            if (root != null)
            {
                Console.WriteLine("Status: " + root.status);
                

                FilteredNewsItems.Clear();
                foreach (var news in root.items)
                {

                    FilteredNewsItems.Add(new HaberlerItem
                    {
                        Title = news.title,
                        ImageUrl = news.enclosure.link,
                        PubDate = news.pubDate,
                        Author = news.author
                        
                    }); ;
                }

            }
            else
            {
                Console.WriteLine("Failed to fetch news.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void CategoryButton_Clicked(object sender, EventArgs e)
    {
        if (sender is Button clickedButton)
        {
            string categoryText = clickedButton.Text;


            clickedButton.BackgroundColor = Colors.Red; 


            


            Kategori selectCategory = new Kategori();

            for (int i = 0; i < Kategori.CategoryList.Count; i++)
            {
                if (Kategori.CategoryList[i].Tittle == categoryText)
                {
                    selectCategory.Tittle = Kategori.CategoryList[i].Tittle;
                    selectCategory.Link = Kategori.CategoryList[i].Link;
                }
            }
            

            GetRoot(selectCategory);
        }
    }
}