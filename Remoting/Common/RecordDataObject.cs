using System;

public class RecordDataObject
{
    public int id { get; private set; }
    public string StringField { get; set; }
    public DateTime DateField { get; set; }

    public RecordDataObject() { }
    public RecordDataObject(int _id, string _StringField, DateTime _DateField)
    {
        id = _id;
        StringField = _StringField;
        DateField = _DateField;
    }
}