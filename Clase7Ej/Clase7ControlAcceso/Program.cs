using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;




namespace Clase7ControlAcceso
{
    class Program
    {
        static double rec = 0; // Variable estática para almacenar la recaudación acumulad
        static double totalConImpuestos = 0;
        static int cantAccesos = 0;
        static int accesos = 0;
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                MostrarMenu();
                opcion = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                VerificarOpcion(opcion);
            } while (opcion != 4);
        }
        #region // vista
        static public void MostrarMenu()
        {
            Console.WriteLine("===== Menú Principal =====\n1. Verificar acceso \n2. Imprimir recaudación \n3. Mostrar cantidad de accesos \n4. Salir \n");
            Console.WriteLine("==========================");
        }
        static public void ImprimirRecaudacion(double totalConImpuestos)
        {
            // Implementar lógica para imprimir recaudación
            Console.WriteLine("Funcionalidad en desarrollo...\n");

            rec += totalConImpuestos;
            Console.WriteLine("la recaudacion es de: {0:F2}", rec);
        }
        static public void MostrarCantidadDeAccesos()
        {
            // Implementar lógica para mostrar la cantidad de accesos
            Console.WriteLine("Funcionalidad en desarrollo...");
            Console.WriteLine("cantidad de accesos registrados: {0}", cantAccesos);
        }
        #endregion
        #region // Dominio
        static public void VerificarOpcion(int opcion)
        {
            switch (opcion)
            {
                case 1:
                    RegistrarAcceso();
                    break;
                case 2:
                    ImprimirRecaudacion(totalConImpuestos);
                    break;
                case 3:
                    MostrarCantidadDeAccesos();
                    break;
                case 4:
                    Console.WriteLine("Proceso terminado");
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        }
        static public void RegistrarAcceso()
        {
            Console.WriteLine("|Registrar Acceso|\n");
            Console.WriteLine("Tiene ticket? 1/ tiene   2/No tiene");
            int ticket = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (ticket == 1)
            {
                Console.WriteLine("Acceso permitido.");
                accesos++;
            }
            else
            {
                GenerarTicket();
                accesos++;
            }
            cantAccesos += accesos;
        }
        static public void GenerarTicket()
        {
            double total = 0;
            
            int dias = 0;
            
          
            Console.WriteLine("Necesita generar un ticket.\n");
            Console.WriteLine("Ingrese cantidad de vehículos:");
            int cantidadVehiculos = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            
            for (int i = 0; i < cantidadVehiculos; i++)
            {
                total += SeleccionarTipoVehiculo();
                Console.Clear();
            }
            Console.WriteLine("Ingrese cantidad de días(1 - 10):");
            dias = Convert.ToInt32(Console.ReadLine());
            total = AplicarPorcentaje(total, dias);
            double totalConImpuestos = AplicarImpuestos(total);
            rec += totalConImpuestos;

            Console.WriteLine($"Total a pagar por {dias} días y {cantidadVehiculos} vehículos: {total:F2} + impuestos: {totalConImpuestos:F2}.");
            Console.Write("Pulse una tecla para seguir...");
            Console.ReadKey();
            Console.Clear();
        }
        static public void TomarIdentificador()
        {
   
        }
        static public double AplicarPorcentaje(double total, int dias)
        {
           
            double valor;
            switch (dias)
            {
                case 1:
                    valor = total*1+total;
                    break;
                case 2:
                    valor = total*2.20+total;
                    break;
                case 3:
                    valor = total * 3.20 + total;
                    break;
                case 4:
                    valor = total * 4.20 + total;
                    break;
                default:
                    valor = total * 4.80 + total;  // 380% adicional (para 5-10 días)
                    break;
            }
            Debug.WriteLine("valor de porcentaje solo con impuesto de dias: {0:F2}",valor);
    
            AplicarImpuestos(valor);
            return valor;
        }
        static public double AplicarImpuestos(double valor)
        {
            double iva = valor * 0.21;
            Debug.WriteLine("el recargo de iva: {0:F2}",iva);
            double impuestoAconcagua = valor * 0.15;
            double totalConImpuesto=  valor + iva + impuestoAconcagua;
            return totalConImpuesto;
        }
        static public double SeleccionarTipoVehiculo()
        {
            Console.WriteLine("===== Tipo de vehículo ==============\n1. Sin vehículo  \t 4. Camioneta  \n2. Moto  \t \t 5. Buggy  \n3. Auto  \t \t 6. Vehículo náutico ");
            Console.WriteLine("=====================================");
            int opcion = Convert.ToInt32(Console.ReadLine());

            double valor = 0;
            switch (opcion)
            {
                case 1:
                    valor = 100;
                    break;
                case 2:
                    valor = 800;
                    break;
                case 3:
                    valor = 1000;
                    break;
                case 4:
                    valor = 1500;
                    break;
                case 5:
                    valor = 5000;
                    break;
                case 6:
                    valor = 1200;
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
            return valor;
        }
        #endregion
    }
}