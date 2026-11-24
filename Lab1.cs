using System;

class Motor
{
    public string Model { get; set; }
    public int BeygirGucu { get; set; }

    public Motor(string model, int beygirGucu)
    {
        Model = model;
        BeygirGucu = beygirGucu;
    }

    public void Calistir()
    {
        Console.WriteLine($"{Model} motoru calisti ({BeygirGucu} HP)");
    }
}

class Araba
{
    public string Marka { get; set; }

    // Composition: Araba'nin bir Motor nesnesi var
    private Motor motor;

    public Araba(string marka, string motorModeli, int beygirGucu)
    {
        Marka = marka;
        motor = new Motor(motorModeli, beygirGucu);
    }

    public void Calistir()
    {
        Console.WriteLine($"{Marka} araba calistiriliyor");
        motor.Calistir();
    }

    public void BilgileriGoster()
    {
        Console.WriteLine($"Marka: {Marka}");
        Console.WriteLine($"Motor: {motor.Model}");
        Console.WriteLine($"Beygir Gucu: {motor.BeygirGucu}");
    }
}

class Program
{
    static void Main()
    {
        Araba araba = new Araba("BMW", "N20", 245);

        araba.BilgileriGoster();

        Console.WriteLine();

        araba.Calistir();
    }
}

// Bu iliski COMPOSITION'dir (has-a): Araba'nin bir Motor'u vardir.
// Inheritance (is-a) olsaydi "Araba bir Motor'dur" demek gerekirdi, bu mantiksiz.