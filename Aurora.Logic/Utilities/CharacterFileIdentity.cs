using Builder.Presentation.Models;

namespace Builder.Presentation.Utilities;

/// <summary>
/// Compares character files by their normalized on-disk path. CharacterFile instances are
/// recreated whenever the browser list refreshes, so reference equality is not a stable identity.
/// </summary>
public static class CharacterFileIdentity
{
    public static bool RefersToSameFile(CharacterFile? left, CharacterFile? right) =>
        left is not null
        && right is not null
        && RefersToSameFile(left.FilePath, right.FilePath);

    public static bool RefersToSameFile(string? leftPath, string? rightPath)
    {
        if (string.IsNullOrWhiteSpace(leftPath) || string.IsNullOrWhiteSpace(rightPath))
            return false;

        string left = Normalize(leftPath);
        string right = Normalize(rightPath);
        StringComparison comparison = OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;

        return string.Equals(left, right, comparison);
    }

    public static void EnsureSameFile(CharacterFile owner, CharacterFile target)
    {
        if (RefersToSameFile(owner, target))
            return;

        throw new InvalidOperationException(
            $"Refusing to save '{Path.GetFileName(target.FilePath)}' because the active tab owns " +
            $"'{Path.GetFileName(owner.FilePath)}'. Reload the character tabs and try again.");
    }

    private static string Normalize(string path)
    {
        try
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
        }
        catch (Exception) when (path.Length > 0)
        {
            return Path.TrimEndingDirectorySeparator(path);
        }
    }
}
