namespace Remoting.Client
{
    public class RecordDataStateObject : RecordDataObject
    {
        public int state { get; private set; }
        public RecordDataObject Data { get; private set; }
        public RecordDataStateObject() { }
        public RecordDataStateObject(RecordDataObject o, int _state) : base(o.id, o.StringField, o.DateField)
        {
            state = _state;
            Data = o;
        }
    }
}
