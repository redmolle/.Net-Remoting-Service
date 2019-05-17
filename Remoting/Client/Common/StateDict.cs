using System.Collections.Generic;

public static class StateDict
{
    public const int Create = 1;
    public const int Update = 2;
    public const int Delete = 3;
    private static string CreateName { get { return "создать"; } }
    private static string UpdateName { get { return "изменить"; } }
    private static string DeleteName { get { return "удалить"; } }


    public static readonly List<KeyValuePair<int, string>> Names =
        new List<KeyValuePair<int, string>>() {
            new KeyValuePair<int, string>(Create, CreateName),
            new KeyValuePair<int, string>(Update, UpdateName),
            new KeyValuePair<int, string>(Delete, DeleteName)
        };
}