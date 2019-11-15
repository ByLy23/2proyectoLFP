class MyProgram
{
    static void Main(string[] args)
    {
        /*
        #################### Ejemplo comentario multiples lineas #################
        ####### Linea 1
        ####### Linea 2
        ####### Linea 3
        */
        int x, y; //Solo declaracion de dos variables enteras
        //Deberia ser 0
        Console.WriteLine(x); 
        //Deberia ser 0
        Console.WriteLine(y); 
        if(x==0){
             if(y==0){
                 Console.WriteLine("EUREKA!!");
                 x=50;
                 y=100;
            }else{
                Console.WriteLine("Y Esta mal :(, no deberia de ser: "+Y);
            }

        }else{
            Console.WriteLine("X Esta mal :(, no deberia de ser: "+x);
        }
        //Deberia ser 50
        Console.WriteLine(x);
        //Deberia ser 100
        Console.WriteLine(y);
        //Deberia de ser 190
        Console.WriteLine("(x+50-10/2)*2= 190 R://"+(x+50-10/2)*2);
        Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
        while(x>=180){
            Console.WriteLine("Iteracion no. "+x);
            x = x - 1 ;
        }
        int tamanioArreglo = 5;
        //El arreglo es {6,7,8,9,10}
        int[] arreglo = { tamanioArreglo+1, tamanioArreglo+2, tamanioArreglo+3,tamanioArreglo+4,tamanioArreglo+5 };
        //Arreglo1 de 5 posiciones {0,0,0,0,0}.
        int[] arreglo1 = new int[tamanioArreglo];
        Console.WriteLine(">>>>>>> Arreglo <<<<<<<<<<");
        for(int i=0;i<tamanioArreglo;i++){
            Console.WriteLine(arreglo[i]);
            arreglo1 = i;
        }
        Console.WriteLine(">>>>>>> Arreglo1 <<<<<<<<<<");
        int j = 0;
        while(j<tamanioArreglo){
            Console.WriteLine(arreglo1[j]);
            j = j +1;
        }
    }
}