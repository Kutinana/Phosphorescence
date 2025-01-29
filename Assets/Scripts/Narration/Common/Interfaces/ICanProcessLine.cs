namespace Phosphorescence.Narration
{
    public interface ICanProcessLine
    {
        public void Process(OnLineReadEvent line);
    }

    public interface ICanProcessLines
    {
        public void Process(OnLinesReadEvent lines);
    }
}