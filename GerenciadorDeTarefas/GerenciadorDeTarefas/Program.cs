﻿using GerenciadorDeTarefas.Classes;
using GerenciadorDeTarefas.Enum;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace GerenciadorDeTarefas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TarefaRepository<Tarefas> listaTarefas = new TarefaRepository<Tarefas>();

            int opcao = -1;
            while (opcao != 0)
            {
                Console.Clear();
                ExibirCabecalhoGeral();

                bool opcaoCorreta = true;
                do
                {
                    try
                    {
                        opcao = int.Parse(Console.ReadLine());
                        opcaoCorreta = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Opção inválida! Tente novamente: ");
                    }
                } while (opcaoCorreta);

                Console.WriteLine();
                switch (opcao)
                {
                    case 1:
                        //Adicionar tarefa
                        Console.Clear();
                        AdicionarTarefa();
                        Console.Clear();
                        Console.WriteLine("\n\nDeseja realizar outra operação? \n=> Enter para continuar\n=> 0 para encerrar");
                        Console.ReadKey();
                        break;
                    case 2:
                        //Editar tarefa
                        Console.Clear();
                        EditarTarefa();
                        Console.WriteLine("\n\nDeseja realizar outra operação? \n=> Enter para continuar\n=> 0 para encerrar");
                        Console.ReadKey();
                        break;
                    case 3:
                        //Remover tarefa
                        Console.Clear();
                        RemoverTarefa();
                        Console.WriteLine("\n\nDeseja realizar outra operação? \n=> Enter para continuar\n=> 0 para encerrar");
                        Console.ReadKey();
                        break;
                    case 4:
                        //Visualizar tarefa
                        Console.Clear();
                        VisualizarTarefa();
                        Console.WriteLine("\n\nDeseja realizar outra operação? \n=> Enter para continuar\n=> 0 para encerrar");
                        Console.ReadKey();
                        break;
                    case 5:
                        //Salvar tarefa
                        Console.Clear();
                        SalvarTarefa();
                        Console.WriteLine("\n\nDeseja realizar outra operação? \n=> Enter para continuar\n=> 0 para encerrar");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Saindo do sistema...");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Pressione enter para continuar");
                        Console.ReadKey();
                        break;
                }
            }

            static void ExibirCabecalhoGeral()
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("      GERENCIADOR DE TAREFAS     ");
                Console.WriteLine("\n================================\n\n");
                Console.WriteLine("Opções do sistema: ");
                Console.WriteLine("1 - Adicionar tarefa");
                Console.WriteLine("2 - Editar tarefa");
                Console.WriteLine("3 - Remover tarefa");
                Console.WriteLine("4 - Visualizar tarefa");
                Console.WriteLine("5 - Salvar tarefa");
                Console.WriteLine("0 - Sair");
            }

            void AdicionarTarefa()
            {
                var culture = new CultureInfo("pt-br");

                Console.WriteLine("================================\n");
                Console.WriteLine("         ADICIONAR TAREFA         ");
                Console.WriteLine("\n================================\n\n");

                Console.WriteLine("Título:");
                string titulo = Console.ReadLine();

                Console.WriteLine("\nDescrição:");
                string descricao = Console.ReadLine();

                bool continuar = false;
                while (!continuar)
                {
                    try
                    {
                        Console.WriteLine("\nData de conclusão (Formato dd/mm/aaaa):");
                        string data = Console.ReadLine();
                        DateTime dataConclusao = DateTime.ParseExact(data, "dd/MM/yyyy", culture);

                        Tarefas novaTarefa = new Tarefas(titulo, descricao, dataConclusao);
                        listaTarefas.tarefas.Add(novaTarefa);

                        Console.WriteLine("Tarefa adicionada com sucesso!");
                        continuar = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Formato inválido, digite novamente (formato dd/mm/aaaa)");
                        continuar = false;
                    }
                }
            }

            void EditarTarefa()
            {
                var culture = new CultureInfo("pt-br");
                Console.WriteLine("================================\n");
                Console.WriteLine("         EDITAR TAREFA         ");
                Console.WriteLine("\n================================\n\n");

                if (listaTarefas.tarefas.Count == 0)
                {
                    Console.WriteLine("Nenhuma tarefa encontrada!");
                }
                else
                {
                    Console.WriteLine("Escolha o número da tarefa que deseja editar: ");
                    for (int i = 0; i < listaTarefas.tarefas.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {listaTarefas.tarefas[i].Titulo}");
                    }
                    int numeroTarefa = int.Parse(Console.ReadLine());

                    foreach (var tarefa in listaTarefas.tarefas)
                    {
                        if (listaTarefas.tarefas.IndexOf(tarefa) == (numeroTarefa - 1))
                        {
                            Console.WriteLine("Selecione a informação que deseja alterar: ");
                            Console.WriteLine("1. Título");
                            Console.WriteLine("2. Descrição");
                            Console.WriteLine("3. Data de conclusão");
                            Console.WriteLine("4. Status");
                            int resposta = int.Parse(Console.ReadLine());

                            switch (resposta)
                            {
                                case 1:
                                    Console.WriteLine("Novo título: ");
                                    tarefa.Titulo = Console.ReadLine();
                                    Console.WriteLine("Título alterada com sucesso!");
                                    break;
                                case 2:
                                    Console.WriteLine("Nova descrição: ");
                                    tarefa.Descricao = Console.ReadLine();
                                    Console.WriteLine("Descrição alterada com sucesso!");
                                    break;
                                case 3:
                                    bool continuar = false;
                                    while (!continuar)
                                    {
                                        try
                                        {
                                            Console.WriteLine("\nNova data de conclusão (Formato dd/mm/aaaa):");
                                            string data = Console.ReadLine();
                                            DateTime dataConclusao = DateTime.ParseExact(data, "dd/MM/yyyy", culture);

                                            tarefa.DataDeConclusao = dataConclusao;
                                            Console.WriteLine("Data alterada com sucesso!");
                                            continuar = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"Formato inválido, digite novamente (formato dd/mm/aaaa)");
                                            continuar = false;
                                        }
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine($"Novo status: ");
                                    Console.WriteLine($"1. Pendente");
                                    Console.WriteLine($"2. Fazendo");
                                    Console.WriteLine($"3. Concluída");
                                    Console.WriteLine($"4. Cancelada");
                                    int status = int.Parse(Console.ReadLine());
                                    if (status == 1)
                                    {
                                        tarefa.Status = Enum.status.Pendente;
                                    }
                                    else if (status == 2)
                                    {
                                        tarefa.Status = Enum.status.Fazendo;

                                    }
                                    else if (status == 3)
                                    {
                                        tarefa.Status = Enum.status.Concluída;
                                    }
                                    else
                                    {
                                        tarefa.Status = Enum.status.Cancelada;
                                    }
                                    Console.WriteLine("Status alterado com sucesso!");
                                    break;
                                default:
                                    Console.WriteLine("Opção inválida!");
                                    break;
                            }
                        }
                    }
                }
            }

            void RemoverTarefa()
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("         REMOVER TAREFA         ");
                Console.WriteLine("\n================================\n\n");

                if (listaTarefas.tarefas.Count == 0)
                {
                    Console.WriteLine("Nenhuma tarefa cadastrada!");
                }
                else
                {
                    Console.WriteLine("Escolha o número da tarefa que deseja remover: ");
                    for (int i = 0; i < listaTarefas.tarefas.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {listaTarefas.tarefas[i].Titulo}");
                    }
                    int numeroTarefa = int.Parse(Console.ReadLine());

                    foreach (var tarefa in listaTarefas.tarefas.ToList())
                    {
                        if (listaTarefas.tarefas.IndexOf(tarefa) == (numeroTarefa - 1))
                        {
                            listaTarefas.tarefas.Remove(tarefa);
                            Console.Write("Tarefa excluída com sucesso!");
                        }
                    }


                }

            }

            void VisualizarTarefa()
            {
                Console.WriteLine("================================\n");
                Console.WriteLine("         CONSULTAR TAREFA         ");
                Console.WriteLine("\n================================\n\n");

                if (listaTarefas.tarefas.Count == 0)
                {
                    Console.WriteLine("Nenhuma tarefa cadastrada!");
                }
                else
                {
                    Console.WriteLine("Escolha o número da tarefa que deseja consultar: ");
                    for (int i = 0; i < listaTarefas.tarefas.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {listaTarefas.tarefas[i].Titulo}");
                    }
                    int numeroTarefa = int.Parse(Console.ReadLine());

                    foreach (var tarefa in listaTarefas.tarefas)
                    {
                        if (listaTarefas.tarefas.IndexOf(tarefa) == (numeroTarefa - 1))
                        {
                            tarefa.VisualizarTarefa();
                        }
                    }
                }
            }


            void SalvarTarefa()
            {
                bool salvandoTarefa = true;
                do
                {
                    try
                    {
                        Console.WriteLine("Informe o diretório onde seu arquivo será salvo: ");
                        string resposta = Console.ReadLine();
                        string arquivoTarefas = (resposta + "\\dados.txt");
                        if (listaTarefas != null)
                        {
                            foreach (var tarefa in listaTarefas.tarefas)
                            {
                                using (StreamWriter salvar = new StreamWriter(arquivoTarefas, true))
                                {
                                    salvar.WriteLine($"\nDetalhes da tarefa:");
                                    salvar.WriteLine($"Título: {tarefa.Titulo}");
                                    salvar.WriteLine($"Descrição: {tarefa.Descricao}");
                                    salvar.WriteLine($"Data de conclusão: {tarefa.DataDeConclusao}");
                                    salvar.WriteLine($"Status: {tarefa.Status}");
                                    salvar.WriteLine(tarefa.StatusVencimento());
                                    salvar.Close();
                                    Console.WriteLine("Tarefa salva com sucesso!");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nenhuma tarefa salva.");
                        }
                        salvandoTarefa = false;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("Diretório não encontrado!");
                    } catch (UnauthorizedAccessException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (salvandoTarefa == true);
                
                

                
            }
        }
    }
}