namespace Aurora.Importer.Models;

class DamageComposite
{
    public BaseApiClass? damage_type { get; set; }
    public Dictionary<string, string>? damage_at_slot_level { get; set; }
}
