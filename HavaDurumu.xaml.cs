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
        string sehir = await DisplayPromptAsync("�ehir:", "�ehir ismi", "OK", "Cancel");
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('�', 'C');
        sehir = sehir.Replace('�', 'G');
        sehir = sehir.Replace('�', 'I');
        sehir = sehir.Replace('�', 'O');
        sehir = sehir.Replace('�', 'U');
        sehir = sehir.Replace('�', 'S');
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
                DisplayAlert("Se�ilen Resim", $"Se�ilen Resim: {selectedImageUrl}", "Tamam");
            }

            ImageCollection.SelectedItem = null;
        }
    }
}