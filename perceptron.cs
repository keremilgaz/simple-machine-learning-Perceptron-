using System;

namespace perceptron2
{
    class neuron
    {
        string neuron_name;
        double weight1;
        double weight2;
        double number_trues = 0;
        double number_falses = 0;
        const double LAMBDA = 0.05;

        public neuron(string neuron_name) // neuron oluşturan constructor method.
        {
            this.neuron_name = neuron_name; // neuronun ismi
            weight1 = GetRandomNumber(-1,1); // 1 ve -1 arasında oluşturulan rastgele weight değerleri.
            weight2 = GetRandomNumber(-1, 1);

        }

        public double GetRandomNumber(double minimum, double maximum) // random double değeri oluşturmak için kurulan method.
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public void training(double x1, double x2, double target) // neuronların ağırlıklarını verilen target değerlerine göre değiştiren method.
        {
            x1 = x1 / 10;
            x2 = x2 / 10;
            double output = x1 * weight1 + x2 * weight2; // output hesaplama.
            if (output < 0.5)
            {
                output = -1;
            }
            else
            {
                output = 1;
            }
            
            if (output != target) // eğer output target tan farklı ise ağırlıkların değerleri değiştirildi.
               {
                   weight1 += LAMBDA * (target - output) * x1;
                   weight2 += LAMBDA * (target - output) * x2;
               }          
        }




        public void truth_accucary(double x1, double x2, double target) // verilen target değerlerine göre kaç doğru ve kaç yanlış output çıktığını hesaplayan metod.
        {
            x1 = x1 / 10;
            x2 = x2 / 10;
            double output = x1 * weight1 + x2 * weight2;
            if (output < 0.5)
            {
                output = -1;
            }
            else
            {
                output = 1;
            }
            if (target == output)
            {
                number_trues += 1;
            }

            else
            {
                number_falses += 1;
            }

        }

        public void yazdir()
        {
            double accucary_rate = 100*(number_trues / (number_trues + number_falses));

            Console.WriteLine("Doğruluk yüzdesi: %" + accucary_rate);
            truth_accucary_sifirla();
        }

        public void truth_accucary_sifirla() 
        {
            number_trues = 0;
            number_falses = 0;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            neuron a = new neuron("a");

            int[,] my_array = new int[,] { { 6, 5, 1}, { 2, 4, 1}, { -3, -5, -1}, { -1, -1, -1}, // pdf te verilen 8 adet veri.
                                        { 1, 1, 1 }, { -2, 7, 1 }, { -4, -2, -1 }, { -6, 3, -1 } };

            for (int t = 0; t < (my_array.Length / 3); t++) // verilen 8 adet verinin hiçbir işlem yapılmadan önceki doğruluk değeri
            {
                a.truth_accucary(my_array[t, 0], my_array[t, 1], my_array[t, 2]);
            }
            Console.WriteLine("PDF te verilen 8 verinin hiçbir işlem yapılmadan önceki yüzdesi;");
            a.yazdir();

            for (int i = 0; i < 10; i++) // ağrılık değiştirme işlemleri burda başlıyor(10 epok.).
            {
                for(int t= 0; t < (my_array.Length / 3); t++)
                {
                    a.training(my_array[t,0],my_array[t,1], my_array[t, 2]);
                }
            }

            for (int t = 0; t < (my_array.Length / 3) ; t++) // eğitim yapıldıktan sonraki doğruluk değerleri hesaplanıyor.
             {
                 a.truth_accucary(my_array[t, 0], my_array[t, 1], my_array[t, 2]);
             }
            Console.WriteLine("PDF te verilen 8 verinin 10 epok sonraki yüzdesi;");
            a.yazdir();

            for (int i = 0; i < 90; i++) // ağrılık değiştirme işlemleri burda başlıyor(100 epok).for un 90 kere dönmesinin sebebi zaten bir önceki eğitim işleminde 10 kere eğtim yapılmıştı.
            {
                for (int t = 0; t < (my_array.Length / 3); t++)
                {
                    a.training(my_array[t, 0], my_array[t, 1], my_array[t, 2]);
                }
            }

            for (int t = 0; t < (my_array.Length / 3); t++) // eğitim yapıldıktan sonraki doğruluk değerleri hesaplanıyor.
            {
                a.truth_accucary(my_array[t, 0], my_array[t, 1], my_array[t, 2]);
            }

            Console.WriteLine("PDF te verilen 8 verinin 100 epok sonraki yüzdesi;");
            a.yazdir();

            //********************************************************************************************************************//


            int[,] test_array = new int[,] { { -4, 5, 1}, { 1, 3, 1}, { 3, 3, -1}, { 1, -1, -1}, { 5, 4, -1 } }; // 5 adet veri test nöronuna gönderilecek.

            for (int t = 0; t < (test_array.Length / 3); t++) // verilen 5 adet verinin teste sokulması.
            {
                a.truth_accucary(test_array[t, 0], test_array[t, 1], test_array[t, 2]);
            }
            Console.WriteLine();
            Console.WriteLine("test Listesinin başarı yüzdesi;");
            a.yazdir();




        }
    }
}
