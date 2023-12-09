namespace TMS.Core;

public class Setting : Entity
{
    public SettingKey Key { get; set; }
    public string Value { get; set; }
}