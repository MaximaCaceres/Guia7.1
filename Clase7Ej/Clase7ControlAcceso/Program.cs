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
        static public string[] matricula = new string[300];
        static int indiceMatriculas = 0;
        static int pos;
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                MostrarMenu();
                opcion = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                VerificarOpcion(opcion, matricula, pos);
            } while (opcion != 6);
        }
        #region // vista
        static public void MostrarMenu()
        {
            Console.WriteLine("===== Menú Principal =====\n1. Verificar acceso \n2. Imprimir recaudación \n3. Mostrar cantidad de accesos \n4. Mostrar matriculas \n5.Buscar Matricula\n6. Salir");
            Console.WriteLine("==========================");
        }
        static public void ImprimirRecaudacion(double totalConImpuestos)
        {
            Console.WriteLine("la recaudacion es de: {0:F2}", rec);
        }
        static public void MostrarCantidadDeAccesos()
        {
            Console.WriteLine("cantidad de accesos registrados: {0}", cantAccesos);
        }
        #endregion
        #region // Dominio
        static public void VerificarOpcion(int opcion, string[] matriculas, int pos)
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
                    OrdenarMatriculas();
                    MostrarMatricula();
                    break;
                case 5:
                    string matriculaBuscada;
                    Console.WriteLine("Ingrese matricula a encontrar");
                    matriculaBuscada = Console.ReadLine();
                    pos = BuscarMatricula(matriculas, matriculaBuscada);
                    if (pos != -1)
                    {
                        Console.WriteLine($"La matrícula {matriculaBuscada} se encuentra en la posición {pos} del registro.");
                    }
                    else
                    {
                        Console.WriteLine($"La matrícula {matriculaBuscada} no se encuentra en el registro.");
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 6:
                    Console.WriteLine("Proceso Terminado");
                    break;
                default:
                    Console.WriteLine("opcion invalida");
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
            int dias;

            Console.WriteLine("Necesita generar un ticket.\n");
            Console.WriteLine("Ingrese cantidad de vehículos:");
            int cantidadVehiculos = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            for (int i = 0; i < cantidadVehiculos; i++)
            {
                total += SeleccionarTipoVehiculo();
                Console.WriteLine("Ingrese la matrícula del vehículo {0}:", i + 1);
                string identificacion = Console.ReadLine();
                RegistrarMatricula(identificacion);
                Console.Clear();
            }
            Console.WriteLine("Ingrese cantidad de días (1 - 10):");
            dias = Convert.ToInt32(Console.ReadLine());
            total = AplicarPorcentaje(total, dias);
            totalConImpuestos = AplicarImpuestos(total);
            rec += totalConImpuestos;

            Console.WriteLine($"Total a pagar por {dias} días y {cantidadVehiculos} vehículos: {total:F2} + impuestos: {totalConImpuestos:F2}.");
            Console.Write("Pulse una tecla para seguir...");
            Console.ReadKey();
            Console.Clear();
        }

        static public double AplicarPorcentaje(double total, int dias)
        {

            double valor;
            switch (dias)
            {
                case 1:
                    valor = total * 1 + total;
                    break;
                case 2:
                    valor = total * 2.20 + total;
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
            Debug.WriteLine("valor de porcentaje solo con impuesto de dias: {0:F2}", valor);

            AplicarImpuestos(valor);
            return valor;
        }
        static public double AplicarImpuestos(double valor)
        {
            double iva = valor * 0.21;
            Debug.WriteLine("el recargo de iva: {0:F2}", iva);
            double impuestoAconcagua = valor * 0.15;
            double totalConImpuesto = valor + iva + impuestoAconcagua;
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
        #region // parte 2
        //La funcionalidad a agregar es:
        // Registrar la cadena de identificación del vehículo durante el control de acceso
        // Disponer de la opción en el menú de listar ordenados de menor a mayor las identificaciones que han sido registradas.
        //Disponer de una pantalla que permita buscar por un identificador dado en el registro del sistema.

        public static void RegistrarMatricula(string identificacion)
        {
            if (indiceMatriculas < 300)
            {
                matricula[indiceMatriculas] = identificacion;
                indiceMatriculas++;
            }
            else
            {
                Console.WriteLine("Espacio insuficiente para registrar la matrícula.");
            }
        }
        static public void MostrarMatricula()
        {
            Console.WriteLine("Matrículas registradas:");
            for (int i = 0; i < indiceMatriculas; i++)
            {
                Console.WriteLine("Identificador {0}: {1}", i + 1, matricula[i]);
            }
            Console.Write("Pulse una tecla para seguir...");
            Console.ReadKey();
            Console.Clear();

        }
        public static void OrdenarMatriculas()
        {
            for (int i = 0; i < indiceMatriculas - 1; i++)
            {
                for (int j = i + 1; j < indiceMatriculas; j++)
                {
                    if (EsMenor(matricula[j], matricula[i]))
                    {
                        string aux = matricula[i];
                        matricula[i] = matricula[j];
                        matricula[j] = aux;
                    }
                }
            }
        }
        public static bool EsMenor(string str1, string str2)
        {
            int i = 0;
            while (str1[i] != '\0' && str2[i] != '\0')
            {
                if (str1[i] < str2[i])
                    return true;
                else if (str1[i] > str2[i])
                    return false;

                i++;
            }

            return str1[i] == '\0';
        }
        public static int BuscarMatricula(string[] matriculas, string matriculaBuscada)
        {
            int i = 0;
            int pos = -1;
            while (pos == -1 && i < indiceMatriculas)
            {
                if (matriculas[i] == matriculaBuscada)
                {
                    pos = i + 1;

                }
                i++;
            }
            return pos;

        }
        #endregion
    }
}