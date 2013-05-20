namespace Guardian.Tests
{
    public class Thing
    {
        public int? field
        {
            get { return null; }
        }

        public string Property
        {
            get { return null; }
        }

        public Thing NestedThing
        {
            get { return new Thing(); }
        }

        public string Method()
        {
            return null;
        }

        public string Method(string argument)
        {
            return null;
        }
    }
}
