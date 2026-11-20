using System;
using System.IO;
using System.Linq;

namespace OrganizadorArquivos;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Uso: OrganizadorArquivos <pasta> [--dry-run]");
            return 1;
        }

        var pasta = args[0];
        var dryRun = args.Contains("--dry-run", StringComparer.OrdinalIgnoreCase);

        if (!Directory.Exists(pasta))
        {
            Console.WriteLine($"[ERRO] Pasta não encontrada: {pasta}");
            return 1;
        }

        var arquivos = Directory.GetFiles(pasta)
            .Select(f => new FileInfo(f))
            .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System))
            .GroupBy(f => ObterCategoria(f.Extension))
            .ToDictionary(g => g.Key, g => g.ToList());

        foreach (var (categoria, itens) in arquivos)
        {
            var destino = Path.Combine(pasta, categoria);
            if (!dryRun)
                Directory.CreateDirectory(destino);

            foreach (var arquivo in itens)
            {
                var caminhoDestino = Path.Combine(destino, arquivo.Name);
                if (File.Exists(caminhoDestino))
                {
                    Console.WriteLine($"[PULAR] {arquivo.Name} já existe em {categoria}/");
                    continue;
                }

                if (dryRun)
                {
                    Console.WriteLine($"[DRY-RUN] {arquivo.Name} -> {categoria}/");
                }
                else
                {
                    File.Move(arquivo.FullName, caminhoDestino);
                    Console.WriteLine($"[OK] {arquivo.Name} -> {categoria}/");
                }
            }
        }

        return 0;
    }

    static string ObterCategoria(string extensao)
    {
        return extensao.ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" or ".webp" => "Imagens",
            ".pdf" or ".doc" or ".docx" or ".txt" or ".rtf" or ".odt" => "Documentos",
            ".xls" or ".xlsx" or ".csv" => "Planilhas",
            ".mp4" or ".mkv" or ".avi" or ".mov" or ".wmv" => "Videos",
            ".mp3" or ".wav" or ".flac" or ".aac" => "Audio",
            ".zip" or ".rar" or ".7z" or ".tar" or ".gz" => "Compactados",
            ".exe" or ".msi" or ".apk" => "Instaladores",
            ".cs" or ".js" or ".ts" or ".py" or ".java" or ".cpp" or ".h" or ".json" or ".xml" or ".yml" or ".yaml" => "Codigo",
            _ => "Outros"
        };
    }
}