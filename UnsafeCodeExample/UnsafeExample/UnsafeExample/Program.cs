using System;
using System.Drawing;

namespace UnsafeExample
{
    class Program
    {
		//операторы указателей описаны тут:
		//https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/operators/pointer-related-operators

		//про неуправляемые типы:
		//https://docs.microsoft.com/ru-ru/dotnet/csharp/language-reference/builtin-types/unmanaged-types

		static void Main(string[] args)
	    {
		    Point aPoint = new Point();         //определяем переменную из
                                                //пространства System.Drawing (Это структура)

			MyPoint bPoint = new MyPoint();		//определяем переменную из нашего
                                                //пространства UnsafeExample (Это класс)
		    
		    unsafe //сообщаем, что работаем с небезопасным участком кода
		    {

                Point* point = &aPoint;
				//присваиваем указателю значение точки А
				//это возможно, т.к. мы работаем со структурой,
				//в которой инкапсулированы поля с unmanaged типами данных

				//Point* myBPoint = &bPoint;
				//Этот код не скомпилируется, т.к.
				//Point - это ссылочный тип


				Console.WriteLine("Знaчeниe по указанному адресу: " + *point);
				//выводим на консоль значение точки А
				//значения координат будут равны 0
				//здесь мы обращаемся к данным

				

				Console.WriteLine("Адрес: " + (long) point);
			    //выводим на консоль адрес указателя point

			    point->X = 10; //заполняем поля объекта
			    point->Y = 10; //и выводим их на консоль

			    Console.WriteLine("Координаты точки А: {0}, {1}",
                    point->X, point->Y);


                //------------------------------------------------------------//

			    fixed (int* pointX = &bPoint.X)
                //"защищаем" указатель от уборщика мусора
                //в данном случае работа идет с полями класса
			    {
                    
				    *pointX = 1;
					//fixed (int* pointY = &bPoint.Y) { *pointY = 1; }
					//fixed (int* pointY = &aPoint.Y) { *pointY = 1; }
					//любой из этих вариантов не скомпилируется
					//т.к. происходит работа со свойствами
					//https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0211

				}

				int* numbers = stackalloc int[] { 0, 1, 2, 3, 4, 5 };
				int* p1 = &numbers[1];
				int* p2 = &numbers[5];
				Console.WriteLine(p2 - p1);
				//stackalloc выделяет блок памяти в стеке
				//его не нужно помещать в блок fixed

				//без stackalloc или fixed код ниже не скомпилируется
				//int[] array = { 10, 20, 5, 2, 54, 9 };
				//int* arr_ptr = &array[0];

				//------------------------------------------------------------//

				var intSize = sizeof(MyStruct*);
				Console.WriteLine(intSize);
				//Этот код позволит определить, сколько конкретный тип занимает
				//места в памяти

				//For struct types, that value includes any padding,
                //as the preceding example demonstrates
			}
		}
	    public class MyPoint
	    {
			public int X;
		    public int Y { get; set; }

		}

        public struct MyStruct
        {
			public int A;
			public long B;
			public Point C;
		}
    }
}
