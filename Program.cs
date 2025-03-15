// See https://aka.ms/new-console-template for more information
public static class DemoProgram
{
    public static void Main()
    {
        NetSyllable.Tester.Test();
        int wordCount = 0;
        int syllableCount = 0;
        string demo = "brittle wine communion";
        NetSyllable.SyllableCounter.Syllable("brittle wine communion", ref syllableCount, ref wordCount);
        Console.WriteLine($"{demo}: {syllableCount} syllables and {wordCount} words");
    }
}
