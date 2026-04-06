using System.Text;
using System.Xml;
using Builder.Presentation.Models;

namespace Aurora.App.Services;

/// <summary>
/// Extension methods that patch text-editable fields back to a character XML
/// file without requiring a full CharacterManager round-trip.
/// </summary>
public static class CharacterFileSaveExtensions
{
    /// <summary>
    /// Patches every text-editable node in the character XML with values from
    /// the snapshot, then saves the file. Calculated and element-derived fields
    /// are left untouched.
    /// </summary>
    public static bool SaveTextEdits(this CharacterFile file, CharacterSnapshot snap)
    {
        var path = file.FilePath;
        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            return false;

        var doc = new XmlDocument();
        doc.Load(path);

        var root = doc.DocumentElement;
        if (root == null) return false;

        var buildNode = root["build"];
        if (buildNode == null) return false;

        // ── input node ──────────────────────────────────────────────────────
        var inputNode = buildNode["input"];
        if (inputNode != null)
        {
            SetText(inputNode, "name",               snap.Name);
            SetText(inputNode, "player-name",        snap.PlayerName);
            SetText(inputNode, "gender",             snap.Gender);
            SetText(inputNode, "experience",         snap.Experience.ToString());
            SetText(inputNode, "backstory",          snap.Backstory);
            SetText(inputNode, "background-trinket", snap.Trinket);

            var currency = inputNode["currency"];
            if (currency != null)
            {
                SetText(currency, "copper",    snap.CoinCopper.ToString());
                SetText(currency, "silver",    snap.CoinSilver.ToString());
                SetText(currency, "electrum",  snap.CoinElectrum.ToString());
                SetText(currency, "gold",      snap.CoinGold.ToString());
                SetText(currency, "platinum",  snap.CoinPlatinum.ToString());
                SetText(currency, "equipment", snap.InventoryEquipmentText);
                SetText(currency, "treasure",  snap.InventoryTreasureText);
            }

            var org = inputNode["organization"];
            if (org != null)
            {
                SetText(org, "name",   snap.Organisation);
                SetText(org, "allies", snap.Allies);
            }

            // notes
            var notesNode = inputNode["notes"];
            if (notesNode != null)
            {
                foreach (XmlNode note in notesNode.ChildNodes)
                {
                    if (note.Name != "note") continue;
                    var col = note.Attributes?["column"]?.Value;
                    if (col == "left")  note.InnerText = snap.Notes1;
                    if (col == "right") note.InnerText = snap.Notes2;
                }
            }

            var quest = inputNode["quest"];
            if (quest != null)
                quest.InnerText = snap.InventoryQuestText;
        }

        // ── appearance node ─────────────────────────────────────────────────
        var appearance = buildNode["appearance"];
        if (appearance != null)
        {
            SetText(appearance, "age",    snap.Age);
            SetText(appearance, "height", snap.Height);
            SetText(appearance, "weight", snap.Weight);
            SetText(appearance, "eyes",   snap.Eyes);
            SetText(appearance, "skin",   snap.Skin);
            SetText(appearance, "hair",   snap.Hair);
        }

        // ── spell prepared state (prepared casters only) ─────────────────────
        // Only write prepared state for Cleric/Druid/Wizard/Paladin/Artificer etc.
        // Known casters (Sorcerer/Bard/etc.) have no per-spell prepared toggle.
        if (snap.IsSpellcasterPrepared && snap.SpellLevels.Count > 0)
        {
            // Build lookups by both Id and Name so we can match against whatever the XML uses.
            var preparedById   = snap.SpellLevels.SelectMany(lvl => lvl.Spells)
                .Where(s => !string.IsNullOrEmpty(s.Id))
                .ToDictionary(s => s.Id, s => s.IsPrepared, StringComparer.OrdinalIgnoreCase);
            var preparedByName = snap.SpellLevels.SelectMany(lvl => lvl.Spells)
                .ToDictionary(s => s.Name, s => s.IsPrepared, StringComparer.OrdinalIgnoreCase);

            var magic = buildNode["magic"];
            if (magic != null)
            {
                foreach (XmlNode spellcasting in magic.ChildNodes)
                {
                    if (spellcasting.Name != "spellcasting") continue;
                    var spellsNode = spellcasting["spells"];
                    if (spellsNode == null) continue;
                    foreach (XmlNode spellNode in spellsNode.ChildNodes)
                    {
                        if (spellNode.Name != "spell") continue;

                        // Prefer matching by element ID; fall back to name.
                        var spellId   = spellNode.Attributes?["id"]?.Value;
                        var spellName = spellNode.Attributes?["name"]?.Value;
                        bool isPrepared;
                        if (spellId != null && preparedById.TryGetValue(spellId, out isPrepared))
                        { /* matched by ID */ }
                        else if (spellName != null && preparedByName.TryGetValue(spellName, out isPrepared))
                        { /* matched by name */ }
                        else continue;

                        // Don't toggle always-prepared spells (domain spells etc.).
                        var alwaysPrepared = spellNode.Attributes?["always-prepared"]?.Value;
                        if (alwaysPrepared == "true") continue;

                        var preparedAttr = spellNode.Attributes?["prepared"];
                        if (isPrepared)
                        {
                            if (preparedAttr == null)
                            {
                                var a = doc.CreateAttribute("prepared");
                                a.Value = "true";
                                spellNode.Attributes!.Append(a);
                            }
                            else
                            {
                                preparedAttr.Value = "true";
                            }
                        }
                        else
                        {
                            if (preparedAttr != null)
                                spellNode.Attributes!.Remove(preparedAttr);
                        }
                    }
                }
            }
        }

        // ── write ────────────────────────────────────────────────────────────
        using var writer = new XmlTextWriter(path, Encoding.UTF8)
        {
            Formatting  = Formatting.Indented,
            IndentChar  = '\t',
            Indentation = 1,
        };
        doc.Save(writer);
        return true;
    }

    private static void SetText(XmlNode parent, string childName, string? value)
    {
        var node = parent[childName];
        if (node != null)
            node.InnerText = value ?? "";
    }
}
