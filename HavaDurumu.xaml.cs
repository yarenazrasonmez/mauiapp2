using System.Collections.ObjectModel;

namespace MauiApp2;

public partial class HavaDurumu : ContentPage
{
    private List<string> cities = new List<string>();
    public ObservableCollection<SehirModel> ImageList { get; set; } = new ObservableCollection<SehirModel>();


    public HavaDurumu()

    {
        InitializeComponent();


        ImageCollection.ItemsSource = ImageList;
    }

    public void OnButtonClick(object sender, EventArgs e)
    {

        a();
        
    }

    public async Task a()
    {
        string sehir = await DisplayPromptAsync("Þehir:", "Þehir ismi", "OK", "Cancel");
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('Ç', 'C');
        sehir = sehir.Replace('Ð', 'G');
        sehir = sehir.Replace('Ý', 'I');
        sehir = sehir.Replace('Ö', 'O');
        sehir = sehir.Replace('Ü', 'U');
        sehir = sehir.Replace('Þ', 'S');
        ImageList.Add(new SehirModel { Name = sehir });

        string src = new SehirModel { Name = sehir }.Source;

        Console.WriteLine(src);
    }


    private void ImageCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            string selectedImageUrl = e.CurrentSelection[0] as string;
            Console.WriteLine(selectedImageUrl);

            if (!string.IsNullOrEmpty(selectedImageUrl))
            {
                DisplayAlert("Seçilen Resim", $"Seçilen Resim: {selectedImageUrl}", "Tamam");
            }

            ImageCollection.SelectedItem = null;
        }
    }
}