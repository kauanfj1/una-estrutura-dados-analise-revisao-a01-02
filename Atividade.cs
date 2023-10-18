using System;
using System.Collections.Generic;
using System.Linq;

class Produto
{
    public string Nome { get; set; }
    public string CodigoDeBarras { get; set; }
    public int Quantidade { get; set; }
    public double PrecoUnitario { get; set; }
    public DateTime DataValidade { get; set; }
}

class Program
{
    static List<Produto> estoque = new List<Produto>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Sistema de Gerenciamento de Estoque\n");
            Console.WriteLine("1. Adicionar Produto");
            Console.WriteLine("2. Atualizar Produto");
            Console.WriteLine("3. Remover Produto");
            Console.WriteLine("4. Buscar Produto");
            Console.WriteLine("5. Calcular Valor Total do Estoque");
            Console.WriteLine("6. Gerar Relatório de Produtos Prestes a Vencer");
            Console.WriteLine("7. Sair\n");

            Console.Write("Escolha a opção (1-7): ");
            int escolha;
            if (int.TryParse(Console.ReadLine(), out escolha))
            {
                switch (escolha)
                {
                    case 1:
                        AdicionarProduto();
                        break;
                    case 2:
                        AtualizarProduto();
                        break;
                    case 3:
                        RemoverProduto();
                        break;
                    case 4:
                        BuscarProduto();
                        break;
                    case 5:
                        CalcularValorTotalEstoque();
                        break;
                    case 6:
                        GerarRelatorioPrestesAVencer();
                        break;
                    case 7:
                        Console.WriteLine("Saindo do programa. Obrigado!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }

    static void AdicionarProduto()
    {
        Console.WriteLine("Adicionar Produto ao Estoque\n");

        Produto novoProduto = new Produto();

        Console.Write("Nome do Produto: ");
        novoProduto.Nome = Console.ReadLine();

        Console.Write("Código de Barras: ");
        novoProduto.CodigoDeBarras = Console.ReadLine();

        Console.Write("Quantidade: ");
        if (int.TryParse(Console.ReadLine(), out int quantidade))
        {
            novoProduto.Quantidade = quantidade;
        }
        else
        {
            Console.WriteLine("Quantidade inválida. Produto não adicionado.");
            return;
        }

        Console.Write("Preço Unitário: ");
        if (double.TryParse(Console.ReadLine(), out double preco))
        {
            novoProduto.PrecoUnitario = preco;
        }
        else
        {
            Console.WriteLine("Preço inválido. Produto não adicionado.");
            return;
        }

        Console.Write("Data de Validade (dd-MM-yyyy): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dataValidade))
        {
            novoProduto.DataValidade = dataValidade;
        }
        else
        {
            Console.WriteLine("Data de Validade inválida. Produto não adicionado.");
            return;
        }

        estoque.Add(novoProduto);
        Console.WriteLine("Produto adicionado com sucesso!");
    }

    static void AtualizarProduto()
    {
        Console.WriteLine("Atualizar Produto\n");

        Console.Write("Digite o Código de Barras do Produto que deseja atualizar: ");
        string codigoDeBarras = Console.ReadLine();

        Produto produtoExistente = estoque.FirstOrDefault(p => p.CodigoDeBarras == codigoDeBarras);

        if (produtoExistente != null)
        {
            Console.Write("Novo Nome do Produto (ou pressione Enter para manter o mesmo): ");
            string novoNome = Console.ReadLine();
            if (!string.IsNullOrEmpty(novoNome))
                produtoExistente.Nome = novoNome;

            Console.Write("Nova Quantidade (ou pressione Enter para manter a mesma): ");
            string novaQuantidadeStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(novaQuantidadeStr))
            {
                if (int.TryParse(novaQuantidadeStr, out int novaQuantidade))
                    produtoExistente.Quantidade = novaQuantidade;
                else
                    Console.WriteLine("Quantidade inválida. Produto não atualizado.");
            }

            Console.Write("Novo Preço Unitário (ou pressione Enter para manter o mesmo): ");
            string novoPrecoStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(novoPrecoStr))
            {
                if (double.TryParse(novoPrecoStr, out double novoPreco))
                    produtoExistente.PrecoUnitario = novoPreco;
                else
                    Console.WriteLine("Preço inválido. Produto não atualizado.");
            }

            Console.Write("Nova Data de Validade (dd-MM-yyyy) (ou pressione Enter para manter a mesma): ");
            string novaDataValidadeStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(novaDataValidadeStr))
            {
                if (DateTime.TryParse(novaDataValidadeStr, out DateTime novaDataValidade))
                    produtoExistente.DataValidade = novaDataValidade;
                else
                    Console.WriteLine("Data de Validade inválida. Produto não atualizado.");
            }

            Console.WriteLine("Produto atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado no estoque.");
        }
    }

    static void RemoverProduto()
    {
        Console.WriteLine("Remover Produto do Estoque\n");

        Console.Write("Digite o Código de Barras do Produto que deseja remover: ");
        string codigoDeBarras = Console.ReadLine();

        Produto produtoParaRemover = estoque.FirstOrDefault(p => p.CodigoDeBarras == codigoDeBarras);

        if (produtoParaRemover != null)
        {
            estoque.Remove(produtoParaRemover);
            Console.WriteLine("Produto removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Produto não encontrado no estoque.");
        }
    }

    static void BuscarProduto()
    {
        Console.WriteLine("Buscar Produto\n");

        Console.WriteLine("1. Buscar por Nome");
        Console.WriteLine("2. Buscar por Código de Barras");
        Console.WriteLine("3. Buscar por Data de Validade");

        Console.Write("Escolha a opção de busca (1-3): ");
        int escolha;
        if (int.TryParse(Console.ReadLine(), out escolha))
        {
            switch (escolha)
            {
                case 1:
                    Console.Write("Digite o Nome do Produto: ");
                    string nome = Console.ReadLine();
                    List<Produto> produtosPorNome = estoque.Where(p => p.Nome.ToLower().Contains(nome.ToLower())).ToList();
                    ExibirProdutosEncontrados(produtosPorNome);
                    break;
                case 2:
                    Console.Write("Digite o Código de Barras do Produto: ");
                    string codigoDeBarras = Console.ReadLine();
                    Produto produtoPorCodigoDeBarras = estoque.FirstOrDefault(p => p.CodigoDeBarras == codigoDeBarras);
                    if (produtoPorCodigoDeBarras != null)
                        ExibirProduto(produtoPorCodigoDeBarras);
                    else
                        Console.WriteLine("Produto não encontrado.");
                    break;
                case 3:
                    Console.Write("Digite a Data de Validade (dd-MM-yyyy): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dataValidade))
                    {
                        List<Produto> produtosPorDataValidade = estoque.Where(p => p.DataValidade.Date == dataValidade.Date).ToList();
                        ExibirProdutosEncontrados(produtosPorDataValidade);
                    }
                    else
                    {
                        Console.WriteLine("Data de Validade inválida.");
                    }
                    break;
                default:
                    Console.WriteLine("Opção de busca inválida.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Opção de busca inválida.");
        }
    }

    static void CalcularValorTotalEstoque()
    {
        double valorTotal = estoque.Sum(p => p.Quantidade * p.PrecoUnitario);
        Console.WriteLine($"O valor total do estoque é: R$ {valorTotal:F2}");
    }

    static void GerarRelatorioPrestesAVencer()
    {
        Console.WriteLine("Gerar Relatório de Produtos Prestes a Vencer\n");

        Console.Write("Digite o número de dias para considerar como limite de validade: ");
        if (int.TryParse(Console.ReadLine(), out int limiteDias))
        {
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = dataAtual.AddDays(limiteDias);

            List<Produto> produtosPrestesAVencer = estoque.Where(p => p.DataValidade >= dataAtual && p.DataValidade <= dataLimite).ToList();

             if (produtosPrestesAVencer.Count > 0)
            {
                ExibirProdutosEncontrados(produtosPrestesAVencer);
            }
            else
            {
                Console.WriteLine("Nenhum produto prestes a vencer encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Número de dias inválido.");
        }
    }

    static void ExibirProdutosEncontrados(List<Produto> produtos)
    {
        if (produtos.Count > 0)
        {
            Console.WriteLine("Produtos encontrados:\n");
            foreach (var produto in produtos)
            {
                ExibirProduto(produto);
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Nenhum produto encontrado.");
        }
    }

    static void ExibirProduto(Produto produto)
    {
        Console.WriteLine($"Nome: {produto.Nome}");
        Console.WriteLine($"Código de Barras: {produto.CodigoDeBarras}");
        Console.WriteLine($"Quantidade: {produto.Quantidade}");
        Console.WriteLine($"Preço Unitário: R$ {produto.PrecoUnitario:F2}");
        Console.WriteLine($"Data de Validade: {produto.DataValidade:dd-MM-yyyy}");
    }
}