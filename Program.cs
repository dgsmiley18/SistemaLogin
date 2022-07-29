using System;
using SistemaLogin.Commands;

namespace SistemaLogin
{
    class Program
    {
        enum Menu
        {
            Login = 1,
            Cadastro = 2,
            Sair = 3
        }

        static void Main(string[] args)
        {
            var executando = false;
            while (!executando)
            {
                Console.WriteLine("Bem-vindo ao Sistema de Login!");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Cadastro");
                Console.WriteLine("3 - Sair");
                Console.WriteLine("Digite a opção desejada: ");
                Menu index = (Menu)int.Parse(Console.ReadLine());

                switch (index)
                {
                    case Menu.Login:
                        Console.Clear();
                        Login.Logando();
                        break;
                    case Menu.Cadastro:
                        Console.Clear();
                        Cadastro.Cadastrando();
                        break;
                    case Menu.Sair:
                        executando = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
