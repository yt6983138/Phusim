using System;

public class Language
{
    public static string LangFilePath = @"";
    private static LanguageItems Lang; // idk y i cant use nullable
    public static void Init()
    {
        try
        {
            LangFilePath = Resource.LangPath + Resource.LangFileName;
            LoadLang();
        }
        catch (Exception e)
        {
            SetDefaultLang();
            SaveLang();
            throw e;
        }
    }
    public static void SetDefaultLang()
    {
        LangFilePath = Resource.LangPath + Resource.LangFileName;
        Lang = Resource.DefaultLangEn;
    }
    public static LanguageItems ReadLang()
    {
        return Lang;
    }
    public static void WriteLang(LanguageItems lang)
    {
        Lang = lang;
    }
    public static void LoadLang()
    {
        Lang = Serializer.DeserializeJson<LanguageItems>(LangFilePath);
    }
    public static void SaveLang() // dont use this
    {
        Serializer.SerializeJson<LanguageItems>(LangFilePath, Lang);
    }
}