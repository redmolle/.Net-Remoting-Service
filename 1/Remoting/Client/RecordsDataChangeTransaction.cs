using System;

[Serializable]
public class RecordsDataChangeTransaction
{
    public RecordDataObject Old { get; set; }
    public RecordDataObject New { get; set; }
    public DateTime ChangeDate { get; set; }
}
