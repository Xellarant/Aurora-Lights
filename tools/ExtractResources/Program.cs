using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

var exePath = @"C:\Program Files (x86)\Aurora\Aurora Character Builder\Aurora Builder.exe";
var outDir = @"C:\Users\Ralla\source\repos\Aurora-Lights\Aurora.Logic\Resources\Data\_extracted";
var existingDir = @"C:\Users\Ralla\source\repos\Aurora-Lights\Aurora.Logic\Resources\Data";
const string prefix = "Builder.Presentation.Resources.Data.";

using var fs = File.OpenRead(exePath);
using var pe = new PEReader(fs);
var meta = pe.GetMetadataReader();

// Enumerate manifest resources
var resources = new List<(string Name, int Offset, int Length)>();
foreach (var handle in meta.ManifestResources)
{
    var res = meta.GetManifestResource(handle);
    var name = meta.GetString(res.Name);
    if (!name.StartsWith(prefix)) continue;
    // Implementation: offset into the resources section
    resources.Add((name, (int)res.Offset, 0));
}

Console.WriteLine($"Found {resources.Count} matching resources");

// Read the resource data section
var resourcesSection = pe.GetSectionData(".text") ; // resources are typically in .text or .rsrc
// Actually for managed resources, they're in a specific RVA
var resourcesDir = pe.PEHeaders.CorHeader!.ResourcesDirectory;
var sectionData = pe.GetSectionData(resourcesDir.RelativeVirtualAddress);
var resourceBytes = sectionData.GetContent();

int extracted = 0, skipped = 0;
Directory.CreateDirectory(outDir);

foreach (var (name, offset, _) in resources)
{
    var rel = name[prefix.Length..];
    var parts = rel.Split('.');

    string filePath;
    if (parts.Length >= 2 && parts[^1] == "xml")
    {
        var dir = string.Join(Path.DirectorySeparatorChar, parts[..^2]);
        var file = parts[^2] + ".xml";
        filePath = Path.Combine(outDir, dir, file);
    }
    else
    {
        filePath = Path.Combine(outDir, string.Join(Path.DirectorySeparatorChar, parts));
    }

    var existingPath = filePath.Replace(outDir, existingDir);
    if (File.Exists(existingPath)) { skipped++; continue; }

    // Read length (first 4 bytes at offset) then data
    var length = BitConverter.ToInt32(resourceBytes.Slice(offset, 4).ToArray(), 0);
    var data = resourceBytes.Slice(offset + 4, length).ToArray();

    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
    File.WriteAllBytes(filePath, data);
    extracted++;
}

Console.WriteLine($"Extracted: {extracted}  Skipped (already present): {skipped}");
if (extracted > 0)
{
    Console.WriteLine("\nNew top-level folders:");
    foreach (var d in Directory.GetDirectories(outDir).Select(Path.GetFileName).Order())
        Console.WriteLine("  " + d);
}
