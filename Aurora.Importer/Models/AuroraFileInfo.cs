namespace Aurora.Importer.Models;

internal class AuroraFileInfo
{
    public string RelativePath { get; set; } = "";
    public string FullPath { get; set; } = "";
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Author? Author { get; set; }
    public FileVersion? FileVersion { get; set; }
}
