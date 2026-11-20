# Organizador de Arquivos

Console app em C# (.NET 8) que organiza arquivos por categoria baseada na extensão.

## Uso

```bash
# Organiza de verdade
dotnet run -- <pasta>

# Simula sem mover (dry-run)
dotnet run -- <pasta> --dry-run
```

## Categorias

| Categoria | Extensões |
|-----------|-----------|
| Imagens | .jpg, .png, .gif, .webp, .bmp |
| Documentos | .pdf, .docx, .txt, .odt, .rtf |
| Planilhas | .xlsx, .csv |
| Videos | .mp4, .mkv, .avi, .mov |
| Audio | .mp3, .wav, .flac |
| Compactados | .zip, .rar, .7z, .tar, .gz |
| Instaladores | .exe, .msi, .apk |
| Codigo | .cs, .js, .ts, .py, .java, .json, .xml, .yml |
| Outros | Tudo que não se encaixa acima |

## Requisitos

- .NET 8 SDK

## Build

```bash
dotnet build -c Release
```

Executável em `bin/Release/net8.0/OrganizadorArquivos.exe`.