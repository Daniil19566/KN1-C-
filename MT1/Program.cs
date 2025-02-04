class Program
{
    private static int counter = 0;
    private static object lockObject = new object();

    private static int[] ar = new int[100000];

    private static int sum = 0;
    static void Main(string[] args)
    {
        var rand = new Random();
        for (int i = 0; i < ar.Length; i++)
        {
            ar[i] = rand.Next(0,100);
        }
        Thread thread1 = new Thread(sumEl);
        Thread thread2 = new Thread(sumEl);

        Otrezok otr1 = new Otrezok(0, 50000);
        Otrezok otr2 = new Otrezok(50000, 100000);
        thread1.Start(otr1);
        thread2.Start(otr2);

        thread1.Join();
        thread2.Join();

        Console.WriteLine($"Final sum value: {sum}");
    }

    static void sumEl(object? obj)
    {
        
        if (obj is Otrezok otr)
            for (int i = otr.Begin; i < otr.End; i++)
            {
                lock (lockObject)
                {
                   sum += ar[i];
                }
            }
       
    }

    record class Otrezok(int Begin, int End);
}