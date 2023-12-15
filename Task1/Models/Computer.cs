namespace Program.Models {

public class Computer {
    public string Motherboard { get; set; }
    public bool HasWifi { get; set; }
    public bool HasLTE { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public string VideoCard { get; set; }

    // Constructor
    public Computer(string motherboard, bool hasWifi, bool hasLTE, DateTime releaseDate, decimal price, string videoCard)
    {
        Motherboard = motherboard;
        HasWifi = hasWifi;
        HasLTE = hasLTE;
        ReleaseDate = releaseDate;
        Price = price;
        VideoCard = videoCard;
    }

    // Methods
    

    public void PrintComputerDetails()
    {
        Console.WriteLine($"Motherboard: {Motherboard}");
        Console.WriteLine($"Has Wifi: {HasWifi}");
        Console.WriteLine($"Has LTE: {HasLTE}");
        Console.WriteLine($"Release Date: {ReleaseDate}");
        Console.WriteLine($"Price: {Price}");
        Console.WriteLine($"Video Card: {VideoCard}");
    }
}
}