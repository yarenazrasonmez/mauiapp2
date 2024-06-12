using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace MauiApp2;

public class ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string yon)
        {
            if (yon.Contains("up"))
                return "yukari.png";
            else if (yon.Contains("down"))
                return "asagi.png";
            else if (yon.Contains("sabit"))
                return "sabit.png";
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public partial class Kurlar : ContentPage
{
	public Kurlar()
	{
		InitializeComponent();

	}
    private static Kurlar instance;

    private string CalculateFark(string alis, string satis)
    {
        if (decimal.TryParse(satis, out decimal satisValue) && decimal.TryParse(alis, out decimal alisValue))
        {
            decimal fark = satisValue - alisValue;
            return fark.ToString("0.00");
        }
        return "0.00";
    }
    public static Kurlar Page
    {
        get
        {
            if (instance == null)
                instance = new Kurlar();
            return instance;
        }
    }
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Load();
    }

    AltinDoviz kurlar;
    async Task Load()
    {
        string jsondata = await GetDovizKurlarý();
        kurlar = JsonSerializer.Deserialize<AltinDoviz>(jsondata);
        DovizListe.ItemsSource = new List<DovizK>()
        {
            new DovizK()
            {
            Dname = "Dolar",
            FAlis = kurlar.USD.alis,
            FSatis = kurlar.USD.satis,
            Fark = CalculateFark(kurlar.USD.alis, kurlar.USD.satis),
            Yon = GetImage(kurlar.USD.d_yon),
            },
            new DovizK()
            {
            Dname = "Euro",
            FAlis = kurlar.EUR.alis,
            FSatis = kurlar.EUR.satis,
            Fark = CalculateFark(kurlar.EUR.alis, kurlar.EUR.satis),
            Yon = GetImage(kurlar.EUR.d_yon),
            },
            new DovizK()
            {
            Dname = "Pound",
            FAlis = kurlar.GBP.alis,
            FSatis = kurlar.GBP.satis,
            Fark = CalculateFark(kurlar.GBP.alis, kurlar.GBP.satis),
            Yon = GetImage(kurlar.GBP.d_yon),
            },
            new DovizK()
            {
            Dname = "GramAltýn",
            FAlis = kurlar.GA.alis,
            FSatis = kurlar.GA.satis,
            Fark = CalculateFark(kurlar.GA.alis, kurlar.GA.satis),
            Yon = GetImage(kurlar.GA.d_yon),
            },
            new DovizK()
            {
            Dname = "ÇeyrekAltýn",
            FAlis = kurlar.C.alis,
            FSatis = kurlar.C.satis,
            Fark = CalculateFark(kurlar.C.alis, kurlar.C.satis),
            Yon = GetImage(kurlar.C.d_yon),
            },
            new DovizK()
            {
            Dname = "GAG",
            FAlis = kurlar.GAG.alis,
            FSatis = kurlar.GAG.satis,
            Fark = CalculateFark(kurlar.GAG.alis, kurlar.GAG.satis),
            Yon = GetImage(kurlar.GAG.d_yon),
            },
            new DovizK()
            {
            Dname = "BTC",
            FAlis = kurlar.BTC.alis,
            FSatis = kurlar.BTC.satis,
            Fark = CalculateFark(kurlar.BTC.alis, kurlar.BTC.satis),
            Yon = GetImage(kurlar.BTC.d_yon),
            },
            new DovizK()
            {
            Dname = "ETH",
            FAlis = kurlar.ETH.alis,
            FSatis = kurlar.ETH.satis,
            Fark = CalculateFark(kurlar.ETH.alis, kurlar.ETH.satis),
            Yon = GetImage(kurlar.ETH.d_yon),
            },
            new DovizK()
            {
            Dname = "XU100",
            FAlis=kurlar.XU100.alis,
            FSatis=kurlar.XU100.satis,
            Fark=CalculateFark(kurlar.XU100.alis, kurlar.XU100.satis),
            }

        };
        foreach (DovizK doviz in DovizListe.ItemsSource)
        {
            if (decimal.TryParse(doviz.FSatis, out decimal satis) && decimal.TryParse(doviz.FAlis, out decimal alis))
            {
                if (satis > alis)
                    doviz.Yon = "up"; 
                else if (alis > satis)
                    doviz.Yon = "down"; 
                else
                    doviz.Yon = "sabit"; 
            }
            else
            {
                doviz.Yon = ""; 
            }
        }
    

    }

    private string GetImage(string str)
    {
        if (str.Contains("up"))
            return "yukari.png";
        if (str.Contains("down"))
            return "asagi.png"; 
        if (str.Contains("sabit"))
            return "sabit.png";
        return "";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Load();
    }

    async Task<string> GetDovizKurlarý()
    {
        string url = "https://api.genelpara.com/embed/altin.json";
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
    }

    public class DovizK
    {
        public string Dname { get; set; }
        public string FAlis { get; set; }
        public string FSatis { get; set; }
        public string Fark { get; set; }
        public string Yon { get; set; }

    }
}



public class AltinDoviz
{
    public USD USD { get; set; }
    public EUR EUR { get; set; }
    public GBP GBP { get; set; }
    public GA GA { get; set; }
    public C C { get; set; }
    public GAG GAG { get; set; }
    public BTC BTC { get; set; }
    public ETH ETH { get; set; }
    public XU100 XU100 { get; set; }
}
public class BTC
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class C
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class ETH
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class EUR
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GA
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GAG
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class GBP
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}



public class USD
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
    public string d_oran { get; set; }
    public string d_yon { get; set; }
}

public class XU100
{
    public string satis { get; set; }
    public string alis { get; set; }
    public string degisim { get; set; }
}
